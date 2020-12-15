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
    private bool isSelected = false;
    public bool AllowSelection { set; get; }

    [Header("Staff Settings")]
    public StaffObject attributes;
    public GameObject energyCounter;
    public GameObject[] energyIcon;
    public SelectionOutline outline;
    public bool isBusy = false;

    protected MouseInput mouseInput;

    protected override void Awake() {
        base.Awake();
        mouseInput = new MouseInput();
        mouseInput.Mouse.MouseLeftClick.performed += _ => MouseLeftClick();
        mouseInput.Disable();
    }

    private void OnDisable() {
        mouseInput.Disable();
    }

    private void MouseLeftClick() {
        if (ContextMenuManager.ContextMenuOpen) return;
        if (GridCursor.IsDisabled()) return;
        if (isBusy) return;

        MoveToGrid(GridCursor.GridPositionOffset);
    }

    public void Selected() {
        if (!AllowSelection) return;
        isSelected = true;
        outline.Selected();

        mouseInput.Enable();
        audioManager.Play("CharacterSelect");
    }

    public void Unselected() {
        if (!AllowSelection) return;
        isSelected = false;
        outline.Unselected();
        mouseInput.Disable();
    }

    public void Hovered() {
        if (!AllowSelection) return;
        
        if (!isSelected) {
            outline.Hovered();
        }
        CheckStatus();
        energyCounter.SetActive(true);
    }

    public void Unhovered() {
        if (!AllowSelection) return;

        if (!isSelected) {
            outline.Unhovered();
        }
        energyCounter.SetActive(false);
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

    public void ReplenishEnergy() {
        energyIcon[0].gameObject.SetActive(true);
        energyIcon[1].gameObject.SetActive(true);
        energyIcon[2].gameObject.SetActive(true);
        attributes.energy = 100;
        isBusy = false;
    }

    public void OnDestroy() {
        mouseInput.Disable();
        mouseInput.Mouse.MouseLeftClick.performed -= _ => MouseLeftClick();
    }

    private void CheckStatus() {
        if (attributes.energy < 70) {
            energyIcon[2].gameObject.SetActive(false);
        } else if (attributes.energy < 40) {
            energyIcon[1].gameObject.SetActive(false);
        } else if (attributes.energy < 10) {
            energyIcon[0].gameObject.SetActive(false);
        }
    }

    public void StaffFix(Furniture brokenFurniture) {
        StartCoroutine(Fixing(brokenFurniture));
        isBusy = true;
    }

    public IEnumerator Fixing(Furniture furniture) {

        yield return new WaitForSeconds(2.0f);
        furniture.SetFixed();

        attributes.DrainEnergyDefault();

        isBusy = false;
        yield break;
    }
}
