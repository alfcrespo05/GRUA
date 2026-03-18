using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
