using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWhenTrue : MonoBehaviour 
{
    public bool withHoveredObject;
    public bool withSelectedObject;

    private void OnEnable() {
        if (withHoveredObject) {
            if (SelectionManager.HoveredObject != null) {
                gameObject.SetActive(true);
            }
            else {
                gameObject.SetActive(false);
            }
        }

        if (withSelectedObject) {
            if (SelectionManager.SelectedObject != null) {
                gameObject.SetActive(true);
            }
            else {
                gameObject.SetActive(false);
            }
        }
    }
}
