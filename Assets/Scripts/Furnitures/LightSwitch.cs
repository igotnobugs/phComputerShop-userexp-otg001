using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Furniture, ISelectable 
{
    // The hovered furniture
    private GameObject contextObject;

    private void Start() {

    }


    private void Update() {

    }

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
