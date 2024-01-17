using UnityEngine;
using UnityEngine.SceneManagement;

public class SpriteButton : MonoBehaviour
{
    // Method called when the mouse clicks on the sprite
    private void OnMouseDown()
    {
        // Load the "SqlScene"
       
        SceneManager.LoadScene("SqlScene");
    }
}
