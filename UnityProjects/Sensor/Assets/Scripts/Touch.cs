using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : Sense
{
    bool isDetected;

    protected override void Initialise()
    {
        isDetected = false;
    }

    protected override void UpdateSense()
    {
        if (isDetected)
            Debug.Log("Enemy Touch Detected");
    }

    private void OnTriggerEnter(Collider other)
    {
        Aspect aspect = other.GetComponent<Aspect>();
        if (aspect != null)
        {
            if (aspect.aspectName == aspectName)
                isDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Aspect aspect = other.GetComponent<Aspect>();
        if (aspect != null)
        {
            if (aspect.aspectName == aspectName)
                isDetected = false;
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        Aspect aspect = other.GetComponent<Aspect>();
        if (aspect != null)
        {
            if (aspect.aspectName == aspectName)
            {
                Debug.Log("Enemy Touch Detected");
            }
        }
    }
    */
}
