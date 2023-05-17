using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    public AudioSource music;

    private void Start()
    {
        PlayerPrefs.SetFloat("Volume", 1f);
    }
    public void SetPlayerPrefs()
    {
        PlayerPrefs.SetFloat("Volume", music.volume);
        PlayerPrefs.Save();
    }
}
