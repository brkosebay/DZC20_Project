using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);
    }

    public List<string> completedMiniGames = new List<string>();

    public void CompleteMiniGame(string miniGameName)
    {
        if (!completedMiniGames.Contains(miniGameName))
        {
            completedMiniGames.Add(miniGameName);
        }
    }

    public bool IsMiniGameCompleted(string miniGameName)
    {
        return completedMiniGames.Contains(miniGameName);
    }
}
