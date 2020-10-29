using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Only show when having a moveable selected object

public class Move : MonoBehaviour, IContextButton
{
    private ContextMenuManager contextManager;

    private void Awake() {
        contextManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ContextMenuManager>();
    }

    public void Enable() {
        if (SelectionManager.SelectedObject != null && 
            SelectionManager.HoveredObject == null) {
            if (SelectionManager.SelectedObject.tag == "Staff") {
                gameObject.SetActive(true);
                return;
            }
        }
        gameObject.SetActive(false);
    }

    public void OnClick() {
        SelectionManager.SelectedObject.GetComponent<Staff>().MoveToGrid();
        contextManager.DisableContextMenu();
    }

    private void OnDisable() {

    }
}
