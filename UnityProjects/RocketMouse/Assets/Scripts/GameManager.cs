using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public GameObject mouse;
    public Text levelTxt;
    public AudioSource music;

    public int level { get; private set; }
    public float speedPerLv;

    private void Start()
    {
        instance = this;
        level = 1;

        SetVolume(PlayerPrefs.GetFloat("Volume"));

        StartCoroutine("LevelUp");
    }
    private void Update()
    {
        levelTxt.text = "Lv." + level;        
    }

    private void SetVolume(float vol)
    {
        music.volume = vol;

        List<AudioSource> musics = mouse.GetComponents<AudioSource>().ToList();
        musics.ForEach(a => a.volume = vol);
    }

    IEnumerator LevelUp()
    {
        yield return new WaitForSecondsRealtime(20f);
        level++;
        mouse.GetComponent<MouseController>().forwardMovementSpeed += speedPerLv;
    }
}
