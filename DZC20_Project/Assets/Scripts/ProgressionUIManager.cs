using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressionUIManager : MonoBehaviour
{
    public TMP_Text panelText; // Assign this in the Inspector
    private Color originalColor = new Color(0x1B / 255f, 0x52 / 255f, 0x34 / 255f); // Hardcoded color
    void Start()
    {
        UpdatePanelBasedOnGameState();
    }
    public static ProgressionUIManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void UpdatePanelBasedOnGameState()
    {
        if (GameStateManager.Instance.changeText)
        {
            panelText.text = GameStateManager.Instance.newTextForPanel;
        }
    }

    public void ShowErrorMessage()
    {
        panelText.text = "Wrong building!";
        panelText.color = Color.red;
        Invoke("RevertToOriginalText", 2); // Revert after 2 seconds
    }

    private void RevertToOriginalText()
    {
        panelText.text = "The next location to go to is: Atlas";
        panelText.color = Color.white;
    }
}

