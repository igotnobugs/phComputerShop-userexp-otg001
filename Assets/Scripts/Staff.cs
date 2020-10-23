using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, ISelectable 
{
    public GameObject _hoveredBox;
    public GameObject _selectedBox;

    private MouseInput _mouseInput;
    private AStarMovement _movementControl;

    private void Awake() {
        _mouseInput = new MouseInput();
        _hoveredBox.SetActive(false);
        _selectedBox.SetActive(false);
    }

    private void OnEnable() {
        _mouseInput.Enable();
    }

    private void OnDisable() {
        _mouseInput.Disable();
    }

    private void Start() 
	{
        _movementControl = GetComponent<AStarMovement>();
        _mouseInput.Mouse.MouseLeftClick.performed += _ => MouseLeftClick();

        // Disable input at start
        _mouseInput.Disable();
    }


    private void Update() 
	{
        
    }

    private void MouseLeftClick() {
        //Do left click actions
        _movementControl.AttemptFindPath();
    }

    public void Selected() {
        _selectedBox.SetActive(true);
        _mouseInput.Enable();
    }

    public void Unselected() {
        _selectedBox.SetActive(false);
        _mouseInput.Disable();
    }

    public void Hovered() {
        _hoveredBox.SetActive(true);
    }

    public void Unhovered() {
        _hoveredBox.SetActive(false);
    }

    public void OnDestroy() {
        // Remove subscriber
        _mouseInput.Disable();
        _mouseInput.Mouse.MouseLeftClick.performed -= _ => MouseLeftClick();
    }
}
