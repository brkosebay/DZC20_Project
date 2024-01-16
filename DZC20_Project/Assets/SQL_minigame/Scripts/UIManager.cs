using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Text;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public TMP_Text narrativeText;
    public TMP_Text resultText;
    public TMP_InputField queryInputField;
    public Button submitButton;
    public Button continueButton;
    public GameController gameController;
    public GameObject hintBubblePanel;
    public TextMeshProUGUI hintText;

    void Start()
    {
        // Optionally, you can add a listener to your submit button here
        submitButton.onClick.AddListener(OnSubmitButtonClicked);
        continueButton.onClick.AddListener(OnContinueButtonClicked);

    }

    public void DisplayNarrative(string narrative)
    {
        narrativeText.text = narrative;
    }

    public void DisplayLocationResults(List<LocationResult> results)
    {
        StringBuilder resultStringBuilder = new StringBuilder();
        foreach (var result in results)
        {
            resultStringBuilder.AppendLine(result.ToString());
        }
        resultText.text = resultStringBuilder.ToString();
    }

    public void DisplaySightingResults(List<SightingResult> results)
    {
        StringBuilder resultStringBuilder = new StringBuilder();
        foreach (var result in results)
        {
            resultStringBuilder.AppendLine(result.ToString());
        }
        resultText.text = resultStringBuilder.ToString();
    }

    public void DisplayResults(string results)
    {
        resultText.text = results;
    }

    public void ShowHintBubble(string message)
    {
        hintText.text = message;
        hintBubblePanel.SetActive(true);
        StartCoroutine(HideHintBubbleAfterDelay(15f)); 
    }

    // Coroutine to hide the hint bubble after a delay
    private IEnumerator HideHintBubbleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        hintBubblePanel.SetActive(false);
    }

    private void OnSubmitButtonClicked()
    {
        string query = queryInputField.text;
        if (!string.IsNullOrEmpty(query))
        {
            gameController.OnQuerySubmitted(query);
            queryInputField.text = ""; // Clear the input field
        }
    }

    public void ShowContinueButton(bool show)
    {
        continueButton.gameObject.SetActive(show);
    }

    // Add this method to be called when the 'Continue' button is clicked
    public void OnContinueButtonClicked()
    {
        gameController.AdvanceStory();
    }

    // Additional UI management functions can be added here as needed
    // For example, updating UI elements for hints, error messages, etc.
}
