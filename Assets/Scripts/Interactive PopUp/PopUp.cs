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
    private RectTransform canvasRect;
    private BaseUI targetUI;
    private Action onCompleteFunc; 

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();
    }

    private void Start() {
        closeButton.onClick.AddListener(() => {
            DestroyPopup();
        });
    }

    // When initiated, must run the Init
    public void Init(string title, string content) {
        popUpTitle.text = title;
        popUpText.text = content;       
    }

    public void MoveTo(Vector2 worldPos) {
        rectTransform.position = worldPos;
    }

    public void SetListener(BaseUI ui) {
        targetUI = ui;
        targetUI.OnActivating += DestroyPopup;
    }

    public void SetOnComplete(Action function) {
        onCompleteFunc = function;
    }

    public void DestroyPopup() {
        onCompleteFunc?.Invoke();
        targetUI.OnActivating -= DestroyPopup;
        Destroy(gameObject);
    }
}
