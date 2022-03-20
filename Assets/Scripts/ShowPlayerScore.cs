using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerScore : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Text>().text = ScoreTracker.GetPlayerScore().ToString();
    }
}
