using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public InputField nameInput;
    public GameObject bestData;
    public Text bestUserData;

    public void GoPlay()
    {
        if (string.IsNullOrEmpty(nameInput.text))
            return;

        PlayerPrefs.SetString("UserName", nameInput.text);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainPlay");
    }

    public void BestScore()
    {
        NameScore[] rank = LoadRank();
        bestUserData.text = "";
        for (int i = 0; i < rank.Length; i++)
        {
            bestUserData.text += string.Format(
                "{0}. {1}:{2:N0}\n",
                i + 1,
                PlayerPrefs.GetString("BestPlayer" + (i + 1)),
                PlayerPrefs.GetFloat("BestScore" + (i + 1)));
        }
        if (PlayerPrefs.HasKey("BestPlayer1"))
            bestData.SetActive(true);
    }

    NameScore[] LoadRank()
    {
        NameScore[] rankArr = new NameScore[3];
        for (int i = 0; i < rankArr.Length; i++)
        {
            rankArr[i] = new NameScore();
            rankArr[i].name = PlayerPrefs.GetString("BestPlayer" + (i + 1));
            rankArr[i].score = PlayerPrefs.GetFloat("BestScore" + (i + 1));
        }
        return rankArr;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
