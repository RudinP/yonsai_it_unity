using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject player;


    private void Awake()
    {
#if UNITY_ANDROID
        GameObject.Find("Joystick canvas XYBZ").SetActive(true);
#elif UNITY_EDITOR || UNITY_STANDALONE
        Screen.SetResolution(640, 960, false);
        GameObject.Find("Joystick canvas XYBZ").SetActive(false);
#endif
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
