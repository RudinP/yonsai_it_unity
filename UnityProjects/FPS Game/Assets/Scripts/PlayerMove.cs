using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7f;

    CharacterController cc;

    float gravity = -20;
    public float yVelocity = 0;

    public float jumpPower = 10f;
    public bool isJumping = false;

    public int hp = 20;
    public int maxHp = 20;

    public Slider hpSlider;

    public GameObject hit;

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
        //dir.Normalize();

        dir = Camera.main.transform.TransformDirection(dir);

        if (isJumping && cc.collisionFlags == CollisionFlags.Below)
        {
            isJumping = false;
            yVelocity = 0;
        }

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            yVelocity = jumpPower;
            isJumping = true;
        }

        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        cc.Move(dir * moveSpeed * Time.deltaTime);

        hpSlider.value = (float)hp / maxHp;
    }

    public void DamageAction(int damage)
    {
        hp -= damage;
        
        if(hp > 0)
        {
            StartCoroutine(PlayHitEffect());
        }
    }

    IEnumerator PlayHitEffect()
    {
        hit.SetActive(true);

        yield return new WaitForSeconds(.3f);

        hit.SetActive(false);
    }
}

