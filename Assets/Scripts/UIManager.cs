using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI worldText;
    public TextMeshProUGUI timeText;

    private int score = 0;
    private int lives = 3;
    private int currentWorld = 1;
    private int currentLevel = 1;
    private float timeElapsed = 0.0f;

    private void Update()
    {
        if (GameManager.activado == false)return;

        timeElapsed += Time.deltaTime;
        scoreText.text = score.ToString("D6");
        livesText.text = "x" + lives.ToString("D2");
        worldText.text = "WORLD " + currentWorld.ToString() + " - " + currentLevel.ToString();
        timeText.text = Mathf.FloorToInt(timeElapsed).ToString("D6");
    }

    public void IncreaseScore(int points)
    {
        score += points;
    }

    public void DecreaseLives()
    {
        lives--;
    }

    public void ChangeWorldAndLevel(int newWorld, int newLevel)
    {
        currentWorld = newWorld;
        currentLevel = newLevel;
    }
}
