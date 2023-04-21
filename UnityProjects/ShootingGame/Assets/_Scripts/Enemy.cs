using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    Vector3 dir;

    public GameObject explosionFactory;

    private void Start()
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

    }
    private void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.tag.Equals("Player"))
        {
            GameObject smObject = GameObject.Find("ScoreManager");
            ScoreManager sm = smObject.GetComponent<ScoreManager>();
            sm.AddScore();
        }
        
        GameObject explosion = Instantiate(explosionFactory);
        explosion.transform.position = transform.position;

        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
