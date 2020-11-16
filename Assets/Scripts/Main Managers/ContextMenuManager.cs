using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Activates when Right-clicking
 * Displays the context menu
 */

public class ContextMenuManager : Singleton<ContextMenuManager> 
{
    public static bool ContextMenuOpen { get; private set; } = false;
    public static Vector3 ContextMenuGridPosition { get; private set; }

    public ContextMenu contextMenu;
    private MouseInput mouseInput;
    private GameObject cursor;

    private void Awake() {
        mouseInput = new MouseInput();
        cursor = GameObject.FindGameObjectWithTag("Cursor");
    }

    private void OnEnable() {
        mouseInput.Enable();
    }

    private void OnDisable() {
        mouseInput.Disable();
    }

    private void Start() {
        mouseInput.Mouse.MouseRightClick.performed += _ => MouseRightClick();
    }

    //If context menu is already open, disable it
    private void MouseRightClick() {
        if (!ContextMenuOpen) {
            if (SelectionManager.HoveredObject != null ||
                SelectionManager.SelectedObject != null) {
                contextMenu.gameObject.SetActive(true);
                MoveContextMenu(GridCursor.GridPositionWorld);
                cursor.gameObject.SetActive(false);
            }
        }
        else {
            DisableContextMenu();
        }
    }

    private void MoveContextMenu(Vector2 worldPos) {
        ContextMenuOpen = true;
        SelectionManager.enableSelection = false;
        ContextMenuGridPosition = GridCursor.GridPositionOffset;
        contextMenu.MoveTo(worldPos);
    }

    public void DisableContextMenu() {
        ContextMenuOpen = false;
        SelectionManager.enableSelection = true;
        cursor.gameObject.SetActive(true);
        contextMenu.gameObject.SetActive(false);
    }
}
