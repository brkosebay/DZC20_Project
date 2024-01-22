using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgressionUIManager : MonoBehaviour
{
    public GameObject progressionPanel; // Assign this to your progression panel in the Inspector
    public TMP_Text messageText; // Assign a Text component to display the message

    public static ProgressionUIManager Instance;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);
    }

    public void ShowNextLocation(string nextLocation)
    {
        if (progressionPanel != null)
        {
            progressionPanel.SetActive(true);
            messageText.text = "You have unlocked a new location: " + nextLocation;
        }
    }

    public void HideProgressionPanel()
    {
        if (progressionPanel != null)
        {
            progressionPanel.SetActive(false);
        }
    }
}