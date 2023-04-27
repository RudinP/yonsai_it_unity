using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletFactory;
    public GameObject bigBulletFactory;
    public GameObject firePosition;

    public int poolSize;
    public List<GameObject> bulletObjectPool;
    public List<GameObject> bigBulletObjectPool;

    private void Start()
    {
        
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletFactory);
            bulletObjectPool.Add(bullet);
            bullet.SetActive(false);

            GameObject bigBullet = Instantiate(bigBulletFactory);
            bigBulletObjectPool.Add(bigBullet);
            bigBullet.SetActive(false);
        }
    }

    private void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            BigFire();
        }
#endif
    }

    public void Fire()
    {
        if (bulletObjectPool.Count > 0)
        {
            GameObject bullet = bulletObjectPool[0];
            bullet.SetActive(true);
            bulletObjectPool.Remove(bullet);

            bullet.transform.position = firePosition.transform.position;
        }
    }

    public void BigFire()
    {
        if (bigBulletObjectPool.Count > 0)
        {
            GameObject bullet = bigBulletObjectPool[0];
            bullet.SetActive(true);
            bigBulletObjectPool.Remove(bullet);

            bullet.transform .position = firePosition.transform.position;
        }
    }
}
