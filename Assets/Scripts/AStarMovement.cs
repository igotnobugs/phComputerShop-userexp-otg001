using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Get path data from A* pathfinding

public class AStarMovement : MonoBehaviour 
{
    public float movementSpeed = 5.0f;
    public float closeEnough = 0.001f;

    private Tilemap walkableTilemap;
    private Vector3 destination;
    private Pathfinding2D pathfinding;

    private int currentWaypointIndex = 0;

    private void Start() {
        walkableTilemap = GameObject.FindGameObjectWithTag("WalkableTile").GetComponent<Tilemap>();
        destination = transform.position;
        pathfinding = GetComponent<Pathfinding2D>();
    }


    private void Update() {

    }

    private void FixedUpdate() {

        if (pathfinding.myPath == null) return;

        if (currentWaypointIndex >= pathfinding.myPath.Count) {
            return;
        }
     
        Vector3 currentNode = pathfinding.myPath[currentWaypointIndex].worldPosition;   
        if (Vector3.Distance(transform.position, currentNode) > closeEnough) {
            // Move towards node
            transform.position = Vector3.MoveTowards(transform.position,
                                                     currentNode,
                                                     movementSpeed * Time.deltaTime);
        } else {
            // Move to the next point if close enough
            // Set position
            transform.position = pathfinding.myPath[currentWaypointIndex].worldPosition;
            // Increase index
            currentWaypointIndex++;
        }
    }

    public bool AttemptFindPath(Vector3Int gridPosition) {
        // Check if tile is walkable
        if (walkableTilemap.HasTile(gridPosition)) {
            currentWaypointIndex = 0;
            destination = GridCursor.gridPositionOffset;
            pathfinding.FindPath(transform.position, GridCursor.gridPositionOffset);
            return true;
        }

        return false;
    }
}
