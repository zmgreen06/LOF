using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 100;
    public Image healthBar;

    private bool isInvincible = false;
    public float invincibilityDuration = 1f;

    private SpriteRenderer spriteRenderer;  // Reference to the SpriteRenderer

    void Start()
    {
        health = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();  // Get the SpriteRenderer component
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible) return;

        health -= amount;
        healthBar.fillAmount = (float)health / maxHealth;

        if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;

        float elapsed = 0f;
        // Flash the sprite on and off every 0.1 seconds
        while (elapsed < invincibilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;  // Toggle visibility
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }

        spriteRenderer.enabled = true;  // Make sure sprite is visible at the end
        isInvincible = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            TakeDamage(10);
        }
    }
}
