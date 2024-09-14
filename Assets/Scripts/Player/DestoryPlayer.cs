using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestoryPlayer : MonoBehaviour
{
    private GameManager gameManagerX;
    public GameObject PlayerPrefab;

    private void Start()
    {
        // Find and assign GameManager
        GameObject gameManagerObject = GameObject.Find("GameManager");
        if (gameManagerObject != null)
        {
            gameManagerX = gameManagerObject.GetComponent<GameManager>();
            if (gameManagerX == null)
            {
                Debug.LogError("GameManager component not found on GameObject.");
            }
        }
        else
        {
            Debug.LogError("GameManager GameObject not found in the scene.");
        }
    }

    void Update()
    {
        // Check if player has fallen below a certain height
        if (transform.position.y < -20)
        {
            HandlePlayerFall();
        }
    }

    private void HandlePlayerFall()
    {
        gameObject.SetActive(false);

        // Ensure gameManagerX is assigned before accessing its properties
        if (gameManagerX != null)
        {
            gameManagerX.resumePlane.SetActive(true);
           
        }
    }

   
}
