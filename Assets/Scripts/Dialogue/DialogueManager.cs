using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Reference: https://www.youtube.com/watch?v=_nRzoTzeyxU&ab_channel=Brackeys
/* Modified to be scaleable and extendable
 */

public class DialogueManager : MonoBehaviour
{
    public DialogueBox dialogueBox;

    private Queue<Script> scripts;
    private Action onCompleteFunc;


    void Start()
    {
        scripts = new Queue<Script>();
    }

    public void StartDialogue(Dialogue dialogue, Action onCompleteFunc)
    {      
        //scripts.Clear();
        //dialogueBox.nextButton.onClick.AddListener(() => DisplayNextSentence());

        dialogueBox.NextButtonOnClick(() => DisplayNextSentence());
        foreach (Script script in dialogue.scripts)
        {
            scripts.Enqueue(script);
        }

        this.onCompleteFunc = onCompleteFunc;
        dialogueBox.panelTransistion.Show(() => DisplayNextSentence());
    }

    public void DisplayNextSentence()
    {
        if (scripts.Count == 0)
        {
            EndDialogue();
            return;
        }

        Script script = scripts.Dequeue();
        dialogueBox.Set(script.name, script.sentence);
        dialogueBox.AdvanceDialogue();
    }

    void EndDialogue()
    {
        Debug.Log("End of Conversation");

        dialogueBox.panelTransistion.Hide(() => {
            scripts.Clear();
            onCompleteFunc();          
        });
    }

    public void TriggerDialogue(Dialogue dialogue, Action onCompleteFunc) {
        StartDialogue(dialogue, onCompleteFunc);
    }
}
