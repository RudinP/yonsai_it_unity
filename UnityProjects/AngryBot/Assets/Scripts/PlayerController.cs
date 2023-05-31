using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float hp;

    private Animation anim;
    public AnimationClip idleAni;
    public AnimationClip walkAni;
    public AnimationClip runAni;
    private AudioSource audioSrc;
    public AudioClip shotSound;

    public GameObject bullet;
    public Transform shotPoint;
    public GameObject shotFx;

    public Slider lifeBar;
    public float maxHp;

    private void Start()
    {
        anim = GetComponent<Animation>();
        audioSrc = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(playerState != PlayerState.Dead)
        {
            KeyboardInput();
            LookUpdate();
        }

        AnimationUpdate();
    }

    void KeyboardInput()
    {
        float xx = Input.GetAxis("Horizontal");
        float zz = Input.GetAxis("Vertical");

        if(playerState != PlayerState.Attack)
        {
            if (xx != 0 || zz != 0)
            {
                lookDirection = (xx * Vector3.right) + (zz * Vector3.forward);
                speed = walkSpeed;
                playerState = PlayerState.Walk;

                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    speed = runSpeed;
                    playerState = PlayerState.Run;
                }
            }
            else if (xx == 0 && zz == 0 && playerState != PlayerState.Idle)
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

        shotFx.SetActive(true);

        playerState = PlayerState.Attack;
        speed = 0.0f;

        yield return new WaitForSeconds(0.15f);
        shotFx.SetActive(false);

        yield return new WaitForSeconds(0.15f);
        playerState = PlayerState.Idle;
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

    public void Hurt(float damage)
    {
        if(hp > 0)
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
