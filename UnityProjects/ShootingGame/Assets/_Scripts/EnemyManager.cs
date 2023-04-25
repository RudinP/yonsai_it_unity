using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    float currentTime;
    float createTime;

    float minTime = 1;
    float maxTime = 2;

    public GameObject enemyFactory;

    public int poolSize;
    GameObject[] enemyObjectPool;
    public Transform[] spawnPoints;

    private void Start()
    {
        createTime = Random.Range(minTime, maxTime);

        enemyObjectPool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyFactory);
            enemyObjectPool[i] = enemy;
            enemy.SetActive(false);
        }
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject enemy = enemyObjectPool[i];
                if (!enemy.activeSelf)
                {
                    int index = Random.Range(0, spawnPoints.Length);
                    enemy.transform.position = spawnPoints[index].transform.position;
                    enemy.SetActive(true);
                    break;
                }
            }

            currentTime = 0;
            createTime = Random.Range(minTime, maxTime);
        }
    }
}
