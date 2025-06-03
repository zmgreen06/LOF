using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraGrid : MonoBehaviour
{
    public Transform target;
    public Vector2 size;
    public float speed;
    public PlayerController grid;
    
    

    // Update is called once per frame
    void Update()
    {
       
        if(grid.boarderOn){
            Vector3 pos = new Vector3(
                Mathf.RoundToInt(target.position.x/size.x)*size.x, Mathf.RoundToInt(target.position.y/size.y)*size.y);
                transform.position = Vector3.Lerp(transform.position, pos, speed);
        }
    }
}
