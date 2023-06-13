using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    public NavMeshAgent playerNavAgent;

    private void Start()
    {
        playerNavAgent = GameObject.FindWithTag("Player").GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                Vector3 targetPosition = hitInfo.point;
                playerNavAgent.destination = targetPosition;
                transform.position = targetPosition;
            }
        }
    }
}
