using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text narrativeText;
    public TMP_Text resultText;
    public TMP_InputField queryInputField;
    public Button submitButton;
    public Button continueButton;
    public GameController gameController;

    void Start()
    {
        // Optionally, you can add a listener to your submit button here
        submitButton.onClick.AddListener(OnSubmitButtonClicked);
        continueButton.onClick.AddListener(OnContinueButtonClicked);

    }

    // Call this method to display the narrative in the UI
    public void DisplayNarrative(string narrative)
    {
        narrativeText.text = narrative;
    }

    // Call this method to display query results in the UI
    public void DisplayResults(string results)
    {
        resultText.text = results;
    }

    // Invoked when the submit button is clicked
    private void OnSubmitButtonClicked()
    {
        string query = queryInputField.text;

        if (!string.IsNullOrEmpty(query))
        {
            gameController.OnQuerySubmitted(query);
        }
        else
        {
            DisplayResults("Please enter a query.");
        }

        // Optionally, clear the input field after submission
        queryInputField.text = "";
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
