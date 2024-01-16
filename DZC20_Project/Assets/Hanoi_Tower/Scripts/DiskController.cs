using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskController : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool isDragging = false;
    private RodController currentRod;

    void Start() {
        // Initialize the current rod (assuming the disks start on a rod)
        currentRod = FindObjectOfType<RodController>(); // Adjust this to correctly find the initial rod
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging) {
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
            transform.position = cursorPosition;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        RodController closestRod = FindClosestRod();

        if (closestRod != null && closestRod != currentRod && closestRod.CanPlaceDisk(gameObject))
        {
            currentRod.RemoveDisk(gameObject);
            closestRod.AddDisk(gameObject);
            currentRod = closestRod;

            FindObjectOfType<GameControllerHanoiTower>().IncrementMoveCount();
        }
        else
        {
            // Snap back to the current rod
            transform.position = currentRod.GetNextDiskPosition();
        }
    }

    private RodController FindClosestRod()
    {
        RodController[] allRods = FindObjectsOfType<RodController>();
        RodController closestRod = null;
        float minDistance = Mathf.Infinity;

        foreach (RodController rod in allRods)
        {
            float distance = Vector3.Distance(transform.position, rod.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestRod = rod;
            }
        }

        return closestRod;
    }
}