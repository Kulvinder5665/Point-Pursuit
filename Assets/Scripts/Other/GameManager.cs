using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private string objectTag = "Balls";
    private int totalBalls;
    [SerializeField] private int nextSceneIndex;

    public int collectedBalls;
    public TextMeshProUGUI timerText;
    public float timeCount;

    public GameObject resumePlane;
    public bool isPaused = false;

    public Button nextLevelButton;

    void Start()
    {
        // Check button and text assignments
        if (nextLevelButton == null)
        {
            Debug.LogError("NextLevelButton is not assigned.");
        }

        if (timerText == null)
        {
            Debug.LogError("TimerText is not assigned.");
        }

        // Find all GameObjects with the specified tag
        GameObject[] balls = GameObject.FindGameObjectsWithTag(objectTag);
        totalBalls = balls.Length;

        // Ensure UI elements are hidden at the start
        if (resumePlane != null)
        {
            resumePlane.SetActive(false);
        }

        if (nextLevelButton != null)
        {
            nextLevelButton.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Check if the player has collected all the balls
        if (collectedBalls >= totalBalls)
        {
            if (resumePlane != null)
            {
                resumePlane.SetActive(true);
            }

            if (nextLevelButton != null)
            {
                nextLevelButton.gameObject.SetActive(true);
            }

            // Uncomment to use the coroutine for transitioning to the next scene
            // StartCoroutine(NextScene());
        }

        Timer();

        if (Input.GetKeyDown(KeyCode.P) && timeCount > 0)
        {
            TogglePause();
        }

        TimerEnd();
    }

    public void NextLevelProgram()
    {
        StartCoroutine(NextScene());
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(nextSceneIndex);
    }

    void Timer()
    {
        timeCount -= Time.deltaTime;
        timeCount = Mathf.Max(0, timeCount);

        if (timerText != null)
        {
            timerText.text = $"Time: {Mathf.Round(timeCount)}";
        }
    }

    void TimerEnd()
    {
        if (Mathf.Approximately(timeCount, 0f))
        {
            if (resumePlane != null)
            {
                resumePlane.SetActive(true);
            }
            Time.timeScale = 0;
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;

        if (resumePlane != null)
        {
            resumePlane.SetActive(isPaused);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        Debug.Log("Restart is working, Time.timeScale set to 1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogError("Next scene index is out of range.");
        }
    }
}
