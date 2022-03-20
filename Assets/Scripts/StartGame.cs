using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public void StartGamePlay(int dif = 0)
    {
        ScoreTracker.GoToGameScreen(dif);
    }
}
