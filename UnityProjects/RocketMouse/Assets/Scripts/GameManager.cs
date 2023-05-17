using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject mouse;
    public Text levelTxt;
    public GameObject feverTxt;
    public AudioSource music;

    public int level { get; private set; }
    public float speedPerLv;
    public float feverSpeed;

    public bool isFever { get; private set; } = false;
    private void Start()
    {
        instance = this;
        level = 1;
        SetVolume(PlayerPrefs.GetFloat("Volume"));

        StartCoroutine("LevelUp");
        StartCoroutine("Fever");
    }

    private void SetVolume(float vol)
    {
        music.volume = vol;

        List<AudioSource> musics = mouse.GetComponents<AudioSource>().ToList();
        musics.ForEach(a => a.volume = vol);
    }

    IEnumerator LevelUp()
    {
        while (!mouse.GetComponent<MouseController>().dead)
        {
            levelTxt.text = "Lv." + level;
            yield return new WaitForSecondsRealtime(20f);
            level++;
            mouse.GetComponent<MouseController>().forwardMovementSpeed += speedPerLv;
        }
    }

    IEnumerator Fever()
    {
        while (!mouse.GetComponent<MouseController>().dead)
        {
            yield return new WaitForSecondsRealtime(60f);
            isFever = true;
            feverTxt.SetActive(isFever);
            mouse.GetComponent<MouseController>().forwardMovementSpeed += feverSpeed;

            yield return new WaitForSecondsRealtime(60f);
            isFever = false;
            feverTxt.SetActive(isFever);
            mouse.GetComponent<MouseController>().forwardMovementSpeed -= feverSpeed;
        }

    }

}
