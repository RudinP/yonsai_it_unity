using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject player;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        if (player == null) player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (player == null)
        {
            StartCoroutine(Restart());
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Shooting");
    }
}
