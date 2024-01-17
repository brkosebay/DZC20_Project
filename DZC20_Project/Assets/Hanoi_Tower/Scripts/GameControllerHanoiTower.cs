using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System.Linq;

public class GameControllerHanoiTower : MonoBehaviour
{
    public GameObject[] disksAndRods; 
    public GameObject instructionsText;
    public Button startButton; // Assign your start button here
    public GameObject diskText;
    public GameObject rodText;
    public GameObject diskInputFieldGO;
    public GameObject rodInputFieldGO;
    public TMP_Dropdown diskDropdown;
    public TMP_Dropdown rodDropdown;
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
    public void PlayMove()
    {
        // Dropdown values are 1-based, rods array is 0-based
        int diskIndex = diskDropdown.value; // Convert to 0-based index
        int rodIndex = rodDropdown.value; // Convert to 0-based index

        // Check if the selections are valid (not the default "Select" option)
        if (diskIndex >= 0 && rodIndex >= 0)
        {
            // Get the selected disk and rod
            RodController targetRod = rods[rodIndex];
            GameObject diskToMove = rods.SelectMany(rod => rod.disksOnRod)
                                        .FirstOrDefault(disk => disk.GetComponent<DiskController>().diskIndex == diskIndex);

            // Check if there's a disk to move and if the move is valid
            if (diskToMove != null && IsValidMove(diskToMove, targetRod))
            {
                // Perform the move
                MoveDisk(diskToMove, targetRod);
            }
            else
            {
                // Handle invalid move (e.g., display a message to the player)
                Debug.Log("Invalid move!");
            }
        }
        else
        {
            // Handle selection of default dropdown option (e.g., display a message to the player)
            Debug.Log("Disk selected:" + diskIndex + "Rod selected"+rodIndex);
            Debug.Log("Please select a disk and a target rod.");
        }
    }

    private bool IsValidMove(GameObject disk, RodController targetRod)
    {
        // Implement the validation logic here
        // A move is valid if the target rod is empty or the top disk on the target rod is larger than the disk to move
        DiskController diskController = disk.GetComponent<DiskController>();
        GameObject topDisk = targetRod.GetTopDisk();

        return topDisk == null || diskController.Size < topDisk.GetComponent<DiskController>().Size;
    }

    private void MoveDisk(GameObject disk, RodController targetRod)
    {
        // Find the current rod of the disk
        DiskController diskController = disk.GetComponent<DiskController>();
        RodController currentRod = diskController.currentRod;

        // If the disk is already on a rod, remove it from that rod
        if (currentRod != null)
        {
            currentRod.RemoveDisk(disk);
        }

        // Add the disk to the target rod
        targetRod.AddDisk(disk);

        // Update the disk's currentRod reference to the new rod
        diskController.currentRod = targetRod;

        // Optionally, if you want to animate the disk movement, you could do so here.
        // For now, let's just set the disk's position above the target rod.
        float diskHeight = 0.1f; // This should be the height of a disk
        int diskCount = targetRod.disksOnRod.Count;
        Vector3 newPosition = targetRod.transform.position + new Vector3(0, diskHeight * diskCount, 0);
        disk.transform.position = newPosition;
    }

}
