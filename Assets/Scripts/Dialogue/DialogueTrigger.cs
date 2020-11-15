using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Reference: https://www.youtube.com/watch?v=_nRzoTzeyxU&ab_channel=Brackeys
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue(Action onCompleteFunc)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, onCompleteFunc);
    }
}
