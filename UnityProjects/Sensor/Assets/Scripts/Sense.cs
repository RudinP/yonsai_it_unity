using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sense : MonoBehaviour
{
    public bool bDebug = true;
    public Aspect.AspectName aspectName = Aspect.AspectName.Enemy;
    public float detectionRate = 1.0f;

    protected float elapsedTime;
    protected virtual void Initialise() { }
    protected virtual void UpdateSense() { }

    private void Start()
    {
        elapsedTime = 0;
        Initialise();
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime > detectionRate)
        {
            UpdateSense();
            elapsedTime = 0f;
        }
    }
}
