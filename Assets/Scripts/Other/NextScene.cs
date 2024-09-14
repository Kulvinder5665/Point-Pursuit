using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    
    public int nextSceneIndex; // Index of the scene to load

    private GameManager gameManager; // Corrected the type name

    private void Start()
    {
        // Ensure the GameManager GameObject is named correctly and the script is attached
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); // Corrected GameObject name and script name
    }

    //private void OnTriggerEnter(Collider other)
    //{
 

    //    if (gameManager.collectedBalls >= gameManager.totalBalls)
    //    {

    //        if (other.CompareTag("Player"))
    //        {
    //            SceneManager.LoadScene(nextSceneIndex);
    //        }
    //    }
    //}
}
