using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Only show when hovering a selectable

public class Select : MonoBehaviour, IContextButton 
{    
    private ContextMenuManager contextManager;
    private GameObject contextObject;

    private void Awake() {
        contextManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ContextMenuManager>();
    }

    public void Enable() {
        contextObject = null;

        if (SelectionManager.HoveredObject != null &&
            SelectionManager.HoveredObject.tag == "Staff") {
            contextObject = SelectionManager.HoveredObject;
            gameObject.SetActive(true);
            return;
        }

        gameObject.SetActive(false);
    }

    public void OnClick() {
        SelectionManager.SetObjectAsSelected(contextObject);
        contextManager.DisableContextMenu();
    }

    private void OnEnable() {
  
    }
}
