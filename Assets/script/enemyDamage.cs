using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class enemyDamage : MonoBehaviour
{
    
    public Rigidbody2D rb2d;
    public Rigidbody2D Player;
    private float strength = 20;
    public playerHealth playerHealth;
    public PlayerController PlayerController;
    public int damage;
    //private bool hit = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Hit()
    {
        Player.velocity = Vector2.zero;
        PlayerController.canMove = true;
        Player.velocity = Vector2.zero;
    }

    // Update is called once per frame
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
            PlayerController.canMove = false;
            Player.velocity = Vector2.zero;
            //Debug.Log(transform.position);
            Vector2 direction = (Player.transform.position-transform.position);
            //Debug.Log(direction);
            Player.velocity = new Vector2(direction.x, direction.y) * strength;
            //Player.velocity = new Vector2(0f,0f);
            //hit = true;
            Invoke("Hit", .25f);
        }
    }
    void OnDisable()
    {
        PlayerController.canMove = true;
    }

    
}
