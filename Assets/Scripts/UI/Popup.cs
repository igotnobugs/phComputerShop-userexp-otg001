using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* Attached to a pop-up prefab
 * Created by a popup manager
 */

public class Popup : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI popUpTitle = null;
    [SerializeField] private TextMeshProUGUI popUpText = null;

    [SerializeField] private Button closeButton = null;

    private Canvas canvas;
    private RectTransform rectTransform;
    private RectTransform canvasRect;
    public BaseUI targetUI;


    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();
    }

    private void Start() {
        closeButton.onClick.AddListener(() => {
            DestroyPopup();
        });
        if (targetUI != null) {
            targetUI.OnActivated += DestroyPopup;
        }
    }

    // When initiated, must run the Init
    public void Init(string title, string content, BaseUI ui = null) {
        popUpTitle.text = title;
        popUpText.text = content;
        targetUI = ui;
    }


    private void Update() {
        
    }

    public void MoveTo(Vector2 worldPos) {
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(worldPos);
        Vector2 screenPosition = new Vector2(
                (viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f),
                (viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f));

        rectTransform.anchoredPosition = screenPosition;
    }

    public void DestroyPopup() {
        
    }
}
