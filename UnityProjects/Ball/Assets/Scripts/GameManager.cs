using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int coinCount = 0;
    public Text coinText;

    void GetCoin()
    {
        coinCount++;
        coinText.text = coinCount + "°³";
        Debug.Log("µ¿Àü: " +  coinCount);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
    void RedCoinStart()
    {
        DestroyObstacles();
    }

    void DestroyObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
