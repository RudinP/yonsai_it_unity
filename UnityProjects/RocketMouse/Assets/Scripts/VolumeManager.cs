using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public AudioSource music;
    public Slider volumeSdr;
    public Toggle soundTgl;
    private void Update()
    {
        volumeSdr.value = PlayerPrefs.GetFloat("Volume");
    }

    public void SetPlayerPrefs()
    {
        PlayerPrefs.SetFloat("Volume", music.volume);
        PlayerPrefs.Save();
    }

    public void SetSoundTgl()
    {
        if(volumeSdr.value > 0)
        {
            soundTgl.isOn = false;
        }
        else
        {
            soundTgl.isOn = true;
        }
    }

}
