using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public KeyCode pauseKey = KeyCode.Escape;
    private bool isPaused = false;
    private GameManager gameManager;

    void Start()
    {
        pausePanel.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            // Only allow pause if game is not over
            if (gameManager.gameOver) return;

            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        Resume();
        LevelManager.instance.LoadLevel(LevelManager.instance.currentLevel);
        FindObjectOfType<CraneController>().ResetPosition();
    }

    public void RestartGame()
    {
        Resume();
        LevelManager.instance.LoadLevel(1);
        FindObjectOfType<CraneController>().ResetPosition();
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
