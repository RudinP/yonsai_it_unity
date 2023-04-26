using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            other.gameObject.SetActive(false);
            PlayerFire playerFire = GameObject.FindWithTag("Player").GetComponent<PlayerFire>();
            playerFire.bulletObjectPool.Add(other.gameObject);
        }
        else if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.SetActive(false);
            EnemyManager enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
            enemyManager.enemyObjectPool.Add(other.gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
