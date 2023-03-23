using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    int jump;
    int maximum;
    void Start()
    {
        jump = 0;
        maximum = 2;
        Debug.Log("Start");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground") 
        { 
            jump = maximum;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            jump = maximum-1;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jump>0)
        {
            Rigidbody ballRigid;
            ballRigid = gameObject.GetComponent<Rigidbody>();
            ballRigid.AddForce(Vector3.up * 300);

            Debug.Log(ballRigid.mass);
            jump--;
        }
    }
}
