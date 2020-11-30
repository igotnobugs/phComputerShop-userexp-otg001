using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Light Switch
 * 
 * WIP - Actually make it interact with the lights
 */

public class LightSwitch : Furniture
{
    // The hovered furniture
    private GameObject contextObject;

    protected override void Interacted() {
        Debug.Log("Light switch interacted");
    }
}
