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
            currentScoreUI.text = "�������� : " + currentScore;

            if (currentScore > bestScore)
            {
                bestScore = currentScore;
                bestScoreUI.text = "�ְ����� : " + bestScore;

                PlayerPrefs.SetInt("BestScore", bestScore);
                PlayerPrefs.Save(); //�ݺ��� �ȿ��� ���� ���Ѵ�.
            }
        }
    }

    public static ScoreManager Instance = null; //�̱��� ���Ͽ����� �빮�ڷ� �صδ°� ����.
    private void Awake()
    {
        if (Instance == null) Instance = this; 
    }

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreUI.text = "�ְ����� : " + bestScore;
    }


}
