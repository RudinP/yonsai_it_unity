using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    float currentTime;

    public float createTime;
    public GameObject enemyFactory;

    float minTime = 1;
    float maxTime = 5;

    private void Start()
    {
        createTime = Random.Range(minTime, maxTime);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > createTime)
        {
            GameObject enemy = Instantiate(enemyFactory);
            enemy.transform.position = transform.position;

            currentTime = 0;
            createTime = Random.Range(minTime, maxTime);
        }
    }
}
