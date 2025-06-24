using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prayferMovement : MonoBehaviour
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
    public float timeUntillMove;

    public GameObject bullet;
    public float fireForce = 2f;
    public Transform Aim;

    private bool time = false;
    private Animator animator;
    public bool shooting;

    public PlayerController PlayerController;
    public Rigidbody2D Player;

    [Header("Loot")]
    public List<LootDrops> lootTable = new List<LootDrops>();

    // Hit protection
    private bool hasBeenHit = false;
    public float hitCooldown = 0.2f;
    private float lastHitTime;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
        if (wallCheck.y >= 6f)
        {
            rb.velocity = new Vector2(0f, -2f);
        }
        else if (wallCheck.y <= -6f)
        {
            rb.velocity = new Vector2(0f, 2f);
        }
        else if (wallCheck.x >= 8f)
        {
            rb.velocity = new Vector2(-2f, 0f);
            transform.localScale = new Vector3(-.5f, .5f, .5f);
        }
        else if (wallCheck.x <= -11f)
        {
            rb.velocity = new Vector2(2f, 0f);
            transform.localScale = new Vector3(.5f, .5f, .5f);
        }

        timeUntillMove -= Time.deltaTime;
        if (timeUntillMove <= 0)
        {
            shooting = false;
            animator.SetBool("shooting", shooting);
            rng = Random.Range(1, 8);
            if (rng == 1)
            {
                rb.velocity = new Vector2(0f, 2f);
            }
            else if (rng == 2)
            {
                rb.velocity = new Vector2(0f, -2f);
            }
            else if (rng == 3)
            {
                rb.velocity = new Vector2(2f, 0f);
                transform.localScale = new Vector3(.5f, .5f, .5f);
            }
            else if (rng == 4)
            {
                rb.velocity = new Vector2(-2f, 0f);
                transform.localScale = new Vector3(-.5f, .5f, .5f);
            }
            else
            {
                rb.velocity = Vector2.zero;
                shoot();
                time = true;
            }

            SetTimeUntillMove();
        }

        // Reset hit cooldown
        if (hasBeenHit && Time.time - lastHitTime >= hitCooldown)
        {
            hasBeenHit = false;
        }
    }

    private void SetTimeUntillMove()
    {
        if (time)
        {
            timeUntillMove = 1;
            time = false;
        }
        else
        {
            timeUntillMove = Random.Range(minimumTimeTillMove, maximumTimeTillMove);
        }
    }

    public void TakeDamage(float damage)
    {
        if (hasBeenHit) return;

        hasBeenHit = true;
        lastHitTime = Time.time;

        health -= damage;
        if (health <= 0)
        {
            foreach (LootDrops lootDrops in lootTable)
            {
                if (Random.Range(0f, 100f) <= lootDrops.dropChance)
                {
                    InstantiateLoot(lootDrops.itemPrefab);
                    break;
                }
            }

            if (Player != null)
            {
                Player.velocity = Vector2.zero;
            }

            if (PlayerController != null)
            {
                PlayerController.canMove = true;
            }

            StartCoroutine(DieAndDisable());
        }
    }

    public IEnumerator DieAndDisable()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }

    void InstantiateLoot(GameObject loot)
    {
        if (loot)
        {
            Instantiate(loot, transform.position, Quaternion.identity);
        }
    }

    void OnDisable()
    {
        timeUntillMove = 0;
        transform.position = ogPosition;
        health = 3f;
        hasBeenHit = false; // reset flag so enemy can be hit again on respawn
    }

    private void shoot()
    {
        shooting = true;
        animator.SetBool("shooting", shooting);

        GameObject intBullet = Instantiate(bullet, Aim.position, Aim.rotation);
        GameObject intBullet1 = Instantiate(bullet, Aim.position, Aim.rotation);
        GameObject intBullet2 = Instantiate(bullet, Aim.position, Aim.rotation);
        GameObject intBullet3 = Instantiate(bullet, Aim.position, Aim.rotation);

        intBullet.GetComponent<Rigidbody2D>().AddForce(Aim.up * fireForce, ForceMode2D.Impulse);
        intBullet1.GetComponent<Rigidbody2D>().AddForce(-Aim.up * fireForce, ForceMode2D.Impulse);
        intBullet2.GetComponent<Rigidbody2D>().AddForce(Aim.right * fireForce, ForceMode2D.Impulse);
        intBullet3.GetComponent<Rigidbody2D>().AddForce(-Aim.right * fireForce, ForceMode2D.Impulse);

        Destroy(intBullet, 2f);
        Destroy(intBullet1, 2f);
        Destroy(intBullet2, 2f);
        Destroy(intBullet3, 2f);
    }
}