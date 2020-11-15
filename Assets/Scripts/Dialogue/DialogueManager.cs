using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Reference: https://www.youtube.com/watch?v=_nRzoTzeyxU&ab_channel=Brackeys
public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialougeText;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialougeText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialougeText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        Debug.Log("End of Conversation");
    }
}
