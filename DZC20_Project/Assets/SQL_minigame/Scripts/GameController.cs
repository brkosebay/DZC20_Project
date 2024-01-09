using UnityEngine;

public class GameController : MonoBehaviour
{
    public UIManager uiManager;
    public QueryManager queryManager;
    
    private int currentStoryIndex = 0;
    private string[] storySegments; // Populate this array with your story segments
    private bool isGameOver = false;

    void Start()
    {
        // Initialize the story segments based on the "Lost Laptop" narrative
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

        // Display the first part of the story
        uiManager.DisplayNarrative(storySegments[currentStoryIndex]);
    }

    // Call this method when a query is submitted
    public void OnQuerySubmitted(string query)
    {
        if (isGameOver) return;

        queryManager.ExecuteQuery(query, OnQueryResultReceived);
    }

    // Callback for when query results are received
    private void OnQueryResultReceived(string result)
    {
        // Process the result and determine if the story should advance
        bool shouldAdvanceStory = CheckQueryResult(result);

        if (shouldAdvanceStory)
        {
            AdvanceStory();
        }
        else
        {
            uiManager.DisplayResults(result);
        }
    }

    // Check the query result to determine if it should advance the story
    private bool CheckQueryResult(string result)
    {
        // Implement your logic here to decide if the result should advance the story
        // For example, check if a certain key piece of information was uncovered

        return false; // Change this based on your game logic
    }

    // Call this method at the end of segments that don't require a query
    private void ShowContinueForNextSegment()
    {
        uiManager.ShowContinueButton(true);
    }

    // Advance the story to the next segment
    public void AdvanceStory()
    {
        currentStoryIndex++;
        uiManager.ShowContinueButton(false);

        if (currentStoryIndex < storySegments.Length)
        {
            uiManager.DisplayNarrative(storySegments[currentStoryIndex]);
        }
        else
        {
            GameOver();
        }
    }

    // Handle the end of the game
    private void GameOver()
    {
        isGameOver = true;
        uiManager.DisplayNarrative("Congratulations! You've solved the mystery.");
        // Add any additional game over logic here
    }
}
