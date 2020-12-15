using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut2_PriceWages : ShopPhaseBase
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        Tutorial2Dialogue();
    }

    private void Tutorial2Dialogue() {
        gameMgr.tutorial2Dialogue.TriggerDialogue(() => {
            GoToNextDay();
        });
    }

    private void GoToNextDay() {
        gameMgr.mainUIManager.StartSequence(() => {
            shopStateMachine.SetTrigger("goToNextDay");
        });

        gameMgr.mainUIManager.managementTab.AllowInteraction();
    }

}
