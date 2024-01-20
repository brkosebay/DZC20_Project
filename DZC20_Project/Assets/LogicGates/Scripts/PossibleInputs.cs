using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TruthTable
{
    public bool[] inputX;
    public bool[] inputY;
    public bool[] outputZ;

    public TruthTable(string table)
    {
        string[] lines = table.Split('\n');
        // Assuming the first line is the header and can be ignored
        int numberOfEntries = lines.Length - 1;
        inputX = new bool[numberOfEntries];
        inputY = new bool[numberOfEntries];
        outputZ = new bool[numberOfEntries];

        for (int i = 1; i < lines.Length; i++)
        {
            string[] entries = lines[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            inputX[i - 1] = entries[0] == "1";
            inputY[i - 1] = entries[1] == "1";
            outputZ[i - 1] = entries[2] == "1";
        }
    }
}

public class PossibleInputs : MonoBehaviour
{
    public string[] truthTables = new string[]
{
    "Input X   Input Y   Output Z\n" +
    "0         0         0\n" +
    "0         1         1\n" +
    "1         0         1\n" +
    "1         1         0",

    "Input X   Input Y   Output Z\n" +
    "0         0         0\n" +
    "0         1         0\n" +
    "1         0         1\n" +
    "1         1         1",

    "Input X   Input Y   Output Z\n" +
    "0         0         0\n" +
    "0         1         1\n" +
    "1         0         1\n" +
    "1         1         0",

    "Input X   Input Y   Output Z\n" +
    "0         0         0\n" +
    "0         1         0\n" +
    "1         0         0\n" +
    "1         1         1",

    "Input X   Input Y   Output Z\n" +
    "0         0         1\n" +
    "0         1         0\n" +
    "1         0         0\n" +
    "1         1         0",

    "Input X   Input Y   Output Z\n" +
    "0         0         0\n" +
    "0         1         1\n" +
    "1         0         1\n" +
    "1         1         0",

    "Input X   Input Y   Output Z\n" +
    "0         0         0\n" +
    "0         1         0\n" +
    "1         0         1\n" +
    "1         1         1",

    "Input X   Input Y   Output Z\n" +
    "0         0         1\n" +
    "0         1         1\n" +
    "1         0         0\n" +
    "1         1         0",

    "Input X   Input Y   Output Z\n" +
    "0         0         0\n" +
    "0         1         1\n" +
    "1         0         1\n" +
    "1         1         0",

    "Input X   Input Y   Output Z\n" +
    "0         0         1\n" +
    "0         1         0\n" +
    "1         0         0\n" +
    "1         1         0",

    "Input X   Input Y   Output Z\n" +
    "0         0         0\n" +
    "0         1         1\n" +
    "1         0         0\n" +
    "1         1         0",

    "Input X   Input Y   Output Z\n" +
    "0         0         1\n" +
    "0         1         0\n" +
    "1         0         1\n" +
    "1         1         1",

    "Input X   Input Y   Output Z\n" +
    "0         0         0\n" +
    "0         1         0\n" +
    "1         0         0\n" +
    "1         1         1",

    "Input X   Input Y   Output Z\n" +
    "0         0         1\n" +
    "0         1         0\n" +
    "1         0         0\n" +
    "1         1         0",

    "Input X   Input Y   Output Z\n" +
    "0         0         1\n" +
    "0         1         1\n" +
    "1         0         0\n" +
    "1         1         0",

    "Input X   Input Y   Output Z\n" +
    "0         0         0\n" +
    "0         1         0\n" +
    "1         0         0\n" +
    "1         1         1",

    "Input X   Input Y   Output Z\n" +
    "0         0         1\n" +
    "0         1         0\n" +
    "1         0         1\n" +
    "1         1         1",

    "Input X   Input Y   Output Z\n" +
    "0         0         0\n" +
    "0         1         1\n" +
    "1         0         0\n" +
    "1         1         0",

    "Input X   Input Y   Output Z\n" +
    "0         0         1\n" +
    "0         1         1\n" +
    "1         0         1\n" +
    "1         1         0",

    "Input X   Input Y   Output Z\n" +
    "0         0         0\n" +
    "0         1         0\n" +
    "1         0         0\n" +
    "1         1         1",

    "Input X   Input Y   Output Z\n" +
    "0         0         1\n" +
    "0         1         1\n" +
    "1         0         0\n" +
    "1         1         0"
};
    public string GetRandomTruthTable()
    {
        if (truthTables == null || truthTables.Length == 0)
        {
            Debug.LogError("No truth tables are available.");
            return null;
        }
        int randomIndex = UnityEngine.Random.Range(0, truthTables.Length);
        return truthTables[randomIndex];
    }
}

