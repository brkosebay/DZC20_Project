using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Add this line for TMP_Text


public class GameManager : MonoBehaviour
{
    public PossibleInputs possibleInputs; // Assign this in the inspector
    public TMP_Text problemText; // Assign this in the inspector
    public Transform gateSelectionParent; // Assign this in the inspector
    public static GameManager Instance { get; private set; }
    public LogicGate inputX;
    public LogicGate inputY;
    public LogicGate outputZ;

    private LogicGate firstSelectedGate;
    public GameObject wirePrefab;

    public void SetupRandomPuzzle()
    {
        string randomTable = possibleInputs.GetRandomTruthTable();
        problemText.text = randomTable;
        // Parse the randomTable string and set up your game accordingly
    }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    public void GateClicked(LogicGate clickedGate)
    {
        if (firstSelectedGate == null)
        {
            // First gate clicked, store it
            firstSelectedGate = clickedGate;
        }
        else
        {
            // Second gate clicked, connect the two gates
            firstSelectedGate.Connect(clickedGate);

            // Create a wire between the two gates
            CreateWire(firstSelectedGate.transform.position, clickedGate.transform.position);

            firstSelectedGate = null; // Reset selection for next connection
        }
    }

    public void CreateWire(Vector3 startPosition, Vector3 endPosition)
    {
        // Instantiate the wire prefab as a child of gateSelectionParent
        WireRenderer wire = Instantiate(wirePrefab, startPosition, Quaternion.identity, gateSelectionParent).GetComponent<WireRenderer>();
        wire.SetPositions(startPosition, endPosition);
    }


    public void Submit()
    {
        // Convert the current problem text into a TruthTable object
        TruthTable currentTable = new TruthTable(problemText.text);

        // Iterate through all rows in the truth table and check the logic
        for (int i = 0; i < currentTable.inputX.Length; i++)
        {
            // Set up the inputs for the circuit
            inputX.GetComponent<LogicGate>().output = currentTable.inputX[i];
            inputY.GetComponent<LogicGate>().output = currentTable.inputY[i];

            // Evaluate the logic based on the current inputs
            bool result = outputZ.GetComponent<LogicGate>().Evaluate();

            // If any row doesn't match, the solution is incorrect
            if (result != currentTable.outputZ[i])
            {
                Debug.Log("Incorrect setup.");
                return;
            }
        }

        // If all rows match, the solution is correct
        Debug.Log("Correct! Player won.");
    }
}