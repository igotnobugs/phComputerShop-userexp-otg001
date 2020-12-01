using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

/* For interactable furnitures
 * Set the position where the interactor have to go to
 * 
 * WIP - Must check when the interactor has reached the destination
 */

public class Furniture : MonoBehaviour, ISelectable, IPointerEnterHandler, IPointerExitHandler  
{
    public Transform interactTransform; //Staff usually goes here
    protected Material mat;
    protected bool isSelected = false;

    public bool isOccupied = false;

    protected virtual void Awake() {
        mat = GetComponentInChildren<SpriteRenderer>().material;
    }


    public virtual void Interact(NPC interactor, Action onComplete = null) {
        Vector3 interactDestination = GridCursor.WorldToGrid(interactTransform.position);
        interactor.MoveToGrid(interactDestination, () => {
            onComplete?.Invoke();
            Interacted();          
            });
    }

    public virtual void Selected() {
        isSelected = true;
        mat.SetInt("_AnimateOutline", 0);
        mat.SetFloat("_OutlineThickness", 2.0f);
    }

    public virtual void Unselected() {
        isSelected = false;
        mat.SetInt("_AnimateOutline", 0);
        mat.SetFloat("_OutlineThickness", 0.0f);
    }

    public virtual void Hovered() {
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

    protected virtual void Interacted() {
        
    }

    public void SetUnoccupied() {
        isOccupied = false;
    }
}
