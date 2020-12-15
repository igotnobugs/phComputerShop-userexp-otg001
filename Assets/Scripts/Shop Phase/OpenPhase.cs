using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPhase : ShopPhaseBase
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        gameMgr.mainUIManager.managementTab.DisallowInteraction();

        gameMgr.sceneAudio.Stop("NightAmbience");
        gameMgr.sceneAudio.Play("ShopOpen");     
        gameMgr.sceneAudio.Play("ShopAmbience");

        Counter counter = FindObjectOfType<Counter>();
        counter.Reset();

        gameMgr.phaseInitial.text = "<color=green>O</color>";
        gameMgr.mainUIManager.StartOpenPhase();
        gameMgr.openShopButton.GetComponent<TransistionUI>().Hide();
        gameMgr.sun.WorkingDays(gameMgr.shopOpenTime);

        //Spawn at the door
        gameMgr.StartSpawningCustomers();


        gameMgr.radialTimer.StartTimerRadial(gameMgr.shopOpenTime, 
            () => shopStateMachine.SetTrigger("openShopClosing"));

        //gameMgr.stateMachine.SetTrigger("runEvent");
    }
}
