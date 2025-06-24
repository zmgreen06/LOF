using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Perks : MonoBehaviour
{
    public GameObject perkPanel;
    public TextMeshProUGUI perkText;
    public bool Bishop = false;

    public PlayerController playerController;
    public Attack attack;

    public Button diagonalMovementButton;
    public Button StunButton;

    public bool stunned = false;
    ////////////////////////////////////////////////////////////PERKS
    
    
    public void DiagonalMovement(){
        perkPanel.SetActive(false);
        Bishop = true;
        playerController.canMove = true;
        attack.canShoot = true;
        diagonalMovementButton.interactable = false;
        Image img = diagonalMovementButton.GetComponent<Image>();
        if (img != null)
        {
            img.color = Color.green;
        }
    }

    public void stunPerk(){
        perkPanel.SetActive(false);
        stunned = true;
        playerController.canMove = true;
        attack.canShoot = true;
        StunButton.interactable = false;
        Image img = StunButton.GetComponent<Image>();
        if (img != null)
        {
            img.color = Color.green;
        }
    }

    public void noMorePerks(){
        perkPanel.SetActive(false);
        playerController.canMove = true;
        attack.canShoot = true;
    }





    public void levelUp(){
        perkPanel.SetActive(true);
        playerController.canMove = false;
        attack.canShoot = false;
    }
}
