using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodController : MonoBehaviour {
    public List<GameObject> disksOnRod = new List<GameObject>();

    public void AddDisk(GameObject disk)
    {
        disksOnRod.Add(disk);
        UpdateDiskPositions();
    }

    public GameObject RemoveTopDisk()
    {
        if (disksOnRod.Count == 0) return null;
        GameObject disk = disksOnRod[disksOnRod.Count - 1];
        disksOnRod.RemoveAt(disksOnRod.Count - 1);
        return disk;
    }

    private void UpdateDiskPositions()
    {
        // Update the position of each disk based on its order in the list
        for (int i = 0; i < disksOnRod.Count; i++)
        {
            // Adjust this logic to set the position based on your game's layout
            disksOnRod[i].transform.position = new Vector3(transform.position.x, transform.position.y + i, transform.position.z);
        }
    }

    public bool CanReceiveDisk(GameObject disk)
    {
        if (disksOnRod.Count == 0) return true;
        return disk.GetComponent<DiskController>().Size < disksOnRod[disksOnRod.Count - 1].GetComponent<DiskController>().Size;
    }

}
