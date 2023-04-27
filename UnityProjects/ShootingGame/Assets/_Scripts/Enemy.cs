using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    Vector3 dir;

    public GameObject explosionFactory;
    public GameObject bulletFactory;

    public GameObject firePosition;

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
                dir.Normalize(); // dir�� ������ ����. �׷��� �ʰ� ������ a = dir.normalized; ���� ���¸� ���
            }
        }

        if (gameObject.name.Contains("Enemy3"))
            StartCoroutine(Fire());

    }
    private void Update()
    {
        transform.position += dir * speed * Time.deltaTime;

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject explosion = Instantiate(explosionFactory);
        explosion.transform.position = transform.position;

        if (collision.gameObject.tag == "Bullet")
        {
            ScoreManager.Instance.Score++;
            collision.gameObject.SetActive(false);

            PlayerFire playerFire = GameObject.FindWithTag("Player").GetComponent<PlayerFire>();
            if (collision.gameObject.name.Contains("Big"))
                playerFire.bigBulletObjectPool.Add(collision.gameObject);
            else
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

    IEnumerator Fire()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(.5f);

            GameObject bullet = Instantiate(bulletFactory);
            bullet.transform.position = firePosition.transform.position;

            yield return new WaitForSeconds(1);
        }
    }
}
