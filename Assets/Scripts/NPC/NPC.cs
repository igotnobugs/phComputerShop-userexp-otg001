using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NPC : MonoBehaviour 
{
    protected AStarMovement movementControl;
    protected Material mat;

    protected virtual void Awake() {
        movementControl = GetComponent<AStarMovement>();
        mat = GetComponentInChildren<SpriteRenderer>().material;
    }

    public virtual void MoveToGrid(Vector3 destination, Action onCompleteFunc = null) {
        movementControl.AttemptFindPath(destination, onCompleteFunc);
    }
}
