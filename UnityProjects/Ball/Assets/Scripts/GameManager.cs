using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int coinCount = 0;
    public Text coinText;
    public Text clearText;

    bool r;
    public void GetCoin()
    {
        coinCount++;
        coinText.text = coinCount + "°³";
        Debug.Log("µ¿Àü: " + coinCount);
    }
    public void RestartGame(string t)
    {
        t += "\n Press R to Restart";
        clearText.text = t;

        r = true;

        GameObject.Find("Main Camera").GetComponent<Camerawork>().ball = gameObject;
        Destroy(GameObject.Find("Ball"));
        Destroy(GameObject.Find("Stage"));
        Destroy(GameObject.Find("FailZone"));
        Destroy(GameObject.Find("ClearZone"));

    }

    void Restart()
    {
        SceneManager.LoadScene("Game");
    }
    public void RedCoinStart()
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
        if (r = true && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

    }
}
