using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
    public GameObject bombEffect;

    public int attackPower = 10;
    public float explosionRadius = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] cols = Physics.OverlapSphere(
            transform.position,
            explosionRadius,
            1 << LayerMask.NameToLayer("Enemy"));

        for (int i = 0; i < cols.Length; i++)
            cols[i].GetComponent<EnemyFSM>().HitEnemy(attackPower);

        GameObject eff = Instantiate(bombEffect);
        eff.transform.position = transform.position;

        Destroy(gameObject);
    }
}
