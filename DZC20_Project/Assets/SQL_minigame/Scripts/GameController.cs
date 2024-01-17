using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;

public class GameController : MonoBehaviour
{
    public QueryManager queryManager;
    public UIManager uiManager;

    private GameState currentState;
    private int currentStoryIndex = 0;
    private string[] storySegments;
    private Dictionary<int, string[]> hintsDictionary;

    void Start()
    {
        InitializeStorySegments();
        currentState = GameState.LocationQuery; // Set initial game state
        uiManager.DisplayNarrative(storySegments[currentStoryIndex]);
    }

    void InitializeStorySegments()
    {
        storySegments = new string[] {
            "You receive a panicked message from Alex, your friend: 'Hey, I can't find my laptop! I had it in my last class. Can you help me look for it?' You agree to help and decide to start by finding out where Alex's last class was.",
            "You decide to start at Alex's last class. Query the database to find out where it was.",
            "The query reveals that Alex's last class was in the Jefferson Hall. Maybe the laptop was left there?",
            "Next, you remember Alex mentioning they were headed to the library. Time to check there! Write a query to find any laptop sightings in the library.",
            "Your query shows a laptop was indeed seen in the library, but it wasn't there when you arrived. Where to next?",
            "It's time to figure out where Alex's next class is. Maybe they picked up the laptop and took it with them?",
            "After discovering the location of Alex's next class, you head there, only to find the room empty. The professor might know if Alex was there earlier. It's time to find out who teaches that class.",
            "The professor says they haven't seen Alex since the morning. You remember Alex mentioning visiting the cafeteria. Write a query to find any laptop sightings in the Cafeteria.",
            "The query indicates that someone did see the laptop in the cafeteria! But, it seems it was handed over to campus security.",
            "You rush to the campus security office. One last query should confirm if the laptop is there...",
            "The query confirms it! The laptop was turned into campus security, and Alex can finally breathe a sigh of relief. Well done!"
        };
        hintsDictionary = new Dictionary<int, string[]> {
            { 1, new string[] { "Every place has an identifier. Can you find out the ID that matches the name 'Classroom'?", "Remember, you can use the SELECT statement to retrieve the name of a location based on its ID.", "Think about which table holds the names of locations. You're looking for an ID that's usually the first one." } },
            { 3, new string[] { "There's a table that tracks where laptops have been spotted. Can you find which one it is?", "You're interested in a specific location, the 'Library'. How can you filter records by location name?", "Look for a way to relate the 'Locations' table to the sightings. There's a common piece of information that links them." } },
            { 5, new string[] { "Classes have their own table. Can you find where each class is held?", "You might need to find a class by its ID. Which number represents the first class?", "Use SELECT to retrieve details from the 'Classes' table. Remember to specify which class you're interested in with WHERE."} },
            { 6, new string[] { "Who teaches the class? There's a table for professors that might help.", "Classes are linked to professors. How can you find out which professor is linked to 'Intro to CompSci'?", "JOIN the 'Classes' table with 'Professors' to find the right name. You're looking for a specific class name."} },
            { 7, new string[] { "You've seen how to find laptop sightings in the library. Can you do the same for the cafeteria?", "Each location has a unique name and ID. Use the 'Locations' table to find the ID for 'Cafeteria'.", "Construct a query to list sightings at the 'Cafeteria'. You'll need to match the location name to its ID, as you did before." } },
            { 9, new string[] { "The 'Campus Security Office' is another key location. What's its ID?", "If a laptop ends up at security, it should be recorded. How would you find that record?", "Write a query to check the 'LaptopSightings' for the 'Campus Security Office' by matching its ID from the 'Locations' table."} },
        };
    }

    public void OnQuerySubmitted(string query)
    {
        switch (currentState)
        {
            case GameState.LocationQuery:
                queryManager.ExecuteLocationQuery(query, HandleLocationQueryResults);
                break;
            case GameState.SightingQuery:
                queryManager.ExecuteSightingQuery(query, HandleSightingQueryResults);
                break;
            case GameState.ClassQuery:
                queryManager.ExecuteClassQuery(query, HandleClassQueryResults);
                break;
        }
    }

    private void HandleLocationQueryResults(List<LocationResult> results)
    {
        string resultText = results.Count > 0 ? results[0].name : "No results found.";
        string expectedAnswer = expectedLocationAnswers[currentStoryIndex];
        bool isCorrectAnswer = (results.Count == 1) && (results[0].name == expectedAnswer);

        if (isCorrectAnswer)
        {
            // Correct answer, advance the story
            uiManager.DisplayLocationResults(results);
            AdvanceStory();
        }
        else
        {
            // Incorrect answer, do not advance the story
            uiManager.DisplayResults(resultText + "\nThat's not the location we are looking for. Try again.");
        }
    }

