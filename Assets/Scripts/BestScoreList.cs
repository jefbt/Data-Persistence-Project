using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScoreList : MonoBehaviour
{
    public GameObject bestScorePanel;
    public GameObject infoPanel;

    public Text playerText;
    public Text playerBestScoreText;
    public Text highscoreTextNames;
    public Text highscoreTextScores;

    public void ShowBestScore()
    {
        ScoreTracker.CalculateNewBestScores();

        infoPanel.SetActive(false);
        bestScorePanel.SetActive(true);

        int score = ScoreTracker.GetPlayerScore();
        string name = ScoreTracker.GetPlayerName();

        playerText.text = $"{name} : {score}";
        highscoreTextNames.text = "";
        highscoreTextScores.text = "";

        playerBestScoreText.text = "YOUR BEST SCORE: " + ScoreTracker.GetPlayerBestScore();

        int[] scores = ScoreTracker.GetBestScoreValues();
        string[] names = ScoreTracker.GetBestScoreNames();

        for (int i = 0; i < names.Length; i++)
        {
            if (i == 0)
            {
                highscoreTextNames.text += $"1st - {names[i]} :\n";
            }
            else if (i == 1)
            {
                highscoreTextNames.text += $"2nd - {names[i]} :\n";
            }
            else if (i == 2)
            {
                highscoreTextNames.text += $"3rd - {names[i]} :\n";
            }
            else
            {
                highscoreTextNames.text += $"{i+1}th - {names[i]} :\n";
            }
        }

        for (int i = 0; i < scores.Length; i++)
        {
            highscoreTextScores.text += scores[i].ToString() + "\n";
        }
    }

    public void GoToMenu()
    {
        ScoreTracker.GoToMenuScreen();
    }
}
