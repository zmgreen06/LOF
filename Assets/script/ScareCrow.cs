using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareCrowMovement : MonoBehaviour
{
    private Transform player; 
    
    public float moveSpeed = .00002f;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;

    float health, maxHealth = 15f;
    private Vector3 ogPosition;
    private Vector3 wallCheck;
    private float rng;
    
    public float minimumTimeTillMove;
    public float maximumTimeTillMove;
    
    private float timeUntillMove;


    public GameObject crow;
    public float fireForce = 2f;
    public Transform Aim1;
    public Transform Aim2;
    public Transform Aim3;
    public Transform Aim4;

    private bool time = false;

    //private Animator animator;
    public bool crowing;
    

    private void Awake()
    {
        //animator = GetComponent<Animator>();
        timeUntillMove = 0;
        ogPosition = gameObject.transform.position;
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Start()
    {
        health = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
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
            crowing = false;
            rng = Random.Range(1, 4);
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
            else
            {
                rb.velocity = new Vector2(0f, 0f);
                time = true;
            }
            SetTimeUntillMove();
        }

    }

    private void SetTimeUntillMove()
    {
        if(time){
            timeUntillMove = 1;
            time = false;
        }else{
            timeUntillMove = Random.Range(minimumTimeTillMove, maximumTimeTillMove);
            crows();
            crows();
        }
    }


    public void TakeDamage(float damage)
    {
        print(health);
        health -= damage;
        if(health <= 0)
        {
            gameObject.SetActive(false);
            
        }
    }
    // void OnDisable()
    // {
    //     timeUntillMove = 0;
    //     transform.position = ogPosition;
    //     health = 3f;
    // }
    

    private void crows()
    {
        crowing = true;
        GameObject intcrow = Instantiate(crow, Aim1.position, Quaternion.identity);
        GameObject intcrow1 = Instantiate(crow, Aim2.position, Quaternion.identity);
        GameObject intcrow2 = Instantiate(crow, Aim3.position, Quaternion.identity);
        GameObject intcrow3 = Instantiate(crow, Aim4.position, Quaternion.identity);

        Vector2 dir1 = (player.position - Aim1.position).normalized;
        Vector2 dir2 = (player.position - Aim2.position).normalized;
        Vector2 dir3 = (player.position - Aim3.position).normalized;
        Vector2 dir4 = (player.position - Aim4.position).normalized;

        // Apply 180-degree corrected rotation
        intcrow.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir1.y, dir1.x) * Mathf.Rad2Deg + 180f);
        intcrow1.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir2.y, dir2.x) * Mathf.Rad2Deg + 180f);
        intcrow2.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir3.y, dir3.x) * Mathf.Rad2Deg + 180f);
        intcrow3.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir4.y, dir4.x) * Mathf.Rad2Deg + 180f);

        intcrow.GetComponent<Rigidbody2D>().AddForce(dir1 * fireForce, ForceMode2D.Impulse);
        intcrow1.GetComponent<Rigidbody2D>().AddForce(dir2 * fireForce, ForceMode2D.Impulse);
        intcrow2.GetComponent<Rigidbody2D>().AddForce(dir3 * fireForce, ForceMode2D.Impulse);
        intcrow3.GetComponent<Rigidbody2D>().AddForce(dir4 * fireForce, ForceMode2D.Impulse);

        Destroy(intcrow, 10f);
        Destroy(intcrow1, 10f);
        Destroy(intcrow2, 10f);
        Destroy(intcrow3, 10f);



      
        
    }
}
