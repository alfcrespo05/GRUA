using UnityEngine;
using System.Collections;
public class LevelManager : MonoBehaviour
{
    public Transform targetZone;

    public static LevelManager instance;

    [Header("Level Settings")]
    public int currentLevel = 1;

    [Header("Level 1")]
    public float level1Time = 40f;
    public float level1Wind = 3f;
    public int level1Boxes = 3;

    [Header("Level 2")]
    public float level2Time = 30f;
    public float level2Wind = 6f;
    public int level2Boxes = 5;

    [Header("Level 3")]
    public float level3Time = 20f;
    public float level3Wind = 10f;
    public int level3Boxes = 8;

    [Header("Target Zone Positions")]
    public Vector3 level1Position = new Vector3(5, 0, 5);
    public Vector3 level2Position = new Vector3(-8, 0, -6);
    public Vector3 level3Position = new Vector3(0, 0, 8);

    [Header("Pad References")]
    public Transform jumpPad;
    public Transform descendPad;
    public Transform launcherPad;

    [Header("Level 1 Pad Positions")]
    public Vector3 level1JumpPadPos = new Vector3(3, 0, 3);
    public Vector3 level1DescendPadPos = new Vector3(3, 2, 3);
    public Vector3 level1LauncherPadPos = new Vector3(-3, 0, -3);

    [Header("Level 2 Pad Positions")]
    public Vector3 level2JumpPadPos = new Vector3(5, 0, 5);
    public Vector3 level2DescendPadPos = new Vector3(5, 2, 5);
    public Vector3 level2LauncherPadPos = new Vector3(-5, 0, -5);

    [Header("Level 3 Pad Positions")]
    public Vector3 level3JumpPadPos = new Vector3(-4, 0, 4);
    public Vector3 level3DescendPadPos = new Vector3(-4, 2, 4);
    public Vector3 level3LauncherPadPos = new Vector3(4, 0, -4);

    private WindSystem windSystem;
    private GameManager gameManager;
    private BoxSpawner boxSpawner;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        windSystem = FindObjectOfType<WindSystem>();
        gameManager = FindObjectOfType<GameManager>();
        boxSpawner = FindObjectOfType<BoxSpawner>();
        gameManager.totalTime = 0f;
        LoadLevel(currentLevel);
    }

    public void LoadLevel(int level)
    {
        currentLevel = level;

        switch (level)
        {
            case 1:
                ApplyLevel(level1Time, level1Wind, level1Boxes, level1Position, false,
                    level1JumpPadPos, level1DescendPadPos, level1LauncherPadPos);
                break;
            case 2:
                ApplyLevel(level2Time, level2Wind, level2Boxes, level2Position, false,
                    level2JumpPadPos, level2DescendPadPos, level2LauncherPadPos);
                break;
            case 3:
                ApplyLevel(level3Time, level3Wind, level3Boxes, level3Position, true,
                    level3JumpPadPos, level3DescendPadPos, level3LauncherPadPos);
                break;
        }
    }

    void ApplyLevel(float time, float wind, int boxes, Vector3 zonePosition, bool moving,
    Vector3 jumpPos, Vector3 descendPos, Vector3 launcherPos)
    {
        gameManager.timeLeft = time;
        gameManager.resetTime = time;
        windSystem.windStrength = wind;
        boxSpawner.boxCount = boxes;

        targetZone.GetComponent<TargetZone>().SetStartPosition(zonePosition);
        targetZone.GetComponent<TargetZone>().isMoving = moving;

        // Move pads
        if (jumpPad != null) jumpPad.position = jumpPos;
        if (descendPad != null) descendPad.position = descendPos;
        if (launcherPad != null) launcherPad.position = launcherPos;

        boxSpawner.RespawnAll();
        StartCoroutine(ResetAfterSpawn());
    }

    IEnumerator ResetAfterSpawn()
    {
        yield return null; // wait one frame
        gameManager.ResetForNewLevel();
    }

    public void NextLevel()
    {
        if (currentLevel < 3)
            LoadLevel(currentLevel + 1);
        else
            gameManager.TriggerWin(); // completed all levels!
    }
}
