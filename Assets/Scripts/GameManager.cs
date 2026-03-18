using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public float timeLeft = 30f;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI boxCountText;
    public TextMeshProUGUI levelText;

    public bool gameOver = false;
    private int totalBoxes = 0;
    private int deliveredBoxes = 0;

    public float lowTimeThreshold = 10f;
    private bool isLowTime = false;
    private bool timerStarted = false;

   
    private float bestTime = 0f;
    public float totalTime = 0f;
    public int score = 0;
    public float resetTime = 40f;

    public GameObject restartButton;
    public GameObject restartLevelButton;
    public GameObject restartGameButton;

    void Start()
    {

        totalBoxes = GameObject.FindGameObjectsWithTag("Caja").Length;
        timerText.text = "¡ENTREGA UNA CAJA PARA EMPEZAR!";
        timerText.color = Color.yellow;
        levelText.text = "NIVEL " + LevelManager.instance.currentLevel;
        statusText.text = "";
        UpdateUI();
        restartButton.SetActive(false);
        restartButton.SetActive(false);
        restartLevelButton.SetActive(false);
        restartGameButton.SetActive(false);
    }

    void Update()
    {
        if (gameOver) return;
        if (!timerStarted) return;

        timeLeft -= Time.deltaTime;
        timerText.text = "Tiempo: " + Mathf.CeilToInt(timeLeft);

        if (timeLeft <= lowTimeThreshold)
        {
            float pulse = Mathf.Abs(Mathf.Sin(Time.time * 3f));
            timerText.color = new Color(1f, pulse * 0.3f, pulse * 0.3f);
        }
        else
        {
            timerText.color = Color.white;
        }

        if (timeLeft <= 0)
        {
            timeLeft = 0;
            TriggerGameOver("¡DESPEDIDO!");
        }
        if (timerStarted && !gameOver)
            totalTime += Time.deltaTime;
    }

    public void BoxDelivered(int points)
    {
        timerStarted = true;
        deliveredBoxes++;
        score += points;
        timeLeft = resetTime;
        UpdateUI();

        if (deliveredBoxes >= totalBoxes)
            TriggerWin();
    }

    public void TriggerGameOver(string message)
    {
        gameOver = true;
        statusText.text = message;
        timerText.text = "Tiempo: 0";
        restartButton.SetActive(true);
        Time.timeScale = 0f;
        restartButton.SetActive(true);
        restartLevelButton.SetActive(true);
    }

    public void TriggerWin()
    {
        gameOver = true;
        timerText.text = "¡Terminado!";

        // Only show best time on level 3
        if (LevelManager.instance.currentLevel == 3)
        {
            bestTime = PlayerPrefs.GetFloat("MejorTiempo", 0f);

            if (bestTime == 0f || totalTime < bestTime)
            {
                bestTime = totalTime;
                PlayerPrefs.SetFloat("MejorTiempo", bestTime);
                PlayerPrefs.Save();
                statusText.text = "¡Juego Completado! \nNuevo Mejor Tiempo: " + Mathf.RoundToInt(totalTime) + "s";
                restartButton.SetActive(true);
                Time.timeScale = 0f;
                restartButton.SetActive(true);
                restartGameButton.SetActive(true);
            }
            else
            {
                statusText.text = "¡Juego Completado! \nTiempo: " + Mathf.RoundToInt(totalTime) + "s\nMejor Tiempo: " + Mathf.RoundToInt(bestTime) + "s";
                restartButton.SetActive(true);
                Time.timeScale = 0f;
                restartButton.SetActive(true);
                restartGameButton.SetActive(true);
            }
        }
        else
        {
            statusText.text = "¡Nivel Completado! ";
            if (LevelManager.instance.currentLevel < 3)
                Invoke("LoadNextLevel", 3f);
        }
        
    }

    void LoadNextLevel()
    {
        gameOver = false;
        LevelManager.instance.NextLevel();
    }

    void UpdateUI()
    {
        if (boxCountText != null)
            boxCountText.text = "Cajas: " + deliveredBoxes + "/" + totalBoxes;
    }
    public void ResetForNewLevel()
    {
        Time.timeScale = 1f;
        restartButton.SetActive(false);
        restartLevelButton.SetActive(false);
        restartGameButton.SetActive(false);
        deliveredBoxes = 0;
        score = 0;
        timerStarted = false;
        totalTime = 0f;
        timerText.text = "¡ENTREGA UNA CAJA PARA EMPEZAR!";
        timerText.color = Color.yellow;
        levelText.text = "NIVEL " + LevelManager.instance.currentLevel;
        statusText.text = ""; 
        totalBoxes = GameObject.FindGameObjectsWithTag("Caja").Length;
        UpdateUI();
    }
}
