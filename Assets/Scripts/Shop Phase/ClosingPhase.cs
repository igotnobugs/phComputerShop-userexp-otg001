using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingPhase : ShopPhaseBase
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        gameMgr.sceneAudio.Stop("ShopAmbience");
        gameMgr.sceneAudio.Stop("DayAmbience");
        gameMgr.sceneAudio.Play("NightAmbience");

        gameMgr.StopSpawningCustomers();

        // Make customers leave forcefully if present
        if (GameManager.customersInStore.Count > 0) {
            foreach (Customer c in GameManager.customersInStore) {
                c.LeaveTheStore();
            }
        }

        // Check for events here maybe?

        //Set every thing unoccupied
        Computer[] computers = FindObjectsOfType<Computer>();
        for (int i = 0; i < computers.Length; i++) {
            if (computers[i].isOccupied) {
                computers[i].SetUnoccupied();
            }
        }

        gameMgr.phaseInitial.text = "<color=red>C</color>";
        gameMgr.StartCalculatingDayResult(() => {
            shopStateMachine.SetTrigger("doneCalculatingResults");
        });
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateExit(animator, stateInfo, layerIndex);

        gameMgr.sun.GoToNextDay();
      
    }
}
