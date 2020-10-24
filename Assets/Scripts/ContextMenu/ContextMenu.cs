using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenu : MonoBehaviour 
{
    public static bool contextMenuOpen { get; private set; }

    public RectTransform canvas;
    private RectTransform rectTransform;

    public GameObject[] buttons;

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        gameObject.SetActive(false);
        contextMenuOpen = false;
    }

    private void OnEnable() {
        contextMenuOpen = true;
        GridCursor.trackCursor = false;
        SelectionManager.enableSelection = false;

        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
    }

    private void OnDisable() {
        contextMenuOpen = false;
        GridCursor.trackCursor = true;
        SelectionManager.enableSelection = true;      
    }

    public void DisableContextMenu() {
        contextMenuOpen = false;
        gameObject.SetActive(false);
    }

    // Move context menu relative to world pos
    public void MoveTo(Vector2 worldPos) {
        gameObject.SetActive(true);
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(worldPos);
        Vector2 screenPosition = new Vector2(
                (viewportPosition.x * canvas.sizeDelta.x) - (canvas.sizeDelta.x * 0.5f),
                (viewportPosition.y * canvas.sizeDelta.y) - (canvas.sizeDelta.y * 0.5f));

        rectTransform.anchoredPosition = screenPosition;
    }
}
