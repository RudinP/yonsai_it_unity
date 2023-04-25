using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet" || other.gameObject.tag == "Enemy")
        {
            other.gameObject.SetActive(false);
            if (other.gameObject.tag == "Bullet")
            {
                PlayerFire playerFire = GameObject.FindWithTag("Player").GetComponent<PlayerFire>();
                playerFire.bulletObjectPool.Add(other.gameObject);
            }
        }
        else
            Destroy(other.gameObject);
    }
}
