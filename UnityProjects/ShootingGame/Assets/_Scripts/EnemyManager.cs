using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    float currentTime;
    float createTime;

    float minTime = 1;
    float maxTime = 2;

    public GameObject[] enemyFactory;

    public int poolSize;
    public List<GameObject> enemyObjectPool;

    private void Start()
    {
        createTime = Random.Range(minTime, maxTime);

        enemyObjectPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyFactory[i % 3]);
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
                GameObject enemy = enemyObjectPool[Random.Range(0, enemyObjectPool.Count)];
                enemyObjectPool.Remove(enemy);

                float x = Random.Range(-3f, 3f);
                enemy.transform.position = new Vector3(x, 10, 0);

                enemy.SetActive(true);
            }

            currentTime = 0;
            createTime = Random.Range(minTime, maxTime);
        }
    }

}
