using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // If you are using TextMeshPro

public class ItemCounter : MonoBehaviour
{
    public Attack attack;
    public TextMeshProUGUI countText; // Assign in inspector
    

    void Update()
    {
        Attack attack = FindObjectOfType<Attack>();
        
        // Update the text with the count
        countText.text = ":" + attack.pelletCounter;
    }
}
