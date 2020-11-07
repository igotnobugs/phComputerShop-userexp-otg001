using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Displays the context menu 
 * Called by the ContextMenuManager
 * 
 */

public class ContextMenu : Singleton<ContextMenu> 
{   
    private Canvas canvas;
    private RectTransform rectTransform;
    private RectTransform canvasRect;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();
    }

    private void OnEnable() {    
        foreach (Transform child in transform) {
            IContextButton button = child.gameObject.GetComponent<IContextButton>();
            if (button != null) {
                button.Enable();
            }
        }      
    }

    private void Start() {
        gameObject.SetActive(false);
    }

    // Move context menu relative to world pos
    public void MoveTo(Vector2 worldPos) {      
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(worldPos);
        Vector2 screenPosition = new Vector2(
                (viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f),
                (viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f));

        rectTransform.anchoredPosition = screenPosition;
    }
}
