using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject stoneObj;
    float timeCount;
    AudioSource shooterSource;
    // Start is called before the first frame update
    void Start()
    {
        timeCount = 0;
        shooterSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount > 3.0f) 
        {
            shooterSource.Play();
            Instantiate(stoneObj, transform.position, Quaternion.identity);
            timeCount = 0;
        }
    }
}
