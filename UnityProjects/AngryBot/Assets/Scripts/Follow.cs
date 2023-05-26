using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject target;
    public float distance = 5.0f;
    public float height = 8.0f;
    public float speed = 2.0f;

    private Vector3 pos;

    private void Update()
    {
        pos = new Vector3(
            target.transform.position.x,
            height,
            target.transform.position.z - distance);

        gameObject.transform.position = Vector3.Lerp(
            gameObject.transform.position,
            pos,
            speed * Time.deltaTime );
    }
}
