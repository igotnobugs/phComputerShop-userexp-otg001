using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static bool enableSelection;
    public static GameObject hoveredObject { get; private set; }
    public static GameObject selectedObject { get; private set; }

    private MouseInput mouseInput;
    public ContextMenu contextMenu;

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
        mouseInput.Mouse.MouseLeftClick.performed += _ => MouseLeftClick();
        mouseInput.Mouse.MouseRightClick.performed += _ => MouseRightClick();
    }

    private void Update() {
        TrackSelection();
    }

    private void TrackSelection() {
        if (!enableSelection) return;

        // Raycast from camera to mouse position
        Vector2 raycastPos = GridCursor.mousePositionWorld;
        RaycastHit2D hit = Physics2D.Raycast(raycastPos, Vector2.zero);

        if (hit.collider != null) {
            // if hit is a Selecteable
            if (hit.collider.gameObject.GetComponent<ISelectable>() != null) {

                //if hit is not the same as recently hovered
                if (hoveredObject != hit.collider.gameObject) {
                    Debug.Log(hit.collider.gameObject.name);
                    hoveredObject = hit.transform.gameObject;
                    hoveredObject.GetComponent<ISelectable>().Hovered();
                }
            }
        }
        else {
            if (hoveredObject != null) {
                hoveredObject.GetComponent<ISelectable>().Unhovered();
            }
            hoveredObject = null;
        }
    }

    private void MouseLeftClick() {
        // Do nothing without hovered object
        if (hoveredObject == null) return;

        // Deselect hovered selected object
        if (selectedObject != null && selectedObject == hoveredObject) {
            selectedObject.GetComponent<ISelectable>().Unselected();
            selectedObject = null;
            return;
        }

        // Select a new different object 
        if (selectedObject != null && selectedObject != hoveredObject) {
            selectedObject.GetComponent<ISelectable>().Unselected();
        } 

        // Hovered becomes selected
        selectedObject = hoveredObject;
        selectedObject.GetComponent<ISelectable>().Selected();
    }

    private void MouseRightClick() {
        // false track selection means context menu is open
        if (enableSelection) {
            // Hovering a selectable object
            if (hoveredObject != null) {
                hoveredObject.GetComponent<ISelectable>().OnContextMenu(contextMenu);
                return;
            }

            // With selected object and 
            if (selectedObject != null) {
                selectedObject.GetComponent<ISelectable>().OnContextMenu(contextMenu);
                return;
            }
        }

        // Deselect object when not hovering a selectable object
        if (hoveredObject == null && selectedObject != null) {
            selectedObject.GetComponent<ISelectable>().Unselected();
            selectedObject = null;
            return;
        }

        // Context Menu is Open
        if (contextMenu.enabled) {
            contextMenu.DisableContextMenu();
            return;
        }
    }

    public void SetHoveredAsSelected() {
        selectedObject = hoveredObject;
        selectedObject.GetComponent<ISelectable>().Selected();
    }

    public void MoveSelectedObject() {
        if (selectedObject.tag == "Staff") {
            selectedObject.GetComponent<Staff>().MoveToGrid();
        }
    }
}
