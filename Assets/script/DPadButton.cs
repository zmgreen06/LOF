using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DPadButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    public enum Direction { Up, Down, Left, Right }
    public Direction direction;

    private static bool isTouching = false;
    private static PlayerController playerController;

    private void Awake()
    {
        if (playerController == null)
            playerController = FindObjectOfType<PlayerController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouching = true;
        SendDirection();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isTouching)
        {
            SendDirection();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouching = false;
        playerController?.StopMobileInput();
    }

    private void SendDirection()
    {
        if (playerController == null) return;

        switch (direction)
        {
            case Direction.Up:    playerController.SetMobileInputDirection("Up"); break;
            case Direction.Down:  playerController.SetMobileInputDirection("Down"); break;
            case Direction.Left:  playerController.SetMobileInputDirection("Left"); break;
            case Direction.Right: playerController.SetMobileInputDirection("Right"); break;
        }
    }
}