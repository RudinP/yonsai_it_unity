using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = .5f;
    }

    // Update is called once per frame
    void Update()
    {
        //오일러각을 쓰게 되면 짐벌락 현상이 발생하기 때문에 쿼터니언을 사용해야 함.
        float xRot = Input.GetAxis("Vertical") * speed;
        float zRot = Input.GetAxis("Horizontal") * speed;
        transform.Rotate(new Vector3(xRot, 0, -zRot));


        if(Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            Debug.Log("mouse down : " + Input.mousePosition);
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
