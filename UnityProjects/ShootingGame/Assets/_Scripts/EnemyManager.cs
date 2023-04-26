using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    float currentTime;
    float createTime;

    float minTime = 1;
    float maxTime = 2;

    public GameObject enemyFactory;

    public int poolSize;
    public List<GameObject> enemyObjectPool;
    public Transform[] spawnPoints;

    private void Start()
    {
        createTime = Random.Range(minTime, maxTime);

        enemyObjectPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyFactory);
            enemyObjectPool.Add(enemy);
            enemy.SetActive(false);
        }
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            if(enemyObjectPool.Count > 0)
            {
                GameObject enemy = enemyObjectPool[0];
                enemyObjectPool.Remove(enemy);

                int index = Random.Range(0, spawnPoints.Length);
                enemy.transform.position = spawnPoints[index].transform.position;

                enemy.SetActive(true);
            }

            currentTime = 0;
            createTime = Random.Range(minTime, maxTime);
        }
    }
}
