using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFX : MonoBehaviour
{
    public Light gunLight;

    private void Update()
    {
        if(gameObject.activeSelf)
        {
            gunLight.range = Random.Range(4f, 10f);
            transform.localScale = Vector3.one * Random.Range(2f, 4f);
            transform.localEulerAngles = new Vector3(270f, 0, Random.Range(0, 90f));
        }
    }
}
