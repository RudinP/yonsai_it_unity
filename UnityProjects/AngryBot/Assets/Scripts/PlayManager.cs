using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Start()
    {
        enemyLabel.text = string.Format("Enemy : {0}", enemyCount);
        timeLabel.text = string.Format("Time {0:N2}", limitTime);
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

            PlayerController pc = GameObject.Find("Player").GetComponent<PlayerController>();
            pc.playerState = PlayerState.Dead;
        }
    }
}
