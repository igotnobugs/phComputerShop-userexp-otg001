using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private GameObject _hoveredObject;
    private GameObject _selectedObject;
    private MouseInput _mouseInput;

    private void Awake() {
        _mouseInput = new MouseInput();
    }

    private void OnEnable() {
        _mouseInput.Enable();
    }

    private void OnDisable() {
        _mouseInput.Disable();
    }

    private void Start() 
	{
        _mouseInput.Mouse.MouseRightClick.performed += _ => MouseRightClick();
    }

    private void Update() 
	{
        // Raycast from camera to mouse position
        Vector2 raycastPos = GridCursor._mousePositionWorld;
        RaycastHit2D hit = Physics2D.Raycast(raycastPos, Vector2.zero);
       
        if (hit.collider != null) {
            // if hit is a selecteable
            if (hit.collider.gameObject.GetComponent<ISelectable>() != null) {

                //if hit is not the same as recently hovered
                if (_hoveredObject != hit.collider.gameObject) {
                    Debug.Log(hit.collider.gameObject.name);
                    _hoveredObject = hit.transform.gameObject;
                    _hoveredObject.GetComponent<ISelectable>().Hovered();
                }
            }
        } else {
            if (_hoveredObject != null) {
                _hoveredObject.GetComponent<ISelectable>().Unhovered();
            }
            _hoveredObject = null;
        }
    }

    private void MouseRightClick() {
        // Without a hovered object with a selected object - Deselect
        if (_hoveredObject == null && _selectedObject != null) {
            _selectedObject.GetComponent<ISelectable>().Unselected();
            _selectedObject = null;
            return;
        }

        // No hovered - do Nothing
        if (_hoveredObject == null) return;

        // Hovering selected object
        if (_selectedObject != null && _selectedObject == _hoveredObject) {
            _selectedObject.GetComponent<ISelectable>().Unselected();
            _selectedObject = null;
            return;
        }

        // Selecting a new different object
        if (_selectedObject != null && _selectedObject != _hoveredObject) {
            _selectedObject.GetComponent<ISelectable>().Unselected();
        } 

        // Hovered becomes selected
        _selectedObject = _hoveredObject;
        _selectedObject.GetComponent<ISelectable>().Selected();

    }
}
