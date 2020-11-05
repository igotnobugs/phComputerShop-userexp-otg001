using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* For interactable furnitures
 * Set the position where the interactor have to go to
 * 
 * WIP - Must check when the interactor has reached the destination
 */

public class Furniture : MonoBehaviour 
{
    public Transform interactTransform;
    public Vector3 interactDestination;
    //public GameObject interactor;

    public void Start() {
        interactDestination = GridCursor.WorldToGrid(interactTransform.position);
    }

    public virtual void Update() {
        //if (interactor != null) {
        //    if (Vector3.Distance(interactor.transform.position, interactDestination) < 0.01) {
        //        Debug.Log("Reached");
        //    }
        //}
    }

}
