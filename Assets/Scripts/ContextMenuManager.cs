using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenuManager : MonoBehaviour 
{
    public static bool ContextMenuOpen { get; private set; }
    public static Vector3Int ContextMenuGridPosition { get; private set; }

    public ContextMenu contextMenu;
    private MouseInput mouseInput;

    private void Awake() {
        mouseInput = new MouseInput();
    }

    private void OnEnable() {
        mouseInput.Enable();
    }

    private void OnDisable() {
        mouseInput.Disable();
    }

    private void Start() {
        //mouseInput.Mouse.MouseLeftClick.performed += _ => MouseLeftClick();
        mouseInput.Mouse.MouseRightClick.performed += _ => MouseRightClick();
    }

    private void MouseRightClick() {
        if (SelectionManager.HoveredObject != null || 
            SelectionManager.SelectedObject != null) {         
            moveContextMenu(GridCursor.GridPositionWorld);
        }      
    }

    public void moveContextMenu(Vector2 worldPos) {
        ContextMenuOpen = true;
        contextMenu.gameObject.SetActive(true);
        ContextMenuGridPosition = GridCursor.GridPosition;
        contextMenu.MoveTo(worldPos);
    }

    public void DisableContextMenu() {
        ContextMenuOpen = false;
        contextMenu.gameObject.SetActive(false);
    }
}
