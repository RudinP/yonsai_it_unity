using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        float zRotation = transform.localEulerAngles.z;
        zRotation = zRotation - Input.GetAxis("Horizontal");
        transform.localEulerAngles = new Vector3(10, 0, zRotation);

        if(Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            Debug.Log("mouse donw : " + Input.mousePosition);
            if(Input.mousePosition.x < Screen.width / 2)
            {
                //왼쪽을 클릭
                transform.localEulerAngles = new Vector3(10, 0, transform.localEulerAngles.z + speed);
            }
            else
            {
                //오른쪽을 클릭
                transform.localEulerAngles = new Vector3(10, 0, transform.localEulerAngles.z - speed);
            }
        }
    }
}
