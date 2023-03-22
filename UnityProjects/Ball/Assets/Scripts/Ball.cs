using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    float startingPoint;
    void Start()
    {
        Debug.Log("Start");
        startingPoint = transform.position.z;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody ballRigid;
            ballRigid = gameObject.GetComponent<Rigidbody>();
            ballRigid.AddForce(Vector3.up * 300);

            Debug.Log(ballRigid.mass);
        }
    }
}
