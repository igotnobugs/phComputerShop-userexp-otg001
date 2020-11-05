using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/* Handles selection and movement
 * Contains the staffObject
 */

public class Staff : MonoBehaviour, ISelectable
{
    public StaffObject attributes;

    private MouseInput mouseInput;
    private AStarMovement movementControl;
    private Material mat;
    private bool isSelected = false;

    private void Awake() {
        mouseInput = new MouseInput();
        movementControl = GetComponent<AStarMovement>();
        mat = GetComponentInChildren<SpriteRenderer>().material;
    }

    private void OnDisable() {
        mouseInput.Disable();
    }

    private void Start() {     
        mouseInput.Mouse.MouseLeftClick.performed += _ => MouseLeftClick();      
        mouseInput.Disable();
    }

    private void MouseLeftClick() {
        // Do nothing with context menu open
        if (ContextMenuManager.ContextMenuOpen) return;

        // Do left click actions, mainly just moving
        MoveToGrid(GridCursor.GridPositionOffset);
    }

    public void MoveToGrid(Vector3 destination) {
        movementControl.AttemptFindPath(destination);
    }
  
    public void Selected() {
        isSelected = true;
        mat.SetInt("_AnimateOutline", 0);
        mat.SetFloat("_OutlineThickness", 2.0f);
        mouseInput.Enable();
    }

    public void Unselected() {
        isSelected = false;
        mat.SetInt("_AnimateOutline", 0);
        mat.SetFloat("_OutlineThickness", 0.0f);
        mouseInput.Disable();
    }

    public void Hovered() {
        if (isSelected) {
            mat.SetFloat("_OutlineThickness", 6.0f);
        }
        else {
            mat.SetInt("_AnimateOutline", 1);
            mat.SetFloat("_OutlineThickness", 4.0f);
        }
    }

    public void Unhovered() {
        if (isSelected) {
            mat.SetFloat("_OutlineThickness", 2.0f);
        }
        else {
            mat.SetInt("_AnimateOutline", 0);
            mat.SetFloat("_OutlineThickness", 0.0f);
        }
    }

    public void OnDestroy() {
        mouseInput.Disable();
        mouseInput.Mouse.MouseLeftClick.performed -= _ => MouseLeftClick();
    }
}
