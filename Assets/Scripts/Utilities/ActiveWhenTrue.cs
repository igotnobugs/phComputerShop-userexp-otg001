using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWhenTrue : MonoBehaviour 
{
    public bool withHoveredObject;
    public bool withSelectedObject;

    private void Start() {

    }

    private void OnEnable() {
        if (withHoveredObject) {
            if (SelectionManager.hoveredObject != null) {
                gameObject.SetActive(true);
            }
            else {
                gameObject.SetActive(false);
            }
        }

        if (withSelectedObject) {
            if (SelectionManager.selectedObject != null) {
                gameObject.SetActive(true);
            }
            else {
                gameObject.SetActive(false);
            }
        }
    }
}
