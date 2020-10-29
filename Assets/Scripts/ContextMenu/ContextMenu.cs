using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenu : MonoBehaviour 
{   
    public RectTransform canvas;
    private RectTransform rectTransform;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable() {
        foreach (Transform child in transform) {
            IContextButton button = child.gameObject.GetComponent<IContextButton>();
            if (button != null) {
                button.Enable();
            }
        }
    }

    private void OnDisable() {
        //foreach (Transform child in transform)
        //    child.gameObject.SetActive(false);
    }

    private void Start() {
        gameObject.SetActive(false);
    }

    // Move context menu relative to world pos
    public void MoveTo(Vector2 worldPos) {      
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(worldPos);
        Vector2 screenPosition = new Vector2(
                (viewportPosition.x * canvas.sizeDelta.x) - (canvas.sizeDelta.x * 0.5f),
                (viewportPosition.y * canvas.sizeDelta.y) - (canvas.sizeDelta.y * 0.5f));

        rectTransform.anchoredPosition = screenPosition;
    }
}
