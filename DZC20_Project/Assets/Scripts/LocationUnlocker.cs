using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationUnlocker : MonoBehaviour
{
    public string requiredMiniGame; // The name of the mini-game that needs to be completed to unlock this location
    public GameObject unlockedUI; // Reference to a UI element that shows this location is unlocked

    // Start is called before the first frame update
    void Start()
    {
        CheckUnlockStatus();
    }

    void CheckUnlockStatus()
    {
        // Check if the required mini-game has been completed
        if (GameStateManager.Instance.IsMiniGameCompleted(requiredMiniGame))
        {
            UnlockLocation();
        }
        else
        {
            // Optionally, disable interaction if the location is not yet unlocked
            // gameObject.GetComponent<Collider>().enabled = false;
            // gameObject.GetComponent<SpriteRenderer>().color = Color.gray; // Make the location appear "disabled"
        }
    }

    void UnlockLocation()
    {
        // Enable interaction with this location
        // gameObject.GetComponent<Collider>().enabled = true;

        // Update the UI to show that this location is unlocked
        if (unlockedUI != null)
        {
            unlockedUI.SetActive(true);
        }

        // Show a pop-up message if needed
        ShowUnlockMessage();
    }

    void ShowUnlockMessage()
    {
        // Display a pop-up message about the unlocked location
        // You can customize this to show specific information about the location
        Debug.Log("Location unlocked! You can now play the mini-game at " + gameObject.name);
    }
}
