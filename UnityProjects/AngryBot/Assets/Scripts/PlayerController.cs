using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Walk,
    Run,
    Attack,
    Dead,
}

public class PlayerController : MonoBehaviour
{
    public PlayerState playerState;
    public Vector3 lookDirection;
    public float speed;
    public float walkSpeed;
    public float runSpeed;

    private Animation anim;
    public AnimationClip idleAni;
    public AnimationClip walkAni;
    public AnimationClip runAni;

    private void Start()
    {
        anim = GetComponent<Animation>();
    }

    private void Update()
    {
        KeyboardInput();
        LookUpdate();

        AnimationUpdate();
    }

    void KeyboardInput()
    {
        float xx = Input.GetAxis("Horizontal");
        float zz = Input.GetAxis("Vertical");
        
        if(xx != 0 || zz != 0)
        {
            lookDirection = (xx * Vector3.right) + (zz * Vector3.forward);
            speed = walkSpeed;
            playerState = PlayerState.Walk;
            
            if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                speed = runSpeed;
                playerState = PlayerState.Run;
            }
        }
        else if(xx == 0 && zz == 0 && playerState != PlayerState.Idle)
        {
            playerState = PlayerState.Idle;
            speed = 0.0f;
        }
    }

    void LookUpdate()
    {
        Quaternion r = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, r, 600f * Time.deltaTime);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void AnimationUpdate()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                anim.CrossFade(idleAni.name, 0.2f);
                break;
            case PlayerState.Walk:
                anim.CrossFade(walkAni.name, 0.2f);
                break;
            case PlayerState.Run:
                anim.CrossFade(runAni.name, 0.2f);
                break;
            case PlayerState.Attack:
                anim.CrossFade(idleAni.name, 0.2f);
                break;
            case PlayerState.Dead:
                anim.CrossFade(idleAni.name, 0.2f);
                break;
        }
    }
}