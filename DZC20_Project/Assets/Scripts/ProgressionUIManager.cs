using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressionUIManager : MonoBehaviour
{
    public TMP_Text panelText; // Assign this in the Inspector

    void Start()
    {
        UpdatePanelBasedOnGameState();
    }

    private void UpdatePanelBasedOnGameState()
    {
        if (GameStateManager.Instance.changeText)
        {
            panelText.text = GameStateManager.Instance.newTextForPanel;
        }
    }
}

