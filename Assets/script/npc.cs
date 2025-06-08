using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class npc : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index;

    public GameObject cButton;

    public float wordSpeed;
    public bool playerIsClose;

    private Coroutine typingCoroutine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && playerIsClose)
        {
            if (dialoguePanel != null && dialogueText != null)
            {
                if (dialoguePanel.activeInHierarchy)
                {
                    zeroText();
                }
                else
                {
                    dialoguePanel.SetActive(true);
                    typingCoroutine = StartCoroutine(Typing());
                }
            }
        }

        if (dialogueText != null && dialogue.Length > 0 && dialogueText.text == dialogue[index])
        {
            if (cButton != null && playerIsClose)
            {
                cButton.SetActive(true);
            }
        }
    }

    public void zeroText()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        if (dialogueText != null)
        {
            dialogueText.text = "";
        }

        index = 0;

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        if (cButton != null)
        {
            cButton.SetActive(false);
        }
    }

    IEnumerator Typing()
    {
        if (dialogueText != null && dialogue.Length > index)
        {
            dialogueText.text = "";
            foreach (char letter in dialogue[index].ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(wordSpeed);
            }
        }
    }

    public void NextLine()
    {
        if (cButton != null)
        {
            cButton.SetActive(false);
        }

        if (index < dialogue.Length - 1)
        {
            index++;
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }

            if (dialogueText != null)
            {
                dialogueText.text = "";
                typingCoroutine = StartCoroutine(Typing());
            }
        }
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            zeroText();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}

