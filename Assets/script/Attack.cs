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

    // Update is called once per frame
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckMeleeTimer();

        if(Input.GetKeyDown("space"))
        {
            onAttack();
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
