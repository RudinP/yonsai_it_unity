using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            other.gameObject.SetActive(false);
            PlayerFire playerFire;
            if ((playerFire = GameObject.FindWithTag("Player").GetComponent<PlayerFire>()) != null)
            {
                if (other.gameObject.name.Contains("Big"))
                    playerFire.bigBulletObjectPool.Add(other.gameObject);
                else
                    playerFire.bulletObjectPool.Add(other.gameObject);
            }
        }
        else if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.name.Contains("Bullet"))
                Destroy(other.gameObject);
            else
            {
                other.gameObject.SetActive(false);
                EnemyManager enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
                enemyManager.enemyObjectPool.Add(other.gameObject);
            }
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
