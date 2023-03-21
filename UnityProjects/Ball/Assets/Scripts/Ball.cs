using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //정수형 변수인 count 선언 후 1의 값으로 초기화
    int count = 1;

    // 첫번째 프레임 시작 전 처음에 1회 실행되는 메소드
    void Start()
    {
        //Debug 클래스의 Log 메소드를 실행(콘솔창에 매개변수로 받은 내용을 출력)
        Debug.Log("Start");
        count++;
    }

    // 매 프레임마다 실행되는 메소드
    void Update()
    {
        Debug.Log("Update" + count);
        //count값을 1 증가시킨다.
        count += 2;

        //여러줄 tab 반대는 여러줄 shift+tab
    }
}
