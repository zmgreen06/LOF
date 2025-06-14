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
    public GameObject Directions;

    //public npc dialogueDone;

    public bool boarderOn;

    private Vector2 mobileInput = Vector2.zero;
    private bool usingMobileInput = false;

    //public int Quest;
    ////////////////////////////////////////////////////////////////////////////////////////////
    //Moible check
    public void SetMobileInputDirection(string direction)
    {
        usingMobileInput = true;

        switch (direction)
        {
            case "Up":    mobileInput = Vector2.up; break;
            case "Down":  mobileInput = Vector2.down; break;
            case "Left":  mobileInput = Vector2.left; break;
            case "Right": mobileInput = Vector2.right; break;
        }
    }

    public void StopMobileInput()
    {
        mobileInput = Vector2.zero;
    }
    ///////////////////////////////////////////////////////////////////////////////////////

    private void Awake()
    {
        //Quest = 0;
        animator = GetComponent<Animator>();
        canMove = true;
        boarderOn = true;
        Directions.SetActive(true);
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
                if (usingMobileInput)
                {
                    input = mobileInput;
                }
                else
                {
                    input.x = Input.GetAxisRaw("Horizontal");
                    input.y = Input.GetAxisRaw("Vertical");
                }
                

                

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
        Vector3 offsetPos = targetPos + Vector3.down * 0.309f;

    // Use a debug visualization to verify where it's checking
        
        if(Physics2D.OverlapCircle(offsetPos, 0.1f, solidObjectLayer) != null)
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
            else if (hit.CompareTag("TowningtonEnter"))
            {
                transform.position = new Vector2(-9f,-167f);
                MainCamera.SetActive(false);
                MovingCamera.SetActive(true);

            }
            else if (hit.CompareTag("TowningtonExit"))
            {
                transform.position = new Vector2(-9f,28f);
                MainCamera.SetActive(true);
                MovingCamera.SetActive(false);

            }


            
            
        }

        
        }
    }

 


    

}