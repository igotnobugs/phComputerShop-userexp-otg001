using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Need to fix this SPAGHETTI CODE!
 * 
 * 
 */

public class Counter : Furniture 
{
    public int amountCustomerLining;

    public Transform[] line;
    public Customer customerAtCounter;

    public delegate void CounterEvent();
    public event CounterEvent CustomerDonePaying;

    public float efficiency;

    public void Reset() {
        amountCustomerLining = 0;
    }

    public void LineUp(Customer interactor) {

        interactor.numberInLine = amountCustomerLining;

        Vector3 interactDestination = GridCursor.WorldToGrid(line[amountCustomerLining].position);     

        if (interactor.numberInLine <= 0) {
            interactor.MoveToGrid(interactDestination, () => {
                customerAtCounter = interactor;
                customerAtCounter.TalkAtCounterAction?.Invoke();
            });
        } else {
            interactor.MoveToGrid(interactDestination);
        }
        amountCustomerLining++;
    }

    public override void OccupiedBy(NPC npc) {
        base.OccupiedBy(npc);
        efficiency = (npc as Staff).attributes.social * ((npc as Staff).attributes.energy / 100.0f);
    }

    //Called by the customer
    public void CustomerDone() {
        amountCustomerLining--;
        CustomerDonePaying?.Invoke();
        CalculateEfficiencyAgain();
    }

    public void CalculateEfficiencyAgain() {
        if (user == null) {
            efficiency = 0;
            return;
        }
        int energy = (user as Staff).attributes.energy;
        efficiency = (user as Staff).attributes.social * (energy / 100.0f) + 0.25f;       
        if (energy > 80) {
            GameManager.emo.PopEmotion("veryHappy", user.transform, new Vector2(0, 2));
        } else if (energy > 40) {
            GameManager.emo.PopEmotion("happy", user.transform, new Vector2(0, 2));
        } else {
            GameManager.emo.PopEmotion("sad", user.transform, new Vector2(0, 2));
        }
    }
}
