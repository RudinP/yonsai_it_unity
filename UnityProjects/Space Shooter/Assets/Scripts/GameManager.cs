using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Fusion;

public class GameManager : NetworkBehaviour
{
    public static GameManager gm;
    private void Awake()
    {
        if (gm == null)
            gm = this;
    }

    public enum GameState
    {
        Ready,
        Run,
        Pause,
        GameOver
    }

    [Networked] public GameState gState { get; set; }

    public GameObject gameLabel;
    Text gameText;

    public PlayerMove player;

    public GameObject gameOption;

    public Text killTxt;

    public int killCount;

    public GameObject hpAlert;

    bool isAlertOn;

    public Slider hpSlider;
    public GameObject hitEffect;

    public GameObject bulletEffect;
    public Text wModeText;

    public GameObject weapon01;
    public GameObject weapon02;
    public GameObject crosshair01;
    public GameObject crosshair02;
    public GameObject weapon01_R;
    public GameObject weapon02_R;
    public GameObject crosshair02_zoom;

    public List<GameObject> players;

    public override void Spawned()
    {
        Application.targetFrameRate = 60;

        gState = GameState.Ready;

        gameText = gameLabel.GetComponent<Text>();
        gameText.text = "Ready...";
        gameText.color = new Color32(255, 185, 0, 255);

        StartCoroutine(ReadyToStart());
    }

    public override void FixedUpdateNetwork()
    {
        if(!isAlertOn)
            if ((float)player.hp / player.maxHp * 100 <= 15)
            {
                Debug.Log($"{player.hp} / {player.maxHp}");
                StartCoroutine(HpAlert());
                isAlertOn = true;
            }

        if (player != null && player.hp <= 0)
        {
            StopCoroutine(HpAlert());
            player.GetComponentInChildren<Animator>().SetFloat("MoveMotion", 0);

            gameLabel.SetActive(true);
            gameText.text = "Game Over";
            gameText.color = new Color32(255, 0, 0, 255);

            Transform buttons = gameText.transform.GetChild(0);
            buttons.gameObject.SetActive(true);

            gState = GameState.GameOver;
        }
        killTxt.text = "Kill: " + killCount;
    }

    IEnumerator ReadyToStart()
    {
        yield return new WaitForSeconds(2f);
        gameText.text = "Go!";
        yield return new WaitForSeconds(0.5f);
        gameLabel.SetActive(false);

        gState = GameState.Run;
    }

    IEnumerator HpAlert()
    {
        hpAlert.SetActive(true);
        yield return new WaitForSeconds(1f);
        hpAlert.SetActive(false);
        yield return new WaitForSeconds(1f);
        StartCoroutine(HpAlert());
    }

    public void OpenOptionWindow()
    {
        gameOption.SetActive(true);
        Time.timeScale = 0;
        gState = GameState.Pause;
    }

    public void CloseOptionWindow()
    {
        gameOption.SetActive(false);
        Time.timeScale = 1;
        gState = GameState.Run;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void AddPlayer(GameObject obj)
    {
        players.Add(obj);
    }

    public void RemovePlayer(GameObject obj)
    {
        players.Remove(obj);
    }
}
