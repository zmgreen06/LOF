using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LootPickup : MonoBehaviour
{
    private bool pickedUp = false; // Prevent double trigger

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (pickedUp) return; // Already handled
        if (!other.CompareTag("Player")) return;

        pickedUp = true; // Mark as handled

        if (gameObject.CompareTag("PelletDrop"))
        {
            Attack attack = other.GetComponent<Attack>();
            if (attack != null)
            {
                attack.pelletCounter += 5;
            }
        }
        else if (gameObject.CompareTag("HealthDrop"))
        {
            playerHealth Phealth = other.GetComponent<playerHealth>();
            if (Phealth != null && Phealth.health < Phealth.maxHealth)
            {
                Phealth.health = Mathf.Min(Phealth.health + 30, Phealth.maxHealth);
            }
        }

        Destroy(gameObject); // Safely destroy the pickup
    }
}