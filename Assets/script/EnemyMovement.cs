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
        if(wallCheck.y >= 6f)
        {
            rb.velocity = new Vector2(0f, -2f);
        }
        else if(wallCheck.y <= -6f)
        {
            rb.velocity = new Vector2(0f, 2f);
        }
        else if(wallCheck.x >= 8f)
        {
            rb.velocity = new Vector2(-2f, 0f);
        }
        else if(wallCheck.x <= -11f)
        {
            rb.velocity = new Vector2(2f, 0f);
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
            }
            else if(rng == 4)
            {
                rb.velocity = new Vector2(-2f, 0f);
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