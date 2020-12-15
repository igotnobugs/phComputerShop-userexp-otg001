using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut3_Fixing : ShopPhaseBase
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        
        for (int i = 0; i < 2; i++) {
            Computer[] computers = FindObjectsOfType<Computer>();
            int random = Random.Range(0, computers.Length);
            computers[random].SetBroken();
        }

        Tutorial3Dialogue();
    }

    private void Tutorial3Dialogue() {
        gameMgr.tutorial3Dialogue.TriggerDialogue(() => {
            GoToNextDay();
        });
    }

    private void GoToNextDay() {


        gameMgr.mainUIManager.StartSequence(() => {
            shopStateMachine.SetTrigger("goToNextDay");
        });

        Computer[] computers = FindObjectsOfType<Computer>();
        for (int i = 0; i < computers.Length; i++) {
            computers[i].allowBrokenAfterUse = true;
        }

    }
}
