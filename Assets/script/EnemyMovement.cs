using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = .00002f;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;

    float health, maxHealth = 3f;
    private Vector3 ogPosition;
    private Vector3 wallCheck;
    private float rng;
    
    public float minimumTimeTillMove;
    public float maximumTimeTillMove;
    
    private float timeUntillMove;
    
    public PlayerController PlayerController;
    public Rigidbody2D Player;

    public float xWall;
    public float NxWall;
    public float yWall;
    public float NyWall;

    //Loot
    [Header("Loot")]
    public List<LootDrops> lootTable = new List<LootDrops>();

    private void Awake()
    {
        timeUntillMove = 0;
        ogPosition = gameObject.transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        wallCheck = gameObject.transform.localPosition;
        if(wallCheck.y >= yWall)
        {
            rb.velocity = new Vector2(0f, -2f);
            
        }
        else if(wallCheck.y <= NyWall)
        {
            rb.velocity = new Vector2(0f, 2f);
        }
        else if(wallCheck.x >= xWall)
        {
            rb.velocity = new Vector2(-2f, 0f);
            transform.localScale = new Vector3(.5f, .5f, .5f); 
            
        }
        else if(wallCheck.x <= NxWall)
        {
            rb.velocity = new Vector2(2f, 0f);
            transform.localScale = new Vector3(-.5f, .5f, .5f);
        }




        timeUntillMove -= Time.deltaTime;
        if(timeUntillMove <=0)
        {
            rng = Random.Range(1, 5);
            if(rng == 1)
            {
                rb.velocity = new Vector2(0f, 2f);
                
            }
            else if(rng == 2)
            {
                rb.velocity = new Vector2(0f, -2f);
            }
            else if(rng == 3)
            {
                rb.velocity = new Vector2(2f, 0f);
                transform.localScale = new Vector3(-.5f, .5f, .5f);
            }
            else if(rng == 4)
            {
                rb.velocity = new Vector2(-2f, 0f);
                transform.localScale = new Vector3(.5f, .5f, .5f); 
            }
            SetTimeUntillMove();
        }

    }

    private void SetTimeUntillMove()
    {
        timeUntillMove = Random.Range(minimumTimeTillMove, maximumTimeTillMove);
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            //Loot Drops
            foreach(LootDrops LootDrops in lootTable){
                if(Random.Range(0f,100f) <= LootDrops.dropChance){
                    InstantiateLoot(LootDrops.itemPrefab);
                    break;
                }
                
            }
            //end of loot drops


            if (Player != null)
            {
                Player.velocity = Vector2.zero;
            }

            if (PlayerController != null)
            {
                PlayerController.canMove = true;
            }
            gameObject.SetActive(false);
            
        }
    }

    //More Loot drop
    void InstantiateLoot(GameObject loot){
        if(loot){
            GameObject droppedLoot = Instantiate(loot, transform.position, Quaternion.identity);

            
        }
    }

    

    void OnDisable()
    {
        timeUntillMove = 0;
        transform.position = ogPosition;
        health = 3f;

        
    }
    
}