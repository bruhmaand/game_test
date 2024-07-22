using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float timeLimit = 120f; // 2 minutes
    public TMP_Text timerText;
    public GameObject gameOverCanvas; // Reference to the GameOverCanvas


    private float timeRemaining;
    private bool timerRunning = false;

    void Start()
    {
        timeRemaining = timeLimit;
        timerRunning = true;
        gameOverCanvas.SetActive(false); // Make sure GameOverCanvas is hidden initially
    }

    void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                timeRemaining = 0;
                timerRunning = false;
                ShowGameOver();
            }
        }
    }

    void UpdateTimerText()
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void ShowGameOver()
    {
        Debug.Log("Game Over! Time's up!");
        gameOverCanvas.SetActive(true); // Show the GameOverCanvas
        // Stop player movement or any other game actions here if needed
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart the scene
    }
}
