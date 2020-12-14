using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

// Get path data from A* pathfinding

public class AStarMovement : MonoBehaviour 
{
    public float moveSpeed = 5.0f;
    public float closeEnough = 0.001f;
    public Animator animator;
    public AudioManager personalAudio;

    public Action onCompletePath;


    private Pathfinding2D pathfinding;

    private void Awake() {
        pathfinding = GetComponent<Pathfinding2D>();
        personalAudio = GetComponent<PersonalAudio>();
    }

    private void LateUpdate() {
        if (pathfinding.myPath == null)
        {
            if (personalAudio.GetAudioSource("Walking").isPlaying)
                personalAudio.Stop("Walking");

            if (animator != null)
            {
                animator.SetFloat("Speed", 0);
                animator.SetBool("WalkingUp", false);
            }    
            return;
        }

        if (pathfinding.myPath.Count > 0) {
            Vector3 currentNode = pathfinding.myPath[0].worldPosition;
            if (Vector3.Distance(transform.position, currentNode) > closeEnough) {
                if (!personalAudio.GetAudioSource("Walking").isPlaying)
                    personalAudio.Play("Walking");

                if (animator != null) {
                    if (transform.position.y < currentNode.y)
                        animator.SetBool("WalkingUp", true);
                    else if(transform.position.y > currentNode.y) 
                        animator.SetBool("WalkingUp", false);

                    animator.SetFloat("Speed", moveSpeed);
                }

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
            onCompletePath?.Invoke();
        }
    }

    public bool AttemptFindPath(Vector3 destination, Action onCompleteFunc = null) {  
        if (pathfinding.FindPath(transform.position, destination)) {
            //Debug.Log("Path found at " + destination);
            if (onCompleteFunc != null) {
                onCompletePath = onCompleteFunc;
            }
            return true;
        }
        //Debug.Log("Path not found at " + destination);
        return false;
    }
}
