using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class FullscreenOnFirstTap : MonoBehaviour, IPointerDownHandler
{
    private bool fullscreenActivated = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!fullscreenActivated)
        {
            Screen.fullScreen = true;
            fullscreenActivated = true;

            // Optional: Disable this panel so it doesn't block input anymore
            gameObject.SetActive(false);
        }
    }
}