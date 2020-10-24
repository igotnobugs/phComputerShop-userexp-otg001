using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Only show when hovering a selectable

public class Select : MonoBehaviour 
{

    private void Start() {
            
    }


    private void OnEnable() {
        if (SelectionManager.hoveredObject != null) {
            gameObject.SetActive(true);
            return;
        }
        
        gameObject.SetActive(false);    
    }
}
