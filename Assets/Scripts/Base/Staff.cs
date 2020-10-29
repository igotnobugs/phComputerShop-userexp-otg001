using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, ISelectable 
{
    public GameObject hoveredBox;
    public GameObject selectedBox;

    private MouseInput mouseInput;
    private AStarMovement movementControl;

    private void Awake() {
        mouseInput = new MouseInput();
        movementControl = GetComponent<AStarMovement>();
        hoveredBox.SetActive(false);
        selectedBox.SetActive(false);
    }

    private void OnDisable() {
        mouseInput.Disable();
    }

    private void Start() 
	{     
        mouseInput.Mouse.MouseLeftClick.performed += _ => MouseLeftClick();      
        mouseInput.Disable(); // Disable input at start
    }


    private void Update() 
	{
        
    }

    private void MouseLeftClick() {
        // Do nothing with context menu open
        if (ContextMenuManager.ContextMenuOpen) return;

        // Do left click actions
        MoveToGrid(GridCursor.GridPosition);
    }

    public void MoveToGrid() {
        movementControl.AttemptFindPath(GridCursor.GridPosition);
    }

    public void MoveToGrid(Vector3Int gridPosition) {
        movementControl.AttemptFindPath(gridPosition);
    }

    public void Selected() {
        selectedBox.SetActive(true);
        mouseInput.Enable();
    }

    public void Unselected() {
        selectedBox.SetActive(false);
        mouseInput.Disable();
    }

    public void Hovered() {
        hoveredBox.SetActive(true);
    }

    public void Unhovered() {
        hoveredBox.SetActive(false);
    }

    public void OnContextMenu(ContextMenu menu) {
        menu.MoveTo(GridCursor.GridPositionWorld);
    }

    public void OnDestroy() {
        // Remove subscriber
        mouseInput.Disable();
        mouseInput.Mouse.MouseLeftClick.performed -= _ => MouseLeftClick();
    }


}
