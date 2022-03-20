using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class ScoreTracker : MonoBehaviour
{
    public static ScoreTracker instance { get; private set; }

    public int[] bestScoreValue = new int[9];
    public string[] bestScoreName = new string[9];

    public int score = 0;
    public int bestScore = 0;
    public string playerName = "";

    private int difficulty = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        LoadScoreData();
        DontDestroyOnLoad(gameObject);
    }

    public static int GetDifficulty()
    {
        return instance.difficulty;
    }

    public static int GetPlayerBestScore()
    {
        return instance.bestScore;
    }

    public static int[] GetBestScoreValues()
    {
        return instance.bestScoreValue;
    }

    public static string[] GetBestScoreNames()
    {
        return instance.bestScoreName;
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int playerBestScore;
        public int[] score;
        public string[] name;
    }

    public static void SaveScoreData()
    {
        SaveData data = new SaveData();
        data.score = instance.bestScoreValue;
        data.name = instance.bestScoreName;
        data.playerName = instance.playerName;
        data.playerBestScore = instance.bestScore;

        PlayerPrefs.SetString("PlayerName", data.playerName);
        PlayerPrefs.SetInt("PlayerBestScore", data.playerBestScore);
        PlayerPrefs.SetInt("BestScoreSize", data.score.Length);
        for (int i = 0; i < data.score.Length; i++)
        {
            PlayerPrefs.SetString($"BestScoreName{i}", data.name[i]);
            PlayerPrefs.SetInt($"BestScore{i}", data.score[i]);
        }

        PlayerPrefs.Save();
    }

    public static void LoadScoreData()
    {
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            SaveData data = new SaveData();
            data.playerBestScore = PlayerPrefs.GetInt("PlayerBestScore");
            data.playerName = PlayerPrefs.GetString("PlayerName");
            int size = PlayerPrefs.GetInt("BestScoreSize");
            data.score = new int[size];
            data.name = new string[size];
            for (int i = 0; i < size; i++)
            {
                data.score[i] = PlayerPrefs.GetInt($"BestScore{i}");
                data.name[i] = PlayerPrefs.GetString($"BestScoreName{i}");
            }

            instance.bestScoreValue = data.score;
            instance.bestScoreName = data.name;
            instance.playerName = data.playerName;
            instance.bestScore = data.playerBestScore;
        }
        else
        {
            for (int i = 0; i < instance.bestScoreValue.Length; i++)
            {
                instance.bestScoreValue[i] = 60 - i * 5;
                instance.bestScoreName[i] = $"IAM#{i + 1}!";
            }
        }
    }

    public static void SetPlayerScore(int score)
    {
        instance.score = score;
    }
    public static void SetPlayerName(string name)
    {
        instance.playerName = name;
    }

    public void LoadBestScoreScene()
    {
        SceneManager.LoadScene("bestscore");
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("menu");
    }

    public void LoadGameScene(int dif = 0)
    {
        difficulty = dif;
        SceneManager.LoadScene("main");
    }

    public static void GoToBestScoreScreen()
    {
        instance.LoadBestScoreScene();
    }

    public static void GoToMenuScreen()
    {
        instance.LoadMenuScene();
    }

    public static void GoToGameScreen(int dif = 0)
    {
        instance.LoadGameScene(dif);
    }


    public static string GetPlayerName()
    {
        return instance.playerName;
    }

    public static int GetPlayerScore()
    {
        return instance.score;
    }

    public static void CalculateNewBestScores()
    {
        instance.CalculateScores();
    }
    void CalculateScores()
    {
        if (score > bestScore)
        {
            bestScore = score;
        }

        for (int i = 0; i < bestScoreValue.Length; i++)
        {
            if (score > bestScoreValue[i])
            {
                for (int t = bestScoreValue.Length - 1; t > i; t--)
                {
                    bestScoreValue[t] = bestScoreValue[t - 1];
                    bestScoreName[t] = bestScoreName[t - 1];
                }
                bestScoreValue[i] = score;
                bestScoreName[i] = playerName;
                break;
            }
        }

        SaveScoreData();
    }
}
