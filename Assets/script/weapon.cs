using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public float damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
        if (enemy !=null)
        {
            enemy.TakeDamage(damage);
        }
        prayferMovement prayfer = collision.GetComponent<prayferMovement>();
        if (prayfer !=null)
        {
            prayfer.TakeDamage(damage);
        }
        ScareCrowMovement ScareCrow = collision.GetComponent<ScareCrowMovement>();
        if (ScareCrow !=null)
        {
            ScareCrow.TakeDamage(damage);
        }
    }
}
