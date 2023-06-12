using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankAi : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public NavMeshAgent navMeshAgent;

    private GameObject player;
    private Animator animator;
    private Ray ray;
    private RaycastHit hit;
    private float maxDistanceToCheck = 6.0f;
    private float currentDistance;
    private Vector3 checkDirection;
    private int currentTarget;
    private float distanceFromTarget;
    private Transform[] waypoints = null;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        pointA = GameObject.Find("P1").transform;
        pointB = GameObject.Find("P2").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        waypoints = new Transform[2] { pointA, pointB};
        currentTarget = 0;
        navMeshAgent.SetDestination(waypoints[currentTarget].position);

    }

    private void FixedUpdate()
    {
        currentDistance = Vector3.Distance(player.transform.position, transform.position);
        animator.SetFloat("distanceFromPlayer", currentDistance);

        checkDirection = player.transform.position - transform.position;
        ray = new Ray(transform.position, checkDirection);
        if(Physics.Raycast(ray, out hit, maxDistanceToCheck))
        {
            if (hit.collider.gameObject == player)
                animator.SetBool("isPlayerVisible", true);
            else
                animator.SetBool("isPlayerVisible", false);
        }
        else
        {
            animator.SetBool("isPlayerVisible", false);
        }

        distanceFromTarget = Vector3.Distance(waypoints[currentTarget].position, transform.position);
        animator.SetFloat("distanceFromWayPoint", distanceFromTarget);
    }
}
