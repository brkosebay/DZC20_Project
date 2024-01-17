using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System;

public class UIManager : MonoBehaviour
{
    public TMP_Text narrativeText;
    public TMP_Text explanationText;
    public TMP_Text explanationTitle;
    public TMP_Text resultText;
    public TMP_InputField queryInputField;
    public Button submitButton;
    public Button continueButton;
    public Button nextExplanationButton;
    public Button startGameButton;
    public Button prevExplanationButton;
    public GameObject explanationPanel;
    public GameController gameController;
    public GameObject hintBubblePanel;
    public TextMeshProUGUI hintText;
    private int currentPageIndex = 0;
    public Dictionary<int, string[]> explanationPages;

    void Start()
    {
        // Optionally, you can add a listener to your submit button here
        submitButton.onClick.AddListener(OnSubmitButtonClicked);
        continueButton.onClick.AddListener(OnContinueButtonClicked);
        prevExplanationButton.onClick.AddListener(PrevExplanationPage);
        nextExplanationButton.onClick.AddListener(NextExplanationPage);
        startGameButton.onClick.AddListener(OnGameStartButtonClicked);
        explanationPages = new Dictionary<int, string[]> {
    {0, new string[] {
        "Welcome to The SQL Laptop Mystery!",
        "Your task here is to navigate the story using SQL Queries, in hopes of recovering a lost laptop inside the TU/e campus. Before you start, you must know a few things. This information will be presented to you in the next pages."
    }},
    {1, new string[] {
        "Students Table",
        "The <b>Students</b> table contains information about each student on campus. Here's what each column represents:\n\n" +
        "<b>id</b>: A unique number identifying each student.\n" +
        "<b>name</b>: The full name of the student.\n" +
        "<b>major</b>: The major subject the student is studying.\n\n" +
        "Example Entry:\n" +
        "- ID: 1\n" +
        "- Name: Alex Doe\n" +
        "- Major: Computer Science"
    }},
    {2, new string[] {
        "Professors Table",
        "The <b>Professors</b> table holds details about the professors:\n\n" +
        "<b>id</b>: A unique identifier for each professor.\n" +
        "<b>name</b>: The name of the professor.\n\n" +
        "Example Entry:\n" +
        "- ID: 1\n" +
        "- Name: Prof. John Smith"
    }},
    {3, new string[] {
        "Classes Table",
        "In the <b>Classes</b> table, you can find information about the classes taught on campus:\n\n" +
        "<b>id</b>: Each class's unique identifier.\n" +
        "<b>name</b>: The name of the class.\n" +
        "<b>professor_id</b>: The ID of the professor teaching the class, which links to the Professors table.\n\n" +
        "Example Entry:\n" +
        "- ID: 1\n" +
        "- Name: Intro to Computer Science\n" +
        "- Professor ID: 1"
    }},
    {4, new string[] {
        "Locations Table",
        "The <b>Locations</b> table lists various places on campus:\n\n" +
        "<b>id</b>: A unique number for each location.\n" +
        "<b>name</b>: The name of the location, such as 'Library' or 'Cafeteria'.\n\n" +
        "Example Entry:\n" +
        "- ID: 1\n" +
        "- Name: Jefferson Hall"
    }},
    {5, new string[] {
        "LaptopSightings Table",
        "The <b>LaptopSightings</b> table is where sightings of lost laptops are recorded:\n\n" +
        "<b>id</b>: A unique identifier for the sighting.\n" +
        "<b>location_id</b>: The ID of the location where the laptop was seen, referencing the Locations table.\n" +
        "<b>witness_id</b>: The ID of the student who reported the sighting, linking to the Students table.\n" +
        "<b>time</b>: The time when the laptop was sighted.\n\n" +
        "Example Entry:\n" +
        "- ID: 1\n" +
        "- Location ID: 2 (Library)\n" +
        "- Witness ID: 3 (Jane Doe)\n" +
        "- Time: 10:00 AM"
    }},
    {6, new string[] {
        "Understanding Relationships",
        "Our database tables are connected through relationships:\n\n" +
        "- The <b>Classes</b> table is linked to the <b>Professors</b> table by <b>professor_id</b>.\n" +
        "- The <b>LaptopSightings</b> table refers to both <b>Locations</b> and <b>Students</b> by <b>location_id</b> and <b>witness_id</b>, respectively.\n\n" +
        "Understanding these relationships will help you write accurate SQL queries to solve the mystery of the lost laptop!"
    }},
};


    }

    public void NextExplanationPage()
    {
        currentPageIndex++;
        if (currentPageIndex < explanationPages.Count)
        {
            DisplayExplanation(explanationPages[currentPageIndex]);
        }
    }
    public void PrevExplanationPage()
    {
        currentPageIndex--;
        if (currentPageIndex >= 0)
        {
            DisplayExplanation(explanationPages[currentPageIndex]);
        }
    }

    public void OnGameStartButtonClicked()
    {
        explanationPanel.SetActive(false);
    }

    public void DisplayNarrative(string narrative)
    {
        narrativeText.text = narrative;
    }
    public void DisplayExplanation(string[] explanation)
    {
        explanationTitle.text = explanation[0];
        explanationText.text = explanation[1];
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

    public void DisplayClassResults(List<ClassResult> results)
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
