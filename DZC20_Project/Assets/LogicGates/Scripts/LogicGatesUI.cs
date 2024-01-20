using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // For PointerEventData
using TMPro;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System;

public class LogicGatesUI : MonoBehaviour
{
    public TMP_Text explanationText; // Drag your ExplanationText TMP object here in the inspector
    public TMP_Text titleText; // Drag your TitleText TMP object here in the inspector
    public TMP_Text problemText; // Drag your ProblemText TMP object here in the inspector
    public GameManager gameManager; // Drag your GameManager object here in the inspector
    public GameObject explanationPanel; // Drag your ExplanationPanel GameObject here in the inspector
    public Button nextButton; // Drag your NextButton here in the inspector
    public Button prevButton; // Drag your PrevButton here in the inspector
    public Button startGameButton; // Drag your StartGameButton here in the inspector
    public Button submitButton; // Drag your SubmitButton here in the inspector
    public Image backgroundImage; // Drag your BackgroundImage GameObject here in the inspector
    public float zoomDuration = 2.0f; // Duration of the zoom animation in seconds
    public GameObject inputX;
    public GameObject inputY;
    public GameObject outputZ;
    public List<GameObject> gateImages;

    [System.Serializable]
    public class Gate
    {
        public string name;
        public GameObject prefab;
        public Image uiImage; // The UI Image component that represents the gate
        public EventTrigger trigger; // EventTrigger component for detecting clicks
    }

    public List<Gate> gates;
    public Transform gateSpawnParent; // Assign a parent object in the inspector where the gates will be instantiated


    private string[] explanationSlides = {
        @"A professor has caught you snooping around the lab during the instructions and realized you are not signed up for the course.

        She will only let you leave if you prove to her you are a Computer Science student that knows how logic gates work.",
        "You will be given the input of some variables and an output, and you will have unlimited access to a set of logic gates. Your goal is to design a circuit that will solve the problem.",
        @"HINT: You can use the following logic gates:

    - the 'NOT' gate to invert the value of a variable

    - the 'AND' gate to check if two variables are true 

    - the 'OR' gate to check if at least one variable is true 

    - the 'XOR' gate to check if exactly one variable is true."
    };
    private int currentSlide = 0;

    private void Start()
    {   
        HideGameElements();
        nextButton.onClick.AddListener(NextSlide);
        prevButton.onClick.AddListener(PreviousSlide);
        startGameButton.onClick.AddListener(StartGame);
        submitButton.onClick.AddListener(Submit);

        foreach (var gate in gates)
        {
            if (gate.trigger == null)
            {
                Debug.LogError("EventTrigger not set for gate: " + gate.name);
                continue;
            }

            EventTrigger.Entry clickEntry = new EventTrigger.Entry();
            clickEntry.eventID = EventTriggerType.PointerClick;
            clickEntry.callback.AddListener((data) => OnGateClicked(gate));
            gate.trigger.triggers.Add(clickEntry);
        }

        // Initialize the first slide text
        UpdateSlide();
    }

    private void UpdateSlide()
    {
        titleText.text = "Welcome to the Logic Gates Mini-Game!";
        explanationText.text = explanationSlides[currentSlide];

        prevButton.interactable = currentSlide > 0;
        nextButton.interactable = currentSlide < explanationSlides.Length - 1;
    }

    private void NextSlide()
    {
        if (currentSlide < explanationSlides.Length - 1)
        {
            currentSlide++;
            UpdateSlide();
        }
    }

    private void PreviousSlide()
    {
        if (currentSlide > 0)
        {
            currentSlide--;
            UpdateSlide();
        }
    }

    private void StartGame()
    {
        explanationPanel.SetActive(false);
        gameManager.SetupRandomPuzzle(); // Set up the game with a random truth table
        StartCoroutine(ZoomIntoBackground());
    }

    private IEnumerator ZoomIntoBackground()
    {
        yield return new WaitForSeconds(2.0f); // Wait for 2 seconds

        // Calculate the initial and final scale for the zoom animation
        Vector3 initialScale = backgroundImage.transform.localScale;
        Vector3 finalScale = new Vector3(2.0f, 2.0f, 1.0f); // Adjust the values as needed

        // Calculate the initial and final position for the zoom animation
        Vector3 initialPosition = backgroundImage.transform.localPosition;
        Vector3 finalPosition = new Vector3(0.0f, -200.0f, 0.0f); // Adjust the values as needed

        float startTime = Time.time;
        float elapsedTime = 0.0f;

        // Perform the zoom animation
        while (elapsedTime < zoomDuration)
        {
            float t = elapsedTime / zoomDuration;
            backgroundImage.transform.localScale = Vector3.Lerp(initialScale, finalScale, t);
            backgroundImage.transform.localPosition = Vector3.Lerp(initialPosition, finalPosition, t);
            elapsedTime = Time.time - startTime;
            yield return null;
        }

        // Ensure the final scale is set
        backgroundImage.transform.localScale = finalScale;
        backgroundImage.transform.localPosition = finalPosition;
        // Add any additional logic to start the game here
        ShowGameElements();
    }

    private void Submit()
    {
        // Add any additional logic to submit the answer
    }

    public void OnGateClicked(Gate gate)
    {
        // Instantiate the gate prefab as a child of gateSpawnParent
        GameObject newGate = Instantiate(gate.prefab, Vector3.zero, Quaternion.identity, gateSpawnParent);

        // Adjust the RectTransform to properly position the gate within gateSpawnParent
        RectTransform rectTransform = newGate.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero; // Center in the parent
        rectTransform.localScale = new Vector3(0.7f, 0.7f, 1f); // Set the scale to 0.7 on X and Y

        // Add the Draggable component to the new gate
        newGate.AddComponent<Draggable>();
    }

    
    private void HideGameElements()
    {
        // Hide input X, Y and output Z
        inputX.SetActive(false);
        inputY.SetActive(false);
        outputZ.SetActive(false);
        submitButton.gameObject.SetActive(false);
        problemText.gameObject.SetActive(false);
        
        // Hide all gate images
        foreach (var gateImage in gateImages)
        {
            gateImage.SetActive(false);
        }

        // If you have prefabs already in the scene, deactivate them as well
        foreach (var gate in gates)
        {
            gate.prefab.SetActive(false);
        }
    }

    private void ShowGameElements()
    {
        // Show input X, Y and output Z
        inputX.SetActive(true);
        inputY.SetActive(true);
        outputZ.SetActive(true);
        submitButton.gameObject.SetActive(true);
        problemText.gameObject.SetActive(true);
        
        // Show all gate images
        foreach (var gateImage in gateImages)
        {
            gateImage.SetActive(true);
        }

        // If you have prefabs already in the scene, activate them as well
        foreach (var gate in gates)
        {
            gate.prefab.SetActive(true);
        }
    }



}

