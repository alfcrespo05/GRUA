using UnityEngine;
using TMPro;

public class TargetZone : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;

    public float moveSpeed = 2f;
    public float moveRange = 5f;

    private Vector3 startPosition;

    public BoxSpawner boxSpawner;
    public GameManager gameManager;

    private SoundManager soundManager;

    public bool isMoving = false;

    void Start()
    {
        startPosition = transform.position;
        soundManager = FindObjectOfType<SoundManager>();
    }

    void Update()
    {
        if (isMoving)
        {
            float offset = Mathf.Sin(Time.time * moveSpeed) * moveRange;
            transform.position = startPosition + new Vector3(0, 0, offset);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Caja"))
        {
            float boxSize = other.transform.localScale.x;
            int points = Mathf.RoundToInt(boxSize * 10f);
            score += points;
            FindObjectOfType<GameManager>().BoxDelivered(points);

            UpdateUI();
            soundManager.PlayDelivery();
            Destroy(other.gameObject);
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Puntos: " + score;
    }
    public void SetStartPosition(Vector3 position)
    {
        transform.position = position;
        startPosition = position;
    }

}
