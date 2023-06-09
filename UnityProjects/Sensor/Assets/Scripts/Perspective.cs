using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perspective : Sense
{
    public float fieldOfView = 45f;
    public float viewDistance = 20f;

    Transform enemyTrans;
    Vector3 rayDirection;

    protected override void Initialise()
    {
        enemyTrans = GameObject.FindGameObjectWithTag("Enemy").transform;
    }

    protected override void UpdateSense()
    {
        DetectAspect();
    }

    void DetectAspect()
    {
        RaycastHit hit;
        rayDirection = enemyTrans.position - transform.position;

        if(Vector3.Angle(rayDirection, transform.forward) < fieldOfView)
        {
            if(Physics.Raycast(transform.position, rayDirection, out hit, viewDistance)) 
            {
                Aspect aspect = hit.collider.GetComponent<Aspect>();
                if(aspect != null)
                {
                    if(aspect.aspectName == aspectName) 
                    {
                        Debug.Log("Enemy Detected");
                    }
                }
            }
        }
    }
}
