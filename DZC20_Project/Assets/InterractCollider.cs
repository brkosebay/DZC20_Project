using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderTriggerScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "BuildingCollider")
        {
            // Replace "YourSceneName" with the name of the scene you want to load
            SceneManager.LoadScene("SqlScene");
        } else if (other.gameObject.name == "BuildingCollider (3)" || other.gameObject.name == "BuildingCollider (4)")
        {
            // not yet
        } else if (other.gameObject.name == "BuildingCollider (5)")
        {
            // not yet
        }
    }
}
