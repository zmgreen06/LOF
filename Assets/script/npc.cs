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

    public bool dialogueDone;

    public static int Quest;            // static now
    public static bool QuestUpdate;     // static now

    void Start()
    {
        QuestUpdate = false;
        dialogueDone = false;
        index = 0;
    }

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

        if(QuestUpdate){
            Quest += 1;
            QuestUpdate = false;
        }

        (int start, int end) = GetDialogueRangeForQuest();

        if (dialogueText != null && dialogue.Length > 0 && index + start < dialogue.Length && dialogueText.text == dialogue[start + index])
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
        (int start, int end) = GetDialogueRangeForQuest();
        int targetIndex = start + index;

        if (dialogueText != null && targetIndex < dialogue.Length)
        {
            string currentLine = dialogue[targetIndex];

            if (string.IsNullOrWhiteSpace(currentLine))
            {
                EndDialogue();
                yield break;
            }

            dialogueText.text = "";

            foreach (char letter in currentLine.ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(wordSpeed);
            }
        }
        else
        {
            EndDialogue(); // Also call this if index is out of range
        }
    }

    public void NextLine()
    {
        (int start, int end) = GetDialogueRangeForQuest();

        if (cButton != null)
        {
            cButton.SetActive(false);
        }

        // Check if we're at or past the end of the dialogue range
        if ((start + index) >= end - 1)
        {
            EndDialogue();  // <--- call a separate method here
            return;
        }

        index++;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        if (dialogueText != null)
        {
            string nextLine = dialogue[start + index];
            if (string.IsNullOrWhiteSpace(nextLine))
            {
                EndDialogue();  // <--- handle empty line as end
                return;
            }

            dialogueText.text = "";
            typingCoroutine = StartCoroutine(Typing());
        }
    }

    private (int start, int end) GetDialogueRangeForQuest()
    {
        switch (Quest)////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            case 0:
                return (0, 7); // entries 1 through 6
            case 1:
                return (7, 14); // entries 8 through 12
            // Add more cases as needed
            default:
                return (0, 0); // empty fallback
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            zeroText();
        }
        //QUEST CHECKER
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
        //QUEST CHECKER///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if(this.CompareTag("Quest1") && dialogueDone == true){
            if (Quest == 0){
                QuestUpdate = true;       // this updates the static variable for all npcs
                dialogueDone = false;
            }
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

    private void EndDialogue()
    {
        zeroText();
        dialogueDone = true;

    }
    
}