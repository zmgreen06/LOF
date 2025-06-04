using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject Melee;
    bool isAttacking = false;
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

    // Update is called once per frame
    private void Awake()
    {
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
            onShoot();
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
}
