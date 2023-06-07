using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

class NameScore
{
    public string name;
    public float score;
}

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

    public Text playerName;

    private NameScore[] rankArr;

    private void Start()
    {
        enemyLabel.text = string.Format("Enemy : {0}", enemyCount);
        timeLabel.text = string.Format("Time : {0:N2}", limitTime);

        playerName.text = PlayerPrefs.GetString("UserName");

        LoadRank();
    }

    private void Update()
    {
        if (limitTime > 0)
        {
            limitTime -= Time.deltaTime;
            if (limitTime < 0)
                limitTime = 0;
            timeLabel.text = string.Format("Time : {0:N2}", limitTime);
        }
        else
        {
            GameOver();
        }
    }

    void LoadRank()
    {
        rankArr = new NameScore[4];
        for (int i = 0; i < rankArr.Length - 1; i++)
        {
            rankArr[i] = new NameScore();
            rankArr[i].name = PlayerPrefs.GetString("BestPlayer" + (i + 1));
            rankArr[i].score = PlayerPrefs.GetFloat("BestScore" + (i + 1));
        }
    }

    public void Clear()
    {
        if (!playEnd)
        {
            Time.timeScale = 0;
            playEnd = true;
            finalMessage.text = "Clear!!";

            PlayerController pc = GameObject.Find("Player").GetComponent<PlayerController>();
            float score = 12345f + limitTime * 123f + pc.hp * 123f;
            finalScoreLabel.text = string.Format("{0:N0}", score);
            finalGUI.SetActive(true);

            BestCheck(score);
        }
    }

    public void EnemyDie()
    {
        enemyCount--;
        enemyLabel.text = string.Format("Enemy : {0}", enemyCount);

        limitTime += 5;

        if (enemyCount <= 0)
            Clear();
    }

    public void GameOver()
    {
        if (!playEnd)
        {
            Time.timeScale = 0;
            playEnd = true;
            finalMessage.text = "Fail...";
            float score = 1234f - enemyCount * 123f;
            finalScoreLabel.text = string.Format("{0:N0}", score);
            finalGUI.SetActive(true);

            BestCheck(score);

            PlayerController pc = GameObject.Find("Player").GetComponent<PlayerController>();
            pc.playerState = PlayerState.Dead;
        }
    }

    public void Replay()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    private void BestCheck(float score)
    {
        NameScore now = new NameScore();
        now.name = PlayerPrefs.GetString("UserName");
        now.score = score;

        rankArr[rankArr.Length - 1] = now;
        rankArr = rankArr.OrderByDescending(v => v.score).ToArray();

        for (int i = 0; i < rankArr.Length - 1; i++)
        {
            PlayerPrefs.SetString("BestPlayer" + (i + 1), rankArr[i].name);
            PlayerPrefs.SetFloat("BestScore" + (i + 1), rankArr[i].score);
        }
        PlayerPrefs.Save();
    }
}
