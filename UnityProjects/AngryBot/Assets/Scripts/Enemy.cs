using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Move,
    Attack,
    Hurt,
    Die
}

public class Enemy : MonoBehaviour
{
    public EnemyState enemyState;

    public Animator anim;
    
    private float speed;
    public float moveSpeed;
    public float attackSpeed;

    public float findRange;
    public float damage;
    public Transform player;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (enemyState == EnemyState.Idle)
            DistanceCheck();
        else if (enemyState == EnemyState.Move)
        { 
            MoveUpdate();
            AttackRangeCheck();
        }
    }
    
    private void AttackRangeCheck()
    {
        if (Vector3.Distance(player.position, transform.position) < 1.5f
            && enemyState != EnemyState.Attack)
        {
            speed = 0;
            enemyState = EnemyState.Attack;
            anim.SetTrigger("attack");
        }
    }
    private void DistanceCheck()
    {
        if (Vector3.Distance(player.position, transform.position) >= findRange)
        {
            enemyState = EnemyState.Idle;
            anim.SetBool("run", false);
            speed = 0;
        }
        else
        {
            enemyState = EnemyState.Move;
            anim.SetBool("run", true);
            speed = moveSpeed;
        }
    }

    private void MoveUpdate()
    {
        transform.rotation = Quaternion.LookRotation(
            new Vector3(player.position.x, transform.position.y, player.position.z)
            - transform.position);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
