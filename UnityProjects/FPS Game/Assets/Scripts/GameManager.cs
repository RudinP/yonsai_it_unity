using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
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

    public GameState gState;

    public GameObject gameLabel;
    Text gameText;

    PlayerMove player;

    public GameObject gameOption;

    private void Start()
    {
        gState = GameState.Ready;

        gameText = gameLabel.GetComponent<Text>();
        gameText.text = "Ready...";
        gameText.color = new Color32(255, 185, 0, 255);

        StartCoroutine(ReadyToStart());

        player = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    private void Update()
    {
        if (player.hp <= 0)
        {
            player.GetComponentInChildren<Animator>().SetFloat("MoveMotion", 0);

            gameLabel.SetActive(true);
            gameText.text = "Game Over";
            gameText.color = new Color32(255, 0, 0, 255);

            Transform buttons = gameText.transform.GetChild(0);
            buttons.gameObject.SetActive(true);

            gState = GameState.GameOver;
        }
    }

    IEnumerator ReadyToStart()
    {
        yield return new WaitForSeconds(2f);
        gameText.text = "Go!";
        yield return new WaitForSeconds(0.5f);
        gameLabel.SetActive(false);

        gState = GameState.Run;
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
}