    private void HandleClassQueryResults(List<ClassResult> results)
    {
        string resultText = results.Count > 0 ? "Class Name: " + results[0].name: "No class found.";
        string expectedAnswer = expectedClassAnswers[currentStoryIndex];
        bool isCorrectAnswer = (results.Count == 1) && (results[0].name == expectedAnswer);

        if (isCorrectAnswer)
        {
            // Correct answer, advance the story
            uiManager.DisplayClassResults(results);
            AdvanceStory();
        }
        else
        {
            // Incorrect answer, do not advance the story
            uiManager.DisplayResults(resultText + "\nThat's not the location we are looking for. Try again.");
        }
    }

    private void HandleSightingQueryResults(List<SightingResult> results)
    {
        Debug.Log("started handling result...");
        string resultText = results.Count > 0 ? "Location ID: " + results[0].location_id.ToString() + ", Witness ID: " + results[0].witness_id.ToString() + ", Time: " + results[0].time.ToString() : "No sightings found.";
        string expectedAnswer = expectedSightingAnswers[currentStoryIndex];
        bool isCorrectAnswer = (results.Count == 1) && (results[0].location_id.ToString() == expectedAnswer);

        if (isCorrectAnswer)
        {
            // Correct answer, advance the story
            uiManager.DisplaySightingResults(results);
            AdvanceStory();
        }
        else
        {
            // Incorrect answer, do not advance the story
            uiManager.DisplayResults(resultText + "\nNo relevant sightings found. Try again.");
        }
    }

    public void ShowHints()
    {
        if (!hintsDictionary.ContainsKey(currentStoryIndex))
        {
            Debug.LogError("No hints defined for this story segment.");
            return;
        }

        string[] hints = hintsDictionary[currentStoryIndex];
        string hintMessage = string.Join("\n\n", hints);
        uiManager.ShowHintBubble(hintMessage); // Show hint bubble
    }

    public void AdvanceStory()
    {
        currentStoryIndex++;
        if (currentStoryIndex < storySegments.Length)
        {
            uiManager.DisplayNarrative(storySegments[currentStoryIndex]);
            UpdateGameState(); // Update game state based on new story segment
            ShowContinueButtonIfNeeded(); // Decide whether to show the Continue button
        }
        else
        {
            uiManager.DisplayNarrative("Congratulations! You've solved the mystery.");
            uiManager.ShowContinueButton(false); // Hide the Continue button after the game is complete
                                                 // Handle game completion
        }
    }

    private void UpdateGameState()
    {
        // Logic to update the game state based on the story segment
        if (expectedLocationAnswers.ContainsKey(currentStoryIndex) ||
            expectedSightingAnswers.ContainsKey(currentStoryIndex) ||
            expectedClassAnswers.ContainsKey(currentStoryIndex))
        {
            // If the current story segment expects a query, we set the appropriate game state
            if (expectedLocationAnswers.ContainsKey(currentStoryIndex))
            {
                currentState = GameState.LocationQuery;
            }
            else if (expectedSightingAnswers.ContainsKey(currentStoryIndex))
            {
                currentState = GameState.SightingQuery;
            }
            else if (expectedClassAnswers.ContainsKey(currentStoryIndex))
            {
                currentState = GameState.ClassQuery;
            }
            else
            {
                currentState = GameState.None; // Or another default state
            }

            uiManager.ShowContinueButton(false); // Hide Continue button because a query is needed
        }
        else
        {
            // If no query is expected, we do not change the game state and show the Continue button
            currentState = GameState.None; // Or some other state indicating no query is needed
            uiManager.ShowContinueButton(true); // Show Continue button
        }
    }

    private void ShowContinueButtonIfNeeded()
    {
        // Check if the current segment is in the dictionaries of expected answers
        bool shouldShowContinueButton = !expectedLocationAnswers.ContainsKey(currentStoryIndex) &&
                                        !expectedSightingAnswers.ContainsKey(currentStoryIndex) &&
                                        !expectedClassAnswers.ContainsKey(currentStoryIndex);

        uiManager.ShowContinueButton(shouldShowContinueButton);
    }



    private Dictionary<int, string> expectedLocationAnswers = new Dictionary<int, string>
    {
        { 1, "Classroom" },
    };

    private Dictionary<int, string> expectedClassAnswers = new Dictionary<int, string>
    {
        { 5, "Intro to CompSci"},
        { 6, "Prof. Carter" },
    };

    private Dictionary<int, string> expectedSightingAnswers = new Dictionary<int, string>
    {
        { 3, "2"},
        { 7, "3" },
        { 9, "4" }

    };
}



public enum GameState
{
    None,
    LocationQuery,
    SightingQuery,
    ClassQuery,
    ProfessorQuery
}
