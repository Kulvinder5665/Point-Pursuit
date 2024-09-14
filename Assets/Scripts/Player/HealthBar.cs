using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public float damage = 0.1f; // Ensure damage is set to a meaningful value
    private DestoryPlayer destroyPlayer;
    private GameManager gameManager;

    private void Start()
    {
        // Assign and validate the DestoryPlayer component
        destroyPlayer = GetComponent<DestoryPlayer>();
        if (destroyPlayer == null)
        {
            Debug.LogError("DestoryPlayer component not found on GameObject.");
        }

        // Validate healthSlider
        if (healthSlider == null)
        {
            Debug.LogError("HealthSlider is not assigned.");
        }
        else
        {
            healthSlider.value = 1; // Initialize health to full
        }

        // Find and assign the GameManager
        GameObject gameManagerObject = GameObject.Find("GameManager");
        if (gameManagerObject != null)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("GameManager component not found on GameObject.");
            }
        }
      
    }
    private void Update()
    {
        if (healthSlider != null)
        {
            if (healthSlider.value <= 0)
            {
                if (gameManager != null)
                {
                    gameManager.resumePlane.SetActive(true);
                    Time.timeScale = 0;
                    Debug.Log("Health depleted, Time.timeScale set to 0");
                }
            }
            else if(gameManager.isPaused==false && healthSlider.value>0)
            {
                Time.timeScale = 1;
               
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            healthSlider.value -= damage;
        }
    }
}
