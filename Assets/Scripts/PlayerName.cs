using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    public InputField nameText;

    private void Awake()
    {
        if (ScoreTracker.GetPlayerName() != "")
        {
            nameText.text = ScoreTracker.GetPlayerName();
        }
    }

    public void SetPlayerName(Text nameText)
    {
        if (nameText.text != "")
        {
            ScoreTracker.SetPlayerName(nameText.text);
        }
        else
        {
            ScoreTracker.SetPlayerName("guestplayer");
        }

        FindObjectOfType<BestScoreList>().ShowBestScore();
    }
}
