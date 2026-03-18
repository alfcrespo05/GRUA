using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtons : MonoBehaviour
{
    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        LevelManager.instance.LoadLevel(LevelManager.instance.currentLevel);
        FindObjectOfType<CraneController>().ResetPosition();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        LevelManager.instance.LoadLevel(1);
        FindObjectOfType<CraneController>().ResetPosition();
    }
}
