using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static bool enableSelection = true;
    public static GameObject HoveredObject { get; private set; }
    public static GameObject SelectedObject { get; private set; }

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
        mouseInput.Mouse.MouseLeftClick.performed += _ => MouseLeftClick();
        //mouseInput.Mouse.MouseRightClick.performed += _ => MouseRightClick();
    }

    private void Update() {
        TrackSelection();
    }

    private void TrackSelection() {
        if (!enableSelection) return;

        RaycastHit2D hit = Physics2D.Raycast(GridCursor.MousePositionWorld, Vector2.zero);

        if (hit.collider != null) {
            GameObject hitObject = hit.collider.gameObject;
            ISelectable hitSelectable = hitObject.GetComponent<ISelectable>();
            if (hitSelectable != null) {
                //if hit is not the same as recently hovered
                if (HoveredObject != hitObject) {
                    Debug.Log("Hovered: " + hitObject.name);
                    HoveredObject = hitObject;
                    hitSelectable.Hovered();
                }
            }
        }
        else {
            // If something was hovered and remove it
            if (HoveredObject != null) {
                HoveredObject.GetComponent<ISelectable>().Unhovered();
                HoveredObject = null;
            }          
        }
    }

    // Select - selectable object
    private void MouseLeftClick() {
        // Do nothing without hovered object
        if (HoveredObject == null) return;
        //if (ContextMenuManager.ContextMenuOpen) return;

        // Deselect hovered selected object
        if (SelectedObject == null && SelectedObject == HoveredObject) {
            SelectedObject.GetComponent<ISelectable>().Unselected();
            SelectedObject = null;
            return;
        }

        // Select a new different object 
        //if (SelectedObject != null && SelectedObject != HoveredObject) {
        //    SelectedObject.GetComponent<ISelectable>().Unselected();
        //}

        // Hovered becomes selected
        SetObjectAsSelected(HoveredObject);
    }

    public static void SetObjectAsSelected(GameObject go) {
        if (SelectedObject != null && SelectedObject != HoveredObject) {
            SelectedObject.GetComponent<ISelectable>().Unselected();
        }

        ISelectable selectableObject = go.GetComponent<ISelectable>();
        if (selectableObject != null) {            
            SelectedObject = go;
            selectableObject.Selected();
        }     
    }
}
