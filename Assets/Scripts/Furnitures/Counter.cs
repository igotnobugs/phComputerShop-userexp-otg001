using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : Furniture 
{
    public Transform lineTransfrom; //Where customers go

    public virtual void InteractAsCustomer(NPC interactor, Action onComplete = null) {
        Vector3 interactDestination = GridCursor.WorldToGrid(lineTransfrom.position);
        interactor.MoveToGrid(interactDestination, () => {
            onComplete?.Invoke();
            Interacted();
        });
    }

    protected override void Interacted() {

    }
}
