using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesImageDisplay;
    public GameObject titleScreen;
    public GameObject pauseMenu;

    public Text scoreText, bestScoreText;
    public int score, bestScore;

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("Highscore", 0 );
        bestScoreText.text = "Best: " + bestScore;
    }

    public void UpdateLives(int currentLives)
    {
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;

        scoreText.text = "Score: " + score;
    }

    public void CheckForBestScore()
    {
        //if current score grater than best score
        if (score > bestScore)
        {
            //best score = currentscore
            bestScore = score;
            PlayerPrefs.SetInt("Highscore", bestScore);
            bestScoreText.text = "Best: " + bestScore;
        }
    }

    public void ShowTitleScreen()
    {
        CheckForBestScore();
        titleScreen.SetActive(true);
    }

    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        scoreText.text = "Score: 0";
    }
}

