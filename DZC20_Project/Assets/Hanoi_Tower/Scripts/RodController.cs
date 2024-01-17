using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class RodController : MonoBehaviour {    // This list will store the GameObjects of the disks on this rod
    public List<GameObject> disksOnRod = new List<GameObject>();

    // Method to add a disk to this rod
    public void AddDisk(GameObject disk)
    {
        // Add the disk to the list
        disksOnRod.Add(disk);
        // Update the disk's RodController reference
        disk.GetComponent<DiskController>().currentRod = this;
        // Update the disk's position to be on top of the current top disk
        UpdateDiskPosition(disk);
    }

    // Method to remove a disk from this rod
    public void RemoveDisk(GameObject disk)
    {
        // Remove the disk from the list
        disksOnRod.Remove(disk);
    }

    // Method to get the top disk on this rod
    public GameObject GetTopDisk()
    {
        // The top disk is the last one in the list
        if (disksOnRod.Count > 0)
            return disksOnRod.Last();
        return null; // Return null if there are no disks
    }

    // Method to update a disk's position after it has been moved to this rod
    private void UpdateDiskPosition(GameObject disk)
    {
        // Implement the logic to position the disk correctly on the rod
        // You might want to adjust the position based on the number of disks
        float yOffset = 0.1f; // This should be set to the appropriate value for your game
        disk.transform.position = new Vector3(transform.position.x, transform.position.y + disksOnRod.Count * yOffset, transform.position.z);
    }

}
