using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NPC : MonoBehaviour 
{
    protected AStarMovement movementControl;
    protected Material mat;
    protected SceneAudioManager audioManager;
    protected Furniture furnitureUsing;

    public float clumsiness = 0;

    public delegate void NPCEvent();
    public event NPCEvent Moving;

    protected virtual void Awake() {
        movementControl = GetComponent<AStarMovement>();
        mat = GetComponentInChildren<SpriteRenderer>().material;     
    }

    protected virtual void Start() {
        audioManager = FindObjectOfType<SceneAudioManager>();
    }

    public virtual void MoveToGrid(Vector3 destination, Action onCompleteFunc = null) {
        bool pathFound = movementControl.AttemptFindPath(destination, onCompleteFunc);

        if (pathFound) {
            Moving?.Invoke();
            if (furnitureUsing != null) {
                furnitureUsing.SetUnoccupied();
                furnitureUsing = null;
            }
        }
    }

    public virtual void Interact(Furniture furniture, Action onReachFunc = null) {   
        MoveToGrid(furniture.interactTransform.position, () => {
            furniture.OccupiedBy(this);
            furnitureUsing = furniture;
            onReachFunc?.Invoke();
        });
    }

}
