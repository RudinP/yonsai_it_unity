using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //������ ������ count ���� �� 1�� ������ �ʱ�ȭ
    int count = 1;

    // ù��° ������ ���� �� ó���� 1ȸ ����Ǵ� �޼ҵ�
    void Start()
    {
        //Debug Ŭ������ Log �޼ҵ带 ����(�ܼ�â�� �Ű������� ���� ������ ���)
        Debug.Log("Start");
        count++;
    }

    // �� �����Ӹ��� ����Ǵ� �޼ҵ�
    void Update()
    {
        Debug.Log("Update" + count);
        //count���� 1 ������Ų��.
        count += 2;

        //������ tab �ݴ�� ������ shift+tab
    }
}
