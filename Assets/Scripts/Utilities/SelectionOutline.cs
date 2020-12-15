using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionOutline : MonoBehaviour {
    protected Material mat;

    //[Header("Outline Settings")]
    //public Color SelectedColor;
    //public Color OccupiedColor;

    protected virtual void Awake() {
        mat = GetComponentInChildren<SpriteRenderer>().material;
    }

    public virtual void Selected() {
        mat.SetInt("_AnimateOutline", 0);
        mat.SetFloat("_OutlineThickness", 2.0f);
    }

    public virtual void Unselected() {
        mat.SetInt("_AnimateOutline", 0);
        mat.SetFloat("_OutlineThickness", 0.0f);
    }

    public virtual void Hovered() {
        mat.SetInt("_AnimateOutline", 1);
        mat.SetFloat("_OutlineThickness", 3.0f);
    }

    public virtual void Unhovered() {
        mat.SetInt("_AnimateOutline", 0);
        mat.SetFloat("_OutlineThickness", 0.0f);
    }
}
