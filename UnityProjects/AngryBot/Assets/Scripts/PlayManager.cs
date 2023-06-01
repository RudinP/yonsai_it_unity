using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    public bool playEnd;
    public float limitTime;
    public int enemyCount;

    public Text timeLabel;
    public Text enemyLabel;

    public GameObject finalGUI;
    public Text finalMessage;
    public Text finalScoreLabel;

    public Text PlayerName;

    private void Start()
    {
        enemyLabel.text = string.Format("Enemy : {0}", enemyCount);
        timeLabel.text = string.Format("Time {0:N2}", limitTime);

        PlayerName.text = PlayerPrefs.GetString("UserName");
    }

    private void Update()
    {
        
        if(limitTime > 0)
        {
            limitTime -= Time.deltaTime;
            if (limitTime < 0)
                limitTime = 0;
            timeLabel.text = string.Format("Time {0:N2}", limitTime);
        }
        else
        {
            GameOver();
        }
    }

    private void Clear()
    {
        if(!playEnd)
        {
            Time.timeScale = 0;
            playEnd = true;
            finalMessage.text = "Clear!!";

            PlayerController pc = GameObject.Find("Player").GetComponent<PlayerController>();
            float score = 12345f + limitTime * 123f + pc.hp * 123f;
            finalScoreLabel.text = string.Format("{0:N0}", score);

            BestCheck(score);

            finalGUI.SetActive(true);
        }
    }

    public void EnemyDie()
    {
        enemyCount--;
        enemyLabel.text = string.Format("Enemy : {0}", enemyCount);

        if (enemyCount <= 0)
            Clear();
    }

    public void GameOver()
    {
        if (!playEnd)
        {
            Time.timeScale = 0;
            playEnd = true;
            finalMessage.text = "Fail";
            float score = 1234f - enemyCount * 123f;
            finalScoreLabel.text = string.Format("{0:N0}", score);
            finalGUI.SetActive(true) ;

            BestCheck(score);

            PlayerController pc = GameObject.Find("Player").GetComponent<PlayerController>();
            pc.playerState = PlayerState.Dead;
        }
    }

    public void Replay()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainPlay");
    }

    public void Quit()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Title");
    }

    private void BestCheck(float score)
    {
        float bestScore = PlayerPrefs.GetFloat("BestScore");

        if(score > bestScore)
        {
            PlayerPrefs.SetFloat("BestScore", score);
            PlayerPrefs.SetString("BestPlayer", PlayerPrefs.GetString("UserName"));
            PlayerPrefs.Save();
        }
    }
}
