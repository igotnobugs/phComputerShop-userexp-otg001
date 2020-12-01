using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* WIP
 * 
 * Make pupups appear next to target UI
 */

public class PopUpManager : MonoBehaviour 
{
    [SerializeField] private PopUp popUpPrefab = null;

    public Canvas gameUI;

    public PopUp CreatePopUp(Vector2 pivot, Vector2 anchorPosition) {
        PopUp newPopup = Instantiate(popUpPrefab, gameUI.transform);
        newPopup.MoveTo(pivot, anchorPosition);
        return newPopup;
    }
}
