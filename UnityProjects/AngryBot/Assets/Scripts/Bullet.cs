using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float power;
    public float life;

    private void Start()
    {
        Destroy(gameObject, life);
    }
    private void Update()
    {
        /*life -= Time.deltaTime;
        if(life <= 0.0f)
        {
            Destroy(gameObject);
        }
        */
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
