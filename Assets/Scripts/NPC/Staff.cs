using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

/* Handles selection and movement
 * Contains the staffObject
 */

public class Staff : NPC, ISelectable, IPointerEnterHandler, IPointerExitHandler 
{
    public StaffObject attributes;
    [SerializeField] public Furniture mannedFurniture = null;

    private MouseInput mouseInput;
    
    private bool isSelected = false;
    private bool allowSelection = false;

    public GameObject energyCounter;

    private AudioManager audioManager;

    protected override void Awake() {
        base.Awake();
        mouseInput = new MouseInput();       

    }

    private void OnDisable() {
        mouseInput.Disable();
    }

    private void Start() {
        mouseInput.Mouse.MouseLeftClick.performed += _ => MouseLeftClick();
        mouseInput.Disable();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void MouseLeftClick() {
        if (ContextMenuManager.ContextMenuOpen) return;
        if (GridCursor.IsDisabled()) return;

        MoveToGrid(GridCursor.GridPositionOffset);
    }

    public override void MoveToGrid(Vector3 destination, Action onCompleteFunc = null) {
        if (mannedFurniture != null) {
            mannedFurniture.SetUnoccupied();
            mannedFurniture = null;
        }
        base.MoveToGrid(destination, onCompleteFunc);      
    }

    public void Selected() {
        if (!allowSelection) return;
        isSelected = true;
        mat.SetInt("_AnimateOutline", 0);
        mat.SetFloat("_OutlineThickness", 2.0f);
        mouseInput.Enable();
        audioManager.Play("CharacterSelect");
    }

    public void Unselected() {
        if (!allowSelection) return;
        isSelected = false;
        mat.SetInt("_AnimateOutline", 0);
        mat.SetFloat("_OutlineThickness", 0.0f);
        mouseInput.Disable();
    }

    public void Hovered() {
        if (!allowSelection) return;
        if (isSelected) {
            mat.SetFloat("_OutlineThickness", 4.0f);           
        }
        else {
            mat.SetInt("_AnimateOutline", 1);
            mat.SetFloat("_OutlineThickness", 3.0f);           
        }
        energyCounter.SetActive(true);
    }

    public void Unhovered() {
        if (!allowSelection) return;
        if (isSelected) {
            mat.SetFloat("_OutlineThickness", 2.0f);
        }
        else {
            mat.SetInt("_AnimateOutline", 0);
            mat.SetFloat("_OutlineThickness", 0.0f);
        }
        energyCounter.SetActive(false);
    }

    public void EnableSelection() {
        allowSelection = true;
    }

    public void DisableSelection() {
        allowSelection = false;
    }

    public void OnDestroy() {
        mouseInput.Disable();
        mouseInput.Mouse.MouseLeftClick.performed -= _ => MouseLeftClick();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        SelectionManager.HoveredObject = gameObject;
        Hovered();
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (SelectionManager.HoveredObject == gameObject) {
            SelectionManager.HoveredObject = null;
        }
        Unhovered();
    }
}
