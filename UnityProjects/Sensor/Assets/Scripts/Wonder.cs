using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wonder : MonoBehaviour
{
    Vector3 tarPos;
    float minX = -45f;
    float maxX = 45f;
    float minZ = -45f;
    float maxZ = 45f;
    float rotSpeed = 2f;
    float movementSpeed = 5f;

    private void Start()
    {
        GetNextPosition();
    }

    private void Update()
    {
        if (Vector3.Distance(tarPos, transform.position) <= 5f)
            GetNextPosition();

        Quaternion tarRot = Quaternion.LookRotation(tarPos - transform.position);
        transform.rotation = Quaternion.Slerp(
            transform.rotation, tarRot, rotSpeed * Time.deltaTime);
        transform.Translate(new Vector3(0, 0, movementSpeed * Time.deltaTime));
    }

    private void GetNextPosition()
    {
        tarPos = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
    }
}
