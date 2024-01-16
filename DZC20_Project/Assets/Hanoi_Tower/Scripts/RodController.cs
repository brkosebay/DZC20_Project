using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RodController : MonoBehaviour
{
    public List<GameObject> disksOnRod = new List<GameObject>();

    public bool CanPlaceDisk(GameObject disk)
    {
        if (disksOnRod.Count == 0) return true; // The rod is empty

        GameObject topDisk = disksOnRod[disksOnRod.Count - 1]; // Get the top disk on the rod
        float topDiskSize = topDisk.transform.localScale.x; // Assuming disk size is determined by the x scale
        float placingDiskSize = disk.transform.localScale.x;

        return placingDiskSize < topDiskSize; // Can place if the new disk is smaller
    }


    public void AddDisk(GameObject disk)
    {
        disksOnRod.Add(disk);
        disk.transform.position = GetNextDiskPosition();
    }

    public void RemoveDisk(GameObject disk)
    {
        disksOnRod.Remove(disk);
    }

    public Vector3 GetNextDiskPosition()
    {
        float yOffset = 0.1f; // Adjust this based on the thickness of your disks
        float height = disksOnRod.Count * yOffset; // Height is based on the number of disks
        return new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
    }

}
