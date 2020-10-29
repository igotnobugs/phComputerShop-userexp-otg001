using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// if furntireu 

public class Interact : MonoBehaviour, IContextButton 
{
    private ContextMenuManager contextManager;
    private Furniture contextObject;

    private void Awake() {
        contextManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ContextMenuManager>();
    }

    public void Enable() {
        if (SelectionManager.SelectedObject != null) {
            if (SelectionManager.SelectedObject.tag == "Staff" &&
                SelectionManager.HoveredObject.tag == "Furniture") {
                contextObject = SelectionManager.HoveredObject.GetComponent<Furniture>();
                gameObject.SetActive(true);
                return;
            }
        }
        gameObject.SetActive(false);
    }

    public void OnClick() {
        Staff staff = SelectionManager.SelectedObject.GetComponent<Staff>();
        Vector3 worldPosition = transform.parent.position - contextObject.interactPosition.position;
        staff.MoveToGrid(GridCursor.WorldToGrid(worldPosition));
        contextManager.DisableContextMenu();
    }

    private void Update() {
        
    }

}
