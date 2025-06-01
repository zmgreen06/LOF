using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperMovement : MonoBehaviour
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
    private float x;
    private float y;

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
        x=-(transform.position.x);
        y = Mathf.Pow(x, 2.0f);
        Debug.Log(y);
        wallCheck = gameObject.transform.localPosition;
        if(wallCheck.y >= 6f)
        {
            rb.velocity = new Vector2(0f, -2f);
        }
        else if(wallCheck.y <= -6f)
        {
            rb.velocity = new Vector2(0f, 2f);
        }
        else if(wallCheck.x >= 11f)
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
            rng = Random.Range(1, 1);
            if(rng == 1)
            {
                
                rb.velocity = new Vector2(2f, y/20);
                
            }
            else if(rng == 2)
            {
                rb.velocity = new Vector2(-2f, y/20);
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
            gameObject.SetActive(false);
            
        }
    }
    void OnDisable()
    {
        timeUntillMove = 0;
        transform.position = ogPosition;
        health = 3f;
    }
    
}