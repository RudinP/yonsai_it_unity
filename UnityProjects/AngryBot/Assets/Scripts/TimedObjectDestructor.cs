using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedObjectDestructor : MonoBehaviour
{
    public float timeOut;

    private void Awake()
    {
        Invoke("DestroyNow", timeOut);
    }

    private void DestroyNow()
    {
        Destroy(gameObject);
    }
}
