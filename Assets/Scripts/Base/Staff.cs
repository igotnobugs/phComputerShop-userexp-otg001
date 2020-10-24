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
        hoveredBox.SetActive(false);
        selectedBox.SetActive(false);
    }

    private void OnEnable() {
        mouseInput.Enable();
    }

    private void OnDisable() {
        mouseInput.Disable();
    }

    private void Start() 
	{
        movementControl = GetComponent<AStarMovement>();
        mouseInput.Mouse.MouseLeftClick.performed += _ => MouseLeftClick();

        // Disable input at start
        mouseInput.Disable();
    }


    private void Update() 
	{
        
    }

    private void MouseLeftClick() {
        // Do nothing with context menu open
        if (ContextMenu.contextMenuOpen) return;

        // Do left click actions
        MoveToGrid(GridCursor.gridPosition);
    }

    public void MoveToGrid() {
        movementControl.AttemptFindPath(GridCursor.gridPosition);
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
        menu.MoveTo(GridCursor.gridPositionWorld);
    }

    public void OnDestroy() {
        // Remove subscriber
        mouseInput.Disable();
        mouseInput.Mouse.MouseLeftClick.performed -= _ => MouseLeftClick();
    }


}
