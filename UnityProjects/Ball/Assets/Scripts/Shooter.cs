using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject stoneObj;
    float timeCount;
    // Start is called before the first frame update
    void Start()
    {
        timeCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount > 3.0f) 
        {
            Instantiate(stoneObj, transform.position, Quaternion.identity);
            timeCount = 0;
        }
    }
}
