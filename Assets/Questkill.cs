using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questkill : MonoBehaviour
{
    public npc npc;
    public static int questkill;
    public GameObject MovingCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDisable()
    {
        if (npc.Quest == 2){
            print(questkill);
            if (MovingCamera.activeInHierarchy){
                questkill+=1;
                Destroy(gameObject);
            }
            
        }

        
    }
}
