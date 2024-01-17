using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class GameControllerHanoiTower : MonoBehaviour
{
    public GameObject[] disksAndRods; 
    public GameObject instructionsText;
    public Button startButton; // Assign your start button here
    public GameObject diskText;
    public GameObject rodText;
    public GameObject diskInputFieldGO;
    public GameObject rodInputFieldGO;
    public TMP_InputField diskInputField;
    public TMP_InputField rodInputField;
    public RodController[] rods; // Assign your rods in the inspector

    public void StartGame()
    {
        // Activate disks and rods
        foreach (var item in disksAndRods)
        {
            item.SetActive(true);
        }

        diskText.SetActive(true);
        rodText.SetActive(true);
        
        diskInputFieldGO.SetActive(true);
        rodInputFieldGO.SetActive(true);

        // Hide instructions and start button
        if (instructionsText != null)
            instructionsText.SetActive(false);

        if (startButton != null)
            startButton.gameObject.SetActive(false);
    }

    public void MoveDisk()
    {
        int diskNumber;
        int rodNumber;

        if (int.TryParse(diskInputField.text, out diskNumber) && int.TryParse(rodInputField.text, out rodNumber))
        {
            if (rodNumber >= 1 && rodNumber <= rods.Length)
            {
                RodController targetRod = rods[rodNumber - 1];

                foreach (var rod in rods)
                {
                    if (rod.disksOnRod.Count > 0 && rod.disksOnRod[rod.disksOnRod.Count - 1].name == "Disk" + diskNumber)
                    {
                        if (targetRod.CanReceiveDisk(rod.disksOnRod[rod.disksOnRod.Count - 1]))
                        {
                            GameObject diskToMove = rod.RemoveTopDisk();
                            targetRod.AddDisk(diskToMove);
                            break;
                        }
                    }
                }
            }
        }
    }
}