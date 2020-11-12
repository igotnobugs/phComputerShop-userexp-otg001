using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Get path data from A* pathfinding

public class AStarMovement : MonoBehaviour 
{
    public float moveSpeed = 5.0f;
    public float closeEnough = 0.001f;

    private Pathfinding2D pathfinding;

    private void Awake() {
        pathfinding = GetComponent<Pathfinding2D>();
    }

    private void LateUpdate() {
        if (pathfinding.myPath == null) return;

        if (pathfinding.myPath.Count > 0) {
            Vector3 currentNode = pathfinding.myPath[0].worldPosition;
            if (Vector3.Distance(transform.position, currentNode) > closeEnough) {
                // Move towards node
                transform.position = Vector3.MoveTowards(transform.position,
                                                         currentNode,
                                                         moveSpeed * Time.deltaTime);
            }
            else {              
                transform.position = pathfinding.myPath[0].worldPosition; // Set position             
                pathfinding.myPath.RemoveAt(0); // Remove node
            }
        } else {
            //Debug.Log("Path done!");
            pathfinding.myPath = null;
        }
    }

    public bool AttemptFindPath(Vector3 destination) {  
        if (pathfinding.FindPath(transform.position, destination)) {
            //Debug.Log("Path found at " + destination);
            return true;
        }
        //Debug.Log("Path not found at " + destination);
        return false;
    }
}
