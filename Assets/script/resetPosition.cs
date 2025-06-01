using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetPosition : MonoBehaviour
{
    private Vector3 ogPosition;
    void OnEnable()
    {
        Vector3 ogPosition = gameObject.transform.localPosition;
    }

    void OnDisable()
    {
        //Vector3 ogPosition = gameObject.transform.position;
        Debug.Log(ogPosition);
        transform.localPosition = ogPosition;
    }
}
