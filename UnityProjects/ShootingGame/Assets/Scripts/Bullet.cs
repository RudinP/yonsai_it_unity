using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        Vector3 dir = Vector3.up;

        transform.Translate(dir * Time.deltaTime);
    }
}
