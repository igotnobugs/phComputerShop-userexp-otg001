using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Furniture : MonoBehaviour, ISelectable, IPointerEnterHandler, IPointerExitHandler  
{
    [Header("Furniture Settings")]
    public Transform interactTransform; 
    protected Material mat;
    protected bool isSelected = false;
    public bool allowBrokenAfterUse = false;
    public bool isOccupied = false;
    public NPC user;
    public bool isBroken = false;

    [Header("Outline Settings")]
    public Color SelectedColor;
    public Color OccupiedColor;

    protected virtual void Awake() {
        mat = GetComponentInChildren<SpriteRenderer>().material;
    }

    public virtual void Selected() {
        isSelected = true;

        if (!isOccupied) {
            mat.SetColor("_OutlineColor", SelectedColor);
        }

        mat.SetInt("_AnimateOutline", 0);
        mat.SetFloat("_OutlineThickness", 2.0f);
    }

    public virtual void Unselected() {
        isSelected = false;
        mat.SetInt("_AnimateOutline", 0);
        mat.SetFloat("_OutlineThickness", 0.0f);
    }

    public virtual void Hovered() {
        if (!isOccupied) {
            mat.SetColor("_OutlineColor", SelectedColor);
        }

        if (isSelected) {
            mat.SetFloat("_OutlineThickness", 4.0f);
        }
        else {
            mat.SetInt("_AnimateOutline", 1);
            mat.SetFloat("_OutlineThickness", 3.0f);
        }
    }

    public virtual void Unhovered() {
        if (isSelected) {
            mat.SetFloat("_OutlineThickness", 2.0f);
        }
        else {
            mat.SetInt("_AnimateOutline", 0);
            mat.SetFloat("_OutlineThickness", 0.0f);
        }
    }

    public virtual void EnableSelection() {
        throw new NotImplementedException();
    }

    public virtual void DisableSelection() {
        throw new NotImplementedException();
    }

    public virtual void OnPointerEnter(PointerEventData eventData) {
        SelectionManager.HoveredObject = gameObject;
        Hovered();
    }

    public virtual void OnPointerExit(PointerEventData eventData) {
        if (SelectionManager.HoveredObject == gameObject) {
            SelectionManager.HoveredObject = null;
        }
        Unhovered();
    }

    public void SetUnoccupied() {
        Unoccupied();      
    }

    protected virtual void Occupied() {
        isOccupied = true;
        mat.SetColor("_OutlineColor", OccupiedColor);
        mat.SetFloat("_OutlineThickness", 2.0f);
    }

    protected virtual void Unoccupied() {
        isOccupied = false;
        mat.SetFloat("_OutlineThickness", 0.0f);
    }

    public bool CanBeUsed() {
        return !isOccupied && !isBroken;
    }

    public virtual void OccupiedBy(NPC npc) {
        
        Occupied();
        user = npc;
        user.Moving += UserMoved;
    }

    protected virtual void UserMoved() {
        user.Moving -= UserMoved;
        user = null;
        Unoccupied();
    }

    public virtual void SetBroken() {
        isBroken = true;
    }

    public virtual void SetFixed() {
        isBroken = false;
    }


}
