using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public float damage = 1;
    public enum WeaponType { Melee, Pellet }
    public WeaponType weaponType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
        if (enemy !=null)
        {
            enemy.TakeDamage(damage);
            if (weaponType == WeaponType.Pellet)
            {
                Destroy(gameObject);
            }
        }
        prayferMovement prayfer = collision.GetComponent<prayferMovement>();
        if (prayfer !=null)
        {
            prayfer.TakeDamage(damage);
            if (weaponType == WeaponType.Pellet)
            {
                Destroy(gameObject);
            }
        }
        ScareCrowMovement ScareCrow = collision.GetComponent<ScareCrowMovement>();
        if (ScareCrow !=null)
        {
            ScareCrow.TakeDamage(damage);
            if (weaponType == WeaponType.Pellet)
            {
                Destroy(gameObject);
            }
        }
    }
}
