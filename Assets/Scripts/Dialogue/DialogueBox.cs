using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

/* Modified to be scaleable and extendable
 */

[RequireComponent(typeof(EventTrigger))]
public class DialogueBox : MonoBehaviour, IPointerClickHandler  
{
    [SerializeField] private TextMeshProUGUI nameText = null;
    [SerializeField] TextMeshProUGUI sentenceText = null;
    public Button nextButton;
    public TransistionUI panelTransistion;

    private string sentenceToType;
    private IEnumerator typingCoroutine;
    private bool isTypingDone = false;
    private Action nextAction;

    private AudioManager audioManager;

    private void Awake() {
        if (nameText == null || sentenceText == null) {
            Debug.Log("Assign the text fields in Dialogue Panel");
        }
        else {
            nameText.text = "";
            sentenceText.text = "";
        }
    }

    private void Start() {
        typingCoroutine = TypeSentence();
        nextButton.gameObject.SetActive(false);
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void NextButtonOnClick(Action onClick) {
        nextAction = onClick;
        nextButton.onClick.AddListener(() => nextAction?.Invoke());
    }

    public void Set(string name, string sentence) {
        nameText.text = name;
        sentenceToType = sentence;
    }

    public void AdvanceDialogue() {
        // Stop last typing routine
        StopCoroutine(typingCoroutine);

        // Start a new one
        typingCoroutine = TypeSentence();
        StartCoroutine(typingCoroutine);
    }

    private IEnumerator TypeSentence() {
        sentenceText.text = "";
        isTypingDone = false;
        nextButton.gameObject.SetActive(false);
        foreach (char letter in sentenceToType.ToCharArray()) {
            sentenceText.text += letter;
            yield return null;
        }
        isTypingDone = true;
        nextButton.gameObject.SetActive(true);
        yield break;
    }

    public virtual void OnPointerClick(PointerEventData eventData) {
        audioManager.Play("ButtonClick");
        if (!isTypingDone) {
            StopCoroutine(typingCoroutine);
            isTypingDone = true;

            sentenceText.text = sentenceToType;
            nextButton.gameObject.SetActive(true);
        }
        else {
            nextAction?.Invoke();
        }
    }
}
