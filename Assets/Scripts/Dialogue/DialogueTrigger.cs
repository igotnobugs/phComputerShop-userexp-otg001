using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Reference: https://www.youtube.com/watch?v=_nRzoTzeyxU&ab_channel=Brackeys
/* Modified to be scaleable and extendable
 */

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager = null;

    public Dialogue dialogue;

    private void Start() {
        if (dialogueManager == null) {
            dialogueManager = FindObjectOfType<DialogueManager>();
        }
    }

    public void TriggerDialogue(Action onCompleteFunc)
    {
        dialogueManager.StartDialogue(dialogue, onCompleteFunc);
    }
}
