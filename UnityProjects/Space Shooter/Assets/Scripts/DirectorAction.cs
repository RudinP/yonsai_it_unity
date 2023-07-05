using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class DirectorAction : MonoBehaviour
{
    PlayableDirector pd;

    public Camera targetCam;

    private void Start()
    {
        pd = GetComponent<PlayableDirector>();
        pd.Play();
    }

    private void Update()
    {
        if (pd.time >= pd.duration)
        {
            if (Camera.main == targetCam)
                targetCam.GetComponent<CinemachineBrain>().enabled = false;
            else
                targetCam.gameObject.SetActive(false);

            gameObject.SetActive(false);
        }
    }
}
