using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : ShopPhaseBase
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        gameMgr.StartTutorial(() => {
            gameMgr.stateMachine.SetBool("tutorialDone", true);
        });

    }
}
