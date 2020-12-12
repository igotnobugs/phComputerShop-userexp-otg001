using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingPhase : ShopPhaseBase
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        // Make customers leave forcefully if present
        if (gameMgr.customers.Count > 0) {
            foreach (Customer c in gameMgr.customers) {
                c.LeaveTheStore();
            }
        }

        // Check for events here maybe?

        gameMgr.phaseInitial.text = "<color=red>C</color>";
        gameMgr.StartCalculatingDayResult(() => {
            gameMgr.stateMachine.SetTrigger("doneCalculatingResults");
        });
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateExit(animator, stateInfo, layerIndex);

        gameMgr.sun.GoToNextDay();
      
    }
}
