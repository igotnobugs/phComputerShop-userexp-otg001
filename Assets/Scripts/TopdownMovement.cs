using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TopdownMovement : MonoBehaviour, ISelectable 
{  
    public float _movementSpeed = 5.0f;
    public GameObject _hoveredBox;
    public GameObject _selectedBox;

    private Tilemap _walkableMap;
    private MouseInput _mouseInput;
    private bool _isSelected = false;
    private Vector3 _destination;
    private Pathfinding2D _pathfinding;

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

    private void Start() {
        _walkableMap = GameObject.FindGameObjectWithTag("WalkableTile").GetComponent<Tilemap>();
        _destination = transform.position;

        _mouseInput.Mouse.MouseLeftClick.performed += _ => MouseClick();

        if (!_isSelected) {
            _mouseInput.Disable();
        }

        _pathfinding = GetComponent<Pathfinding2D>();
    }


    private void Update() 
	{
        
    }

    int currentWaypointIndex = 0;

    private void LateUpdate() {
        // Pathfind
        if (_pathfinding.m_Path == null) return;

        if (currentWaypointIndex >= _pathfinding.m_Path.Count) {
            return;
        }

        Vector3 currentNode = _pathfinding.m_Path[currentWaypointIndex].worldPosition;
        // Move towards node
        if (Vector3.Distance(transform.position, currentNode) > 0.02f) {
            transform.position = Vector3.MoveTowards(transform.position,
                                                     currentNode,
                                                     _movementSpeed * Time.deltaTime);
        }

        //move to the next point if close enough
        if (Vector3.Distance(_pathfinding.m_Path[currentWaypointIndex].worldPosition,
                            transform.position) < 0.2f) {
            currentWaypointIndex++;
        }



    }

    private void MouseClick() {
        Vector3Int gridPosition = GridCursor._gridPosition;
        if (_walkableMap.HasTile(gridPosition)) {
            currentWaypointIndex = 0;
            _destination = GridCursor._gridPositionOffset;        
            _pathfinding.FindPath(transform.position, GridCursor._gridPositionOffset);
        }     
    }

    public void Selected() {
        _isSelected = true;
        _selectedBox.SetActive(true);
        _mouseInput.Enable();
    }

    public void Unselected() {
        _isSelected = false;
        _selectedBox.SetActive(false);
        _mouseInput.Disable();
    }

    public void Hovered() {      
        _hoveredBox.SetActive(true);
    }

    public void Unhovered() {
        _hoveredBox.SetActive(false);
    }
}
