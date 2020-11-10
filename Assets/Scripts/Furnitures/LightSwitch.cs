using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Light Switch
 * 
 * WIP - Actually make it interact with the lights
 */

public class LightSwitch : Furniture, ISelectable 
{
    // The hovered furniture
    private GameObject contextObject;

    public void Hovered() {
        //throw new System.NotImplementedException();
    }

    public void Selected() {
        //throw new System.NotImplementedException();
    }

    public void Unhovered() {
        //throw new System.NotImplementedException();
    }

    public void Unselected() {
        //throw new System.NotImplementedException();
    }

    public void OnContextMenu(ContextMenu menu) {
        //throw new System.NotImplementedException();
    }
}
