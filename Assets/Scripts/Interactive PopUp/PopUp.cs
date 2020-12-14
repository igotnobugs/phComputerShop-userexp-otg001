using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/* Attached to a pop-up prefab
 * Created by a popup manager
 * 
 */

public class PopUp : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI popUpTitle = null;
    [SerializeField] private TextMeshProUGUI popUpText = null;

    [SerializeField] private Button closeButton = null;

    private Canvas canvas;
    private RectTransform rectTransform;
    private BaseUI targetUI;
    private Action onCompleteFunc;
    public SceneAudioManager audioManager;

    private void Awake() {
        audioManager = FindObjectOfType<SceneAudioManager>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    private void Start() {
        closeButton.onClick.AddListener(() => {
            PlayButtonAudio();
            DestroyPopup();
        });
    }

    // When initiated, must run the Init
    public void Init(string title, string content) {
        popUpTitle.text = title;
        popUpText.text = content;       
    }

    public void MoveTo(Vector2 pivot, Vector2 anchorPosition) {
        rectTransform.position = anchorPosition;
        rectTransform.pivot = pivot;
    }

    public void SetListener(BaseUI ui, bool opposite = false) {
        targetUI = ui;
        if (!opposite) {
            targetUI.OnActivating += DestroyPopup;
        } else {
            targetUI.OnDeactivating += DestroyPopup;
        }
    }

    public void SetOnComplete(Action function) {
        onCompleteFunc = function;
    }

    public void DestroyPopup() {
        onCompleteFunc?.Invoke();
        targetUI.OnActivating -= DestroyPopup;
        targetUI.OnDeactivating -= DestroyPopup;
        Destroy(gameObject);
    }

    public void PlayButtonAudio() {
        if (audioManager != null)
        {
            audioManager.Play("ButtonClick");
        }
        else
            Debug.LogError("No Audio Manager found for PopUp");
    }
}
