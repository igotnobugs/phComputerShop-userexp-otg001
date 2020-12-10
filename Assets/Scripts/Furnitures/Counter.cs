using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : Furniture 
{
    public Transform lineTransfrom; //Where customers go
    public Staff mannedByWho;

    public virtual void InteractAsCustomer(NPC interactor, Action onComplete = null) {
        Vector3 interactDestination = GridCursor.WorldToGrid(lineTransfrom.position);
        interactor.MoveToGrid(interactDestination, () => {
            onComplete?.Invoke();
            InteractedAsCustomer();
        });
    }

    protected override void Interacted(NPC npc = null) {
        if (npc != null && npc as Staff) {
            mannedByWho = npc as Staff;
        }
        isOccupied = true;
    }

    public void InteractedAsCustomer() {

    }

}
