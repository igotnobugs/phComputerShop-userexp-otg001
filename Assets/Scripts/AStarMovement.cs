using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Get path data from A* pathfinding

public class AStarMovement : MonoBehaviour 
{
    public float _movementSpeed = 5.0f;
    public float _closeEnough = 0.001f;

    private Tilemap _walkableMap;
    private Vector3 _destination;
    private Pathfinding2D _pathfinding;

    private int currentWaypointIndex = 0;

    private void Start() {
        _walkableMap = GameObject.FindGameObjectWithTag("WalkableTile").GetComponent<Tilemap>();
        _destination = transform.position;
        _pathfinding = GetComponent<Pathfinding2D>();
    }


    private void Update() {

    }

    private void FixedUpdate() {

        if (_pathfinding.m_Path == null) return;

        if (currentWaypointIndex >= _pathfinding.m_Path.Count) {
            return;
        }
     
        Vector3 currentNode = _pathfinding.m_Path[currentWaypointIndex].worldPosition;   
        if (Vector3.Distance(transform.position, currentNode) > _closeEnough) {
            // Move towards node
            transform.position = Vector3.MoveTowards(transform.position,
                                                     currentNode,
                                                     _movementSpeed * Time.deltaTime);
        } else {
            // Move to the next point if close enough
            // Set position
            transform.position = _pathfinding.m_Path[currentWaypointIndex].worldPosition;
            // Increase index
            currentWaypointIndex++;
        }
    }

    public bool AttemptFindPath() {
        Vector3Int gridPosition = GridCursor._gridPosition;

        // Check if tile is walkable
        if (_walkableMap.HasTile(gridPosition)) {
            currentWaypointIndex = 0;
            _destination = GridCursor._gridPositionOffset;
            _pathfinding.FindPath(transform.position, GridCursor._gridPositionOffset);
            return true;
        }

        return false;
    }
}
