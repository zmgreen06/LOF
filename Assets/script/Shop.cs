using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Shop : MonoBehaviour
{
    public GameObject buyHealth;
    public GameObject shopPanel;
    public bool playerIsClose;
    public bool isActive;
    public int basePrice = 10;
    public static int price;

    public playerHealth health;
    public PlayerController speed;
    public static int amountOfBuys;
    public Attack attack;

    public TextMeshProUGUI priceText;
    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        price = basePrice;
        amountOfBuys = 1;
        updatePriceText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && playerIsClose)
        {
            if(!isActive){
                shopPanel.SetActive(true);
                isActive = true;
            }else if (isActive){
                shopPanel.SetActive(false);
                isActive = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            playerIsClose = true;
            
        }
        //QUEST CHECKER
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }

    public void buyHealthClicked(){
        //print(attack.pelletCounter);
        if(attack.pelletCounter >= price){
            health.maxHealth = health.maxHealth + 30;
            inflation();
        }

    }

    public void buySpeedClicked(){
        if(attack.pelletCounter >= price){
            //print(speed.moveSpeed);
            speed.moveSpeed = speed.moveSpeed + .5f;
            inflation();
        }

    }

    public void inflation(){
        attack.pelletCounter -= price;
        amountOfBuys+=1;
        price = (int)(basePrice*(1+.75f*(amountOfBuys-1)));
        updatePriceText();
    }
    void updatePriceText(){
        priceText.text = "Current Price of Next purchase: " + price.ToString();
    }
}
