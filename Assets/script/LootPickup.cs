using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LootPickup : MonoBehaviour
{
    //public Attack attack;
    //public Image healthBar;
    //private SpriteRenderer spriteRenderer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("My tag is: " + gameObject.tag);
            if (gameObject.tag == "PelletDrop"){
                //Debug.Log("My tag is: " + gameObject.tag);
                Attack attack = other.GetComponent<Attack>();
                if (attack != null)
                {
                    attack.pelletCounter +=5; // Add 3 to pelletCounter
                }
            }
            else if (gameObject.tag == "HealthDrop"){
                //Debug.Log("My tag is: " + gameObject.tag);
                
                playerHealth Phealth = other.GetComponent<playerHealth>();
                if (Phealth.maxHealth > Phealth.health){
                    Phealth.health += 30;
                }
                //healthBar.fillAmount = (float)Phealth.health / Phealth.maxHealth;
            }

            
            Destroy(gameObject); // Destroy the loot after pickup
        }
    }
}