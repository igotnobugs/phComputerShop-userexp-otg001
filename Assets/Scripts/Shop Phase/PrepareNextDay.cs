using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareNextDay : ShopPhaseBase
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        GameManager.store.AdvanceDay();
        gameMgr.sun.EarlyMorning(2.0f);
        gameMgr.sceneAudio.Play("DayAmbience");

        gameMgr.phaseInitial.text = "<color=red>C</color>";

        //Run events

        switch (shopStateMachine.GetInteger("day")) {
            case 0:
                GoAsUsual();
                break;
            case 1:
                shopStateMachine.SetTrigger("tutorial2");
                break;
            case 2:
                shopStateMachine.SetTrigger("tutorial3");
                break;
            //case 3:
             //   shopStateMachine.SetTrigger("tutorial4");
              //  break;
            default:
                GoAsUsual();
                break;
        }        

        if (shopStateMachine.GetInteger("day") > 1) {
            gameMgr.mainUIManager.managementTab.AllowInteraction();
        }
    }

    public void GoAsUsual() {
        gameMgr.mainUIManager.StartSequence(() => {
            shopStateMachine.SetTrigger("goToNextDay");
        });
    }

}
