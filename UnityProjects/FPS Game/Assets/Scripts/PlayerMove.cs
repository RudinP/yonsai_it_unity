using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7f;

    CharacterController cc;

    float gravity = -20;
    float yVelocity = 0;

    private void Start()
    {
        cc = GetComponent<CharacterController>();    
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        //바라보는 방향으로 이동
        dir = Camera.main.transform.TransformDirection(dir);

        //transform.position += dir * moveSpeed * Time.deltaTime;

        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        cc.Move(dir * moveSpeed * Time.deltaTime);
    }
}
