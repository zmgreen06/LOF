using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public bool isMoving;
    

    private Vector2 input;
    
    private Animator animator;

    public LayerMask solidObjectLayer;
    public LayerMask TriggerLayer;

    public Transform Aim;
    public bool isWalking;
    public Vector2 lastMoveDirection;

    public bool canMove;

    public float checkRadius = 0.1f;
    public LayerMask teleportLayer;

    public GameObject MovingCamera;
    public GameObject MainCamera;

    public bool boarderOn;

  

     

    private void Awake()
    {
        animator = GetComponent<Animator>();
        canMove = true;
        boarderOn = true;
    }
    private void Update()
    {
        
        if(canMove == true)
        {
            boarderOn = true;
        }
        if(input.x != 0 || input.y != 0){
            lastMoveDirection = input;
            isWalking = true;
        }
        if(isWalking)
        {

            Vector3 vector3 = Vector3.left * input.x + Vector3.down * input.y;
            Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
            
        }
        if(input.x == 0 && input.y == 0){
            isWalking = false;
            Vector3 vector3 = Vector3.left * lastMoveDirection.x + Vector3.down * lastMoveDirection.y;
            Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }

        
        if (!isMoving && canMove) // ✅ added canMove check here
        {
            if (!isMoving)
            {
                input.x = Input.GetAxisRaw("Horizontal");
                input.y = Input.GetAxisRaw("Vertical");
                

                

                if(input.x!=0) input.y=0;

                if (input != Vector2.zero)
                {
                    animator.SetFloat("moveX", input.x/5);
                    animator.SetFloat("moveY", input.y/5);

                    var targetPos = transform.position;
                    targetPos.x += input.x/5;
                    targetPos.y += input.y/5;



                    if(IsWalkable(targetPos))

                        StartCoroutine(Move(targetPos));
                }
            }
            
        }

        

        animator.SetBool("isMoving", isMoving);
    }

    IEnumerator Move(Vector3 targetPos)
    {
        if(canMove == true)
        {
            //boarderOn = true;
            isMoving = true;
            while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
            transform.position = targetPos;

            isMoving = false;
            


            CheckForEncounters();
        }
        if(canMove != true)
        {
           
        }
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if(Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectLayer) != null)
        {
            return false;
        }
        return true;

    }


    public void StopMovement()
    {
        StopAllCoroutines();  // This stops the Move coroutine
        isMoving = false;
        boarderOn = false;

    }


    void OnTriggerEnter(Collider other)
    {
        
    }





    private void CheckForEncounters()
    {
        if (Physics2D.OverlapCircle(transform.position, checkRadius, teleportLayer)){
            Collider2D hit = Physics2D.OverlapCircle(transform.position, checkRadius, teleportLayer);
            Scene currentScene = SceneManager.GetActiveScene ();
            string sceneName = currentScene.name;
        //print("yo");
        if (hit != null){
            if (hit.CompareTag("ForrestTele"))
            {
                transform.position = new Vector2(-811f,-71.4f);
            }
            else if (hit.CompareTag("ForrestExit"))
            {
                transform.position = new Vector2(-1f,52.6f);
            }
            else if (hit.CompareTag("City1"))
            {
                MainCamera.SetActive(false);
                transform.position = new Vector2(-12f,-62f);
                //MainCamera.SetActive(false);
                MovingCamera.SetActive(true);

            }
            else if (hit.CompareTag("City1WestExit"))
            {
                MovingCamera.SetActive(false);
                
                transform.position = new Vector2(42f,0f);
                MainCamera.SetActive(true);
                

            }
            else if (hit.CompareTag("BarnEnter"))
            {
                transform.position = new Vector2(113f,-57f);
                MainCamera.SetActive(false);
                MovingCamera.SetActive(true);

            }
            else if (hit.CompareTag("BarnExit"))
            {
                transform.position = new Vector2(52f,23f);
                MainCamera.SetActive(true);
                MovingCamera.SetActive(false);

            }


            
            
        }

        
        }
    }

    

}