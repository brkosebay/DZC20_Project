using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderTriggerScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "BuildingCollider")
        {
            if (!GameStateManager.Instance.AudiUnlocked())
            {
                ProgressionUIManager.Instance.ShowErrorMessage();
            }
            // Replace "YourSceneName" with the name of the scene you want to load
            else
            {
                SceneManager.LoadScene("SqlScene");
            }
        } else if (other.gameObject.name == "BuildingCollider (3)" || other.gameObject.name == "BuildingCollider (4)")
        {
            SceneManager.LoadScene("Hanoi Tower");
        } else if (other.gameObject.name == "BuildingCollider (5)")
        {
            SceneManager.LoadScene("LogicGatesScene");
        }
    }
}
