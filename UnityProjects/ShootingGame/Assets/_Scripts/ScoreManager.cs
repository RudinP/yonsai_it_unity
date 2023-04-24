using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    public TMP_Text currentScoreUI;
    public TMP_Text bestScoreUI;

    public int currentScore { get; private set; }
    public int bestScore { get; private set; }
    public int Score
    {
        get 
        { 
            return currentScore;
        }
        set
        {
            currentScore++;
            currentScoreUI.text = "현재점수 : " + currentScore;

            if (currentScore > bestScore)
            {
                bestScore = currentScore;
                bestScoreUI.text = "최고점수 : " + bestScore;

                PlayerPrefs.SetInt("BestScore", bestScore);
                PlayerPrefs.Save(); //반복문 안에서 쓰면 망한다.
            }
        }
    }

    public static ScoreManager Instance = null; //싱글톤 패턴에서는 대문자로 해두는게 관습.
    private void Awake()
    {
        if (Instance == null) Instance = this; 
    }

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreUI.text = "최고점수 : " + bestScore;
    }


}
