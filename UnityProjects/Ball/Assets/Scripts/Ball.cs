using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    int jump;
    int maximum;
    AudioSource jumpSound;
    void Start()
    {
        jump = 0;
        maximum = 2;
        jumpSound = GetComponent<AudioSource>();
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
            jumpSound.Play();
            Rigidbody ballRigid;
            ballRigid = gameObject.GetComponent<Rigidbody>();
            ballRigid.AddForce(Vector3.up * 300);

            jump--;
        }
    }
}
