using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCheck : MonoBehaviour{
        private void OntriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Enter Collider");
        }
        private void OntriggerExit2D(Collider2D collision)
        {
            Debug.Log("Exit Collider");
        }
    }