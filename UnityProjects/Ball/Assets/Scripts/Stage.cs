using UnityEngine;

public class Stage : MonoBehaviour
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


        if (Input.GetMouseButton(0))
        {
            //왼쪽을 클릭
            transform.localEulerAngles = new Vector3(
                transform.localEulerAngles.x,
                transform.localEulerAngles.y,
                transform.localEulerAngles.z + speed);
        }

        else if (Input.GetMouseButton(1))
        {
            //오른쪽을 클릭
            transform.localEulerAngles = new Vector3(
                transform.localEulerAngles.x,
                transform.localEulerAngles.y,
                transform.localEulerAngles.z - speed);
        }

    }

}

