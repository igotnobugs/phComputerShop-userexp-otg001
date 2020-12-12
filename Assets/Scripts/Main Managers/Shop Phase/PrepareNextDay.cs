using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareNextDay : ShopPhaseBase
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        GameManager.store.AdvanceDay();
        gameMgr.sun.EarlyMorning(2.0f);

        gameMgr.phaseInitial.text = "<color=red>C</color>";

        gameMgr.mainUIManager.StartSequence(() => {
            gameMgr.stateMachine.SetTrigger("goToNextDay");
        });
      
    }

}
