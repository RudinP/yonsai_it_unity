using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.Rotate(new Vector3(0.0f, speed * Time.deltaTime , 0.0f));
    }
}
