using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    private GameManager gameManager;
    public Text CounterText;

    public int Count = 0;
    public string totalballs;
    private void Start()
    {
        // Find and assign GameManager
        GameObject gameManagerObject = GameObject.Find("GameManager");
        if (gameManagerObject != null)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("GameManager component not found on GameObject.");
            }
        }
        else
        {
            Debug.LogError("GameManager GameObject not found in the scene.");
        }

        // Check if CounterText is assigned
        if (CounterText == null)
        {
            Debug.LogError("CounterText is not assigned in the Inspector.");
        }

        Count = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CounterText != null)
        {
            Count += 1;
            CounterText.text = $"Count: {Count} /";
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (CounterText != null)
        {
            if (collision.gameObject.CompareTag("Balls"))
            {
                Count += 1;
                CounterText.text = $"Count: {Count}/{totalballs}";
            }
            else if (collision.gameObject.CompareTag("BadBall"))
            {
                if (Count > 0)
                {
                    Count -= 1;
                    CounterText.text = $"Count: {Count}";
                }
            }
        }

        if (gameManager != null)
        {
            gameManager.collectedBalls = Count;
        }
    }
}
