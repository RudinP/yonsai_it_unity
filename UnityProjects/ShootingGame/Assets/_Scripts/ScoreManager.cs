using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text currentScoreUI;
    public int currentScore;

    public void AddScore()
    {
        currentScore++;
        currentScoreUI.text = "현재점수 : " + currentScore;
    }

}
