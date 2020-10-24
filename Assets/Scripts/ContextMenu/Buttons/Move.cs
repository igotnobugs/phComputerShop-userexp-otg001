using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Only show when having a selected staff

public class Move : MonoBehaviour 
{

    private void Start() {
        
    }


    private void OnEnable() {
        if (SelectionManager.selectedObject != null) {
            if (SelectionManager.selectedObject.tag == "Staff") {
                gameObject.SetActive(true);
                return;
            }        
        }

        gameObject.SetActive(false);
        
    }
}
