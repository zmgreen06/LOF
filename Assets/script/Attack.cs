using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject Melee;
    public bool isAttacking = false;
    float atkDuration = 0.3f;
    float atkTimer = 0f;
    public bool spacePress;
    private Animator animator;

    public Transform Aim;
    public GameObject pellet;
    public float fireForce = 10f;
    float shootCooldown = .25f;
    float shootTimer = .5f;
    public int pelletCounter = 10;

    public float playerStrength = 5f; 

    public Perks perks;

    public bool canShoot;

    public float stun;
    public int stunChance;

    public GameObject stunIcon;
    


    //public List<Rigidbody2D> wormKnock;
    public EnemyMovement timeUntillMove;

    // Update is called once per frame
    private void Awake()
    {
        stun = .25f;
        canShoot = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        shootTimer += Time.deltaTime;
        CheckMeleeTimer();

        if(Input.GetKeyDown("space"))
        {
            onAttack();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(canShoot){
                onShoot();
            }
        }
    }

    void onShoot(){
        if(shootTimer > shootCooldown && pelletCounter > 0){
            pelletCounter -= 1;
            shootTimer = 0;
            GameObject intPellet = Instantiate(pellet, Aim.position, Aim.rotation);
            intPellet.GetComponent<Rigidbody2D>().AddForce(-Aim.up * fireForce, ForceMode2D.Impulse);
            Destroy(intPellet, 2f);
        }
    }

    void onAttack()
    {
        if (!isAttacking)
        {
            Melee.SetActive(true);
            isAttacking = true;
            //melee animation here
            
            spacePress = true;
            animator.SetBool("spacePress", spacePress);
            
            
        }
    }

    void CheckMeleeTimer()
    {
        if (isAttacking)
        {
            atkTimer += Time.deltaTime;
            if(atkTimer >= atkDuration)
            {
                atkTimer = 0;
                isAttacking = false;
                Melee.SetActive(false);
                spacePress = false;
                animator.SetBool("spacePress", spacePress);
            }
        }
    }
    public void TouchAttack()
    {
        onAttack();
    }

    public void TouchShoot()
    {
        onShoot();
    }

    private IEnumerator ResetEnemyAfterKnockback(Rigidbody2D enemyRb, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Check if enemyRb or enemyRb.gameObject is null or destroyed
        if (enemyRb == null || enemyRb.gameObject == null)
        {
            yield break; // Exit coroutine early, since enemy is gone
        }

        EnemyMovement enemyMovement = enemyRb.GetComponent<EnemyMovement>();
        prayferMovement prayferMovement = enemyRb.GetComponent<prayferMovement>();

        if (perks.stunned == true)
        {
            stunChance = Random.Range(1, 5);

            if (stunChance == 1)
            {
                if (enemyRb.gameObject.activeInHierarchy)
                {
                    stunIcon.SetActive(true);

                    if (enemyMovement != null)
                    {
                        enemyRb.velocity = Vector2.zero;
                        enemyMovement.timeUntillMove = 10000f;
                    }
                    else if (prayferMovement != null)
                    {
                        enemyRb.velocity = Vector2.zero;
                        prayferMovement.timeUntillMove = 10000f;
                    }

                    stun = 5f;
                    StartCoroutine(stunIconWait(enemyRb, stun));
                }
            }
            else
            {
                if (enemyMovement != null)
                {
                    enemyRb.velocity = Vector2.zero;
                    enemyMovement.timeUntillMove = 0.1f;
                }
                else if (prayferMovement != null)
                {
                    enemyRb.velocity = Vector2.zero;
                    prayferMovement.timeUntillMove = 0.1f;
                }
            }
        }
        else
        {
            if (enemyMovement != null)
            {
                enemyRb.velocity = Vector2.zero;
                enemyMovement.timeUntillMove = 0.1f;
            }
            else if (prayferMovement != null)
            {
                prayferMovement.timeUntillMove = 0.1f;
            }
        }

        stun = .25f;
    }


    IEnumerator stunIconWait(Rigidbody2D enemyRb, float stun)
    {
        yield return new WaitForSeconds(stun);

        if (enemyRb == null || enemyRb.gameObject == null)
        {
            stunIcon.SetActive(false);
            yield break;
        }

        EnemyMovement enemyMovement = enemyRb.GetComponent<EnemyMovement>();
        prayferMovement prayferMovement = enemyRb.GetComponent<prayferMovement>();

        if (enemyMovement != null)
        {
            enemyMovement.timeUntillMove = 0f;
        }
        if (prayferMovement != null)
        {
            prayferMovement.timeUntillMove = 0f;
        }

        stunIcon.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if(collision.gameObject.tag == "Enemy" && isAttacking == true)
        {
            {
                // Apply knockback (normalized!)
                Rigidbody2D enemyRb = collision.GetComponent<Rigidbody2D>();
                if(enemyRb != null){
                    Vector2 direction = (enemyRb.transform.position - transform.position).normalized;
                    enemyRb.velocity = Vector2.zero;
                    enemyRb.AddForce(direction * playerStrength, ForceMode2D.Impulse);
                    
                    // After knockback duration, re-enable movement
                    
                    StartCoroutine(ResetEnemyAfterKnockback(enemyRb, .2f));
                    //Invoke("Hit", .25f);
                }
            }
            
        }
    }
}
