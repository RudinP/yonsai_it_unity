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

        if (Vector3.Angle(rayDirection, transform.forward) < fieldOfView)
        {
            if (Physics.Raycast(transform.position, rayDirection, out hit, viewDistance))
            {
                Aspect aspect = hit.collider.GetComponent<Aspect>();
                if (aspect != null)
                {
                    if (aspect.aspectName == aspectName)
                        Debug.Log("Enemy Detected");
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (enemyTrans == null)
            return;

        Debug.DrawLine(transform.position, enemyTrans.position, Color.red);

        Vector3 frontRayPoint = transform.position + (transform.forward * viewDistance);
        Vector3 dirRight = transform.forward + transform.right;
        Vector3 dirLeft = transform.forward - transform.right;

        dirRight.Normalize();
        dirLeft.Normalize();
        //Vector3 newDirRight = dirRight.normalized;

        Vector3 rightRayPoint = transform.position + dirRight * viewDistance;
        Vector3 leftRayPoint = transform.position + dirLeft * viewDistance;

        Debug.DrawLine(transform.position, frontRayPoint, Color.green);
        Debug.DrawLine(transform.position, rightRayPoint, Color.green);
        Debug.DrawLine(transform.position, leftRayPoint, Color.green);
    }
}
