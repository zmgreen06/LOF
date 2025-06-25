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
    


    public PlayerController playerController;
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
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if(!isActive){
                playerController.canMove = false;
                shopPanel.SetActive(true);
                isActive = true;
            }else if (isActive){
                playerController.canMove = true;
                shopPanel.SetActive(false);
                isActive = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            attack.canShoot = false;
            playerIsClose = true;
            
        }
        //QUEST CHECKER
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            attack.canShoot = true;
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

    public void buyKnockBackClicked(){
        if(attack.pelletCounter >= price){
            //print(speed.moveSpeed);
            attack.playerStrength = attack.playerStrength + 1.5f;
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
