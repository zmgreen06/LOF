using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class FullscreenOnFirstTap : MonoBehaviour
{
    public PlayerController playerController;
    public Attack attack;
    private bool fullscreenActivated = false;

    void Update()
    {
        if (!fullscreenActivated && Input.GetKeyUp(KeyCode.E))
        {
            attack.canShoot = true;
            playerController.canMove = true;
            Screen.fullScreen = true;
            fullscreenActivated = true;

            // Optional: Disable this GameObject if you were using it as an overlay
            gameObject.SetActive(false);
        }
    }
}