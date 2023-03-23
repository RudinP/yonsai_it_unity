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
        //���Ϸ����� ���� �Ǹ� ������ ������ �߻��ϱ� ������ ���ʹϾ��� ����ؾ� ��.
        float xRot = Input.GetAxis("Vertical") * speed;
        float zRot = Input.GetAxis("Horizontal") * speed;
        transform.Rotate(new Vector3(xRot, 0, -zRot));


        if (Input.GetMouseButton(0))
        {
            //������ Ŭ��
            transform.localEulerAngles = new Vector3(
                transform.localEulerAngles.x,
                transform.localEulerAngles.y,
                transform.localEulerAngles.z + speed);
        }

        else if (Input.GetMouseButton(1))
        {
            //�������� Ŭ��
            transform.localEulerAngles = new Vector3(
                transform.localEulerAngles.x,
                transform.localEulerAngles.y,
                transform.localEulerAngles.z - speed);
        }

    }

}
