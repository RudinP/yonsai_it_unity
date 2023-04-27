using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    Vector3 dir;

    private void Update()
    {
        
        if (this.gameObject.name.Contains("Enemy"))
            dir = Vector3.down;
        else
            dir = Vector3.up;

        transform.Translate(dir * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.gameObject.tag == "Enemy" && collision.gameObject.tag == "Player")
            Destroy(collision.gameObject);
    }
}
