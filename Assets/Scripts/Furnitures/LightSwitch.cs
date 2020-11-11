using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Light Switch
 * 
 * WIP - Actually make it interact with the lights
 */

public class LightSwitch : Furniture, ISelectable 
{

    private Material mat;
    private bool isSelected = false;

    // The hovered furniture
    private GameObject contextObject;

    private void Awake() {
        mat = GetComponentInChildren<SpriteRenderer>().material;
    }


    public void Selected() {
        isSelected = true;
        mat.SetInt("_AnimateOutline", 0);
        mat.SetFloat("_OutlineThickness", 2.0f);
    }

    public void Unselected() {
        isSelected = false;
        mat.SetInt("_AnimateOutline", 0);
        mat.SetFloat("_OutlineThickness", 0.0f);
    }

    public void Hovered() {
        if (isSelected) {
            mat.SetFloat("_OutlineThickness", 4.0f);
        }
        else {
            mat.SetInt("_AnimateOutline", 1);
            mat.SetFloat("_OutlineThickness", 3.0f);
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
}
