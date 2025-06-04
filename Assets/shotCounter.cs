using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shotCounter : MonoBehaviour
{
    public Text shotsLeft;

    int shots = 0;
    // Start is called before the first frame update
    void Start()
    {
        shotsLeft.text = shots.ToString() + " Pellets";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
