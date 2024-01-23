using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;
    private List<string> completedMiniGames = new List<string>();
    private Dictionary<string, string> miniGameToLocationMap = new Dictionary<string, string>();
    
    public bool changeText = false;
    public string newTextForPanel; // Store the new text for the panel

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetChangeTextStatus(bool status, string newText = "")
    {
        changeText = status;
        newTextForPanel = newText;
    }
    public bool AudiUnlocked()
    {
        return changeText;
    }
}
