using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Left clicking
 * Find selected and hovered
 */ 

public class SelectionManager : Singleton<SelectionManager>
{
    public static bool enableSelection = true;
    public static GameObject HoveredObject { get; private set; } = null;
    public static GameObject SelectedObject { get; private set; } = null;

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
                    //Debug.Log("Hovered: " + hitObject.name);
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


    // Select a selectable hovered object
    private void MouseLeftClick() {
        if (!enableSelection) return;
        // Do nothing without hovered object
        if (HoveredObject == null) return;

        // Deselect hovered selected object
        if (SelectedObject == HoveredObject) {
            SelectedObject.GetComponent<ISelectable>().Unselected();
            SelectedObject = null;
            return;
        }

        // Hovered becomes selected
        SetObjectAsSelected(HoveredObject);
    }

    public static void SetObjectAsSelected(GameObject go) {
        
        if (SelectedObject != null && SelectedObject != HoveredObject) {
            SelectedObject.GetComponent<ISelectable>().Unselected();
            SelectedObject = null;
        }

        ISelectable selectableObject = go.GetComponent<ISelectable>();
        if (selectableObject != null) {            
            SelectedObject = go;
            selectableObject.Selected();
        }
        Debug.Log(SelectedObject);
    }
}
