using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState
{
    Idle,
    Walk,
    LeftWalk,
    RightWalk,
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
    public Vector3 dest;
    public bool goDest;

    private Animation anim;
    public AnimationClip idleAni;
    public AnimationClip walkAni;
    public AnimationClip leftWalkAni;
    public AnimationClip rightWalkAni;
    public AnimationClip runAni;

    public GameObject bullet;
    public Transform shotPoint;
    public GameObject shotFx;
    private AudioSource audioSrc;
    public AudioClip shotSound;

    public Slider lifeBar;
    public float maxHp;
    public float hp;

    private void Start()
    {
        anim = GetComponent<Animation>();
        audioSrc = GetComponent<AudioSource>();
        goDest = false;
    }

    private void Update()
    {
        if (playerState != PlayerState.Dead)
        {
            KeyboardInput();
            LookUpdate(false);
        }

        AnimationUpdate();
    }

    void KeyboardInput()
    {
        float xx = Input.GetAxisRaw("Horizontal");
        float zz = Input.GetAxisRaw("Vertical");

        if (playerState != PlayerState.Attack)
        {
            if (xx != 0 || zz != 0)
            {
                lookDirection = (xx * Vector3.right) + (zz * Vector3.forward);
                speed = walkSpeed;
                playerState = PlayerState.Walk;
                anim[walkAni.name].speed = 1;

                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    speed = runSpeed;
                    playerState = PlayerState.Run;
                    anim[runAni.name].speed = 2;
                }
                goDest = false;
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                speed = walkSpeed;
                playerState = PlayerState.LeftWalk;
                goDest = false;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                speed = walkSpeed;
                playerState = PlayerState.RightWalk;
                goDest = false;
            }
            else if (goDest)
            {
                lookDirection = dest - transform.position;
                lookDirection.y = 0;

                if (Vector3.Distance(transform.position, dest) < 0.2f)
                {
                    goDest = false;
                    speed = 0.0f;
                    playerState = PlayerState.Idle;
                }
            }
            else if (playerState != PlayerState.Idle)
            {
                playerState = PlayerState.Idle;
                speed = 0.0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && playerState != PlayerState.Dead)
            StartCoroutine(nameof(Shot));
    }

    public IEnumerator Shot()
    {
        GameObject bulletObj = Instantiate(
            bullet,
            shotPoint.position,
            Quaternion.LookRotation(shotPoint.forward));

        Physics.IgnoreCollision(
            bulletObj.GetComponent<Collider>(),
            GetComponent<Collider>());

        audioSrc.clip = shotSound;
        audioSrc.Play();

        playerState = PlayerState.Attack;
        speed = 0;

        shotFx.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        shotFx.SetActive(false);

        yield return new WaitForSeconds(0.15f);
        playerState = PlayerState.Idle;
    }

    public void LookUpdate(bool rightNow)
    {
        if (rightNow)
        {
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
        else
        {
            Quaternion r = Quaternion.LookRotation(lookDirection);
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, r, 600f * Time.deltaTime);
        }

        if (playerState == PlayerState.LeftWalk)
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        else if (playerState == PlayerState.RightWalk)
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        else
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
            case PlayerState.LeftWalk:
                anim.CrossFade(leftWalkAni.name, 0.2f);
                break;
            case PlayerState.RightWalk:
                anim.CrossFade(rightWalkAni.name, 0.2f);
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

    public void Hurt(float damage)
    {
        if (hp > 0)
        {
            hp -= damage;
            lifeBar.value = hp / maxHp;
        }

        if (hp <= 0)
        {
            speed = 0;
            playerState = PlayerState.Dead;

            PlayManager pm = GameObject.Find("PlayManager").GetComponent<PlayManager>();
            pm.GameOver();
        }
    }
}
