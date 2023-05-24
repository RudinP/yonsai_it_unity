using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public Transform target;
    public GameObject cursor;
    public PlayerController playerCtrl;

    private void Update()
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            cursor.transform.position = new Vector3(hit.point.x, 1.0f, hit.point.z);
        }
    }
}