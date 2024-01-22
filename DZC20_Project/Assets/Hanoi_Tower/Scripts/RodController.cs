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
    public float baseOffset = 0.2f; // Offset from the rod's pivot to the base of the first disk
    public float diskHeight = 3f; // Uniform height of each disk

    // Calculates the position for a disk based on the number of disks already on the rod
    public Vector3 CalculatePosition(GameObject disk)
    {
        // Find the index of the disk on the rod (if it's not on the rod yet, it will be added at the end)
        int index = disksOnRod.IndexOf(disk);
        if (index == -1)
        {
            index = disksOnRod.Count; // Position for the next disk to be added
        }

        // Calculate the y position based on the number of disks below this one
        float newYPosition = transform.position.y + baseOffset + index * diskHeight;

        // Return the new position
        return new Vector3(transform.position.x, newYPosition, transform.position.z);
    }

    // Updates the position of a specific disk
    public void UpdateDiskPosition(GameObject disk)
    {
        Vector3 newPosition = CalculatePosition(disk);
        disk.transform.position = newPosition;
    }

}
