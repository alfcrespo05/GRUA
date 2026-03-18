using UnityEngine;

public class FallDetector : MonoBehaviour
{
    public float platformSize = 10f;
    public GameManager gameManager;

    void Update()
    {
        // Check if crane has gone beyond platform edges
        if (Mathf.Abs(transform.position.x) > platformSize ||
            Mathf.Abs(transform.position.z) > platformSize)
        {
            gameManager.TriggerGameOver("¡Te has caído!");
        }
    }
}
