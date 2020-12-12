using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : ShopPhaseBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    
        gameMgr.sun.EarlyMorning(3.0f);

        gameMgr.stateMachine.SetBool("tutorialEnabled", gameMgr.showTutorial);

        gameMgr.transistion.Hide(() => {
            if (gameMgr.showTutorial) {
                gameMgr.stateMachine.SetTrigger("initialized");
            }
            else {
                gameMgr.mainUIManager.StartSequence(() => {
                    gameMgr.stateMachine.SetTrigger("initialized");
                });
            }
        }).setDelay(0.5f);
    }
}
