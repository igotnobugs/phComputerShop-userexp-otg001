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
    private Action onIgnored;
    private Action onFollow;
    private SceneAudioManager audioManager;
    private bool baseUIOpposite = false;

    private void Awake() {
        audioManager = FindObjectOfType<SceneAudioManager>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    private void Start() {
        closeButton.onClick.AddListener(() => {
            PlayButtonAudio();
            DestroyIgnored();
        });
    }

    // When initiated, must run the Init
    public void Init(string title, string content) {
        popUpTitle.text = title;
        popUpText.text = content;       
    }

    public void MoveTo(Vector2 pivot, Vector2 anchorPosition) {
        rectTransform.anchorMin = pivot;
        rectTransform.anchorMax = pivot;     
        rectTransform.anchoredPosition = anchorPosition;
    }

    public void SetListener(BaseUI ui, bool opposite = false, Action onFollowUp = null) {
        targetUI = ui;
        if (!opposite) {
            targetUI.OnActivating += DestroyFollowed;
        }
        else {
            targetUI.OnDeactivating += DestroyFollowed;
        }
        baseUIOpposite = opposite;
        onFollow = onFollowUp;
    }

    public void SetOnIgnored(Action function) {
        onIgnored = function;
    }

    public void DestroyIgnored() {
        onIgnored?.Invoke();
        if (!baseUIOpposite) {
            targetUI.OnActivating -= DestroyFollowed;
        }
        else {
            targetUI.OnDeactivating -= DestroyFollowed;
        }
        Destroy(gameObject);
    }

    public void DestroyFollowed() {
        if (onFollow != null) {
            onFollow?.Invoke();
        } else {
            onIgnored?.Invoke();
        }
        if (!baseUIOpposite) {
            targetUI.OnActivating -= DestroyFollowed;
        }
        else {
            targetUI.OnDeactivating -= DestroyFollowed;
        }
        Destroy(gameObject);
    }


    public void PlayButtonAudio() {
        if (audioManager != null) {
            audioManager.Play("ButtonClick");
        }
        else {
            Debug.LogError("No Audio Manager found for PopUp");
        }
    }
}
