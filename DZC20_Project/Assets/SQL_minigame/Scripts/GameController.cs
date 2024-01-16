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
            "After some thought, you recall Alex grabbing lunch at the cafeteria. Maybe someone saw the laptop there. Try querying for sightings in the cafeteria.",
            "The query indicates that someone did see the laptop in the cafeteria! But, it seems it was handed over to campus security.",
            "You rush to the campus security office. One last query should confirm if the laptop is there...",
            "The query confirms it! The laptop was turned into campus security, and Alex can finally breathe a sigh of relief. Well done!"
        };
        hintsDictionary = new Dictionary<int, string[]> {
            { 1, new string[] { "You need to find the name of a location. Use the SELECT statement.", "Look in the Locations table for the location where id equals 1.", "Use WHERE to specify the condition: id = 1 in the Locations table." } },
            { 3, new string[] { "You need to find the name of a location. Use the SELECT statement.", "Look in the Locations table for the location where id equals 1.", "Use WHERE to specify the condition: id = 1 in the Locations table." } },
            { 5, new string[] { "You need to find the name of a location. Use the SELECT statement.", "Look in the Locations table for the location where id equals 1.", "Use WHERE to specify the condition: id = 1 in the Locations table." } },
            { 7, new string[] { "You need to find the name of a location. Use the SELECT statement.", "Look in the Locations table for the location where id equals 1.", "Use WHERE to specify the condition: id = 1 in the Locations table." } },
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
                // Add additional cases for other query types if needed
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

    private void HandleSightingQueryResults(List<SightingResult> results)
    {
        string resultText = results.Count > 0 ? "Location ID: " + results[0].location_id.ToString() + ", Witness ID: " + results[0].witness_id.ToString() + ", Time: " + results[0].Time.ToString() : "No sightings found.";
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
    string hintMessage = string.Join("\n", hints);
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
            expectedSightingAnswers.ContainsKey(currentStoryIndex))
        {
            // If the current story segment expects a query, we set the appropriate game state
            currentState = expectedLocationAnswers.ContainsKey(currentStoryIndex) ?
                GameState.LocationQuery : GameState.SightingQuery;
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
                                        !expectedSightingAnswers.ContainsKey(currentStoryIndex);

        uiManager.ShowContinueButton(shouldShowContinueButton);
    }



    private Dictionary<int, string> expectedLocationAnswers = new Dictionary<int, string>
    {
        { 1, "Classroom" },
        { 5, "Cafeteria" },
    };

    private Dictionary<int, string> expectedSightingAnswers = new Dictionary<int, string>
    {
        { 3, "2"},
        { 7, "Campus Security" }

    };
}



public enum GameState
{
    None,
    LocationQuery,
    SightingQuery
}
