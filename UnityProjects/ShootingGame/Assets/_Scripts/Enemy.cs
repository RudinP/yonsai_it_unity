using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    Vector3 dir;

    public GameObject explosionFactory;

    private void OnEnable()
    {
        int randValue = Random.Range(0, 10);

        dir = Vector3.down;

        if (randValue < 3)
        {
            GameObject target = GameObject.Find("Player");
            if (target != null)
            {
                dir = target.transform.position - transform.position;
                dir.Normalize(); // dir의 원본을 변경. 그렇지 않고 싶으면 a = dir.normalized; 같은 형태를 사용
            }
        }

    }
    private void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.tag.Equals("Player"))
        {
            ScoreManager.Instance.Score++;
        }
        
        GameObject explosion = Instantiate(explosionFactory);
        explosion.transform.position = transform.position;

        if(collision.gameObject.tag == "Bullet")
        {
            collision.gameObject.SetActive(false);

            PlayerFire playerFire = GameObject.FindWithTag("Player").GetComponent<PlayerFire>();
            playerFire.bulletObjectPool.Add(collision.gameObject);
        }
        else
        {
            Destroy(collision.gameObject);
        }

        gameObject.SetActive(false);

        GameObject emObject = GameObject.Find("EnemyManager");
        EnemyManager enemyManager = emObject.GetComponent<EnemyManager>();
        enemyManager.enemyObjectPool.Add(gameObject);
        
    }
}
