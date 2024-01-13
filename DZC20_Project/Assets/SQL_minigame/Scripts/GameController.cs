using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameController : MonoBehaviour
{
    public QueryManager queryManager;
    public UIManager uiManager;

    private GameState currentState;
    private int currentStoryIndex = 0;
    private string[] storySegments;

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
    // Assuming "Classroom" is the expected answer for segment 1
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
        uiManager.DisplayResults("That's not the location we are looking for. Try again.");
    }
}

private void HandleSightingQueryResults(List<SightingResult> results)
{
    // Assuming "Library" is the expected answer for a certain segment
    string expectedAnswer = expectedSightingAnswers[currentStoryIndex];
    bool isCorrectAnswer = (results.Count == 1) && (results[0].location_id == expectedAnswer);

    if (isCorrectAnswer)
    {
        // Correct answer, advance the story
        uiManager.DisplaySightingResults(results);
        AdvanceStory();
    }
    else
    {
        // Incorrect answer, do not advance the story
        uiManager.DisplayResults("No relevant sightings found. Try again.");
    }
}


    public void AdvanceStory()
    {
        currentStoryIndex++;
        if (currentStoryIndex < storySegments.Length)
        {
            uiManager.DisplayNarrative(storySegments[currentStoryIndex]);
            UpdateGameState(); // Update game state based on new story segment
        }
        else
        {
            uiManager.DisplayNarrative("Congratulations! You've solved the mystery.");
            // Handle game completion
        }
    }

    private void UpdateGameState()
    {
        // Update the game state based on the current story segment
        // For example:
        if (currentStoryIndex == 2)
        {
            currentState = GameState.SightingQuery;
        }
        // Add additional logic to update the game state as needed
    }

    private Dictionary<int, string> expectedLocationAnswers = new Dictionary<int, string>
    {
        { 1, "Classroom" }, // Assuming segment 2 expects "Jefferson Hall"
        // Add other segment-to-answer mappings
    };

    private Dictionary<int, string> expectedSightingAnswers = new Dictionary<int, string>
    {
        { 4, "Library" }, // Assuming segment 4 expects a sighting in "Library"
        // Add other segment-to-answer mappings
    };
}



public enum GameState
{
    LocationQuery,
    SightingQuery
    // Add additional game states as needed
}
