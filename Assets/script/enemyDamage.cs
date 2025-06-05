using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class enemyDamage : MonoBehaviour
{
    
    public Rigidbody2D rb2d;
    public Rigidbody2D Player;
    private float strength = 10f;
    public playerHealth playerHealth;
    public PlayerController PlayerController;
    public int damage;
    public bool boarderOn;
    //private bool hit = false;
   // PlayerController player = FindObjectOfType<PlayerController>();
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController == null)
        PlayerController = FindObjectOfType<PlayerController>();

        if (playerHealth == null)
            playerHealth = FindObjectOfType<playerHealth>();
    }
    private void Hit()
    {
        Player.velocity = Vector2.zero;
        //PlayerController.canMove = true;
        //Player.velocity = Vector2.zero;
    }

    // Update is called once per frame

    private IEnumerator ResetMovementAfterKnockback(float delay)
    {
        
        yield return new WaitForSeconds(delay);
        Rigidbody2D rb = Player.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        PlayerController.canMove = true;
        boarderOn = true;
        // Optional: zero out velocity again if needed
        
       
    }


    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if(collision.gameObject.tag == "Player")
        {
            {
                PlayerController.StopMovement();  // Immediately cancel movement
                PlayerController.canMove = false;
                playerHealth.TakeDamage(damage);
                PlayerController.canMove = false;

                // Immediately zero velocity
                Player.velocity = Vector2.zero;

                // Apply knockback (normalized!)
                Vector2 direction = (Player.transform.position - transform.position).normalized;
                Player.AddForce(direction * strength, ForceMode2D.Impulse);
                
                // After knockback duration, re-enable movement
                
                StartCoroutine(ResetMovementAfterKnockback(.25f));
                //Invoke("Hit", .25f);
            }
            
        }
    }
    void OnDisable()
    {
       
    }

    
}
