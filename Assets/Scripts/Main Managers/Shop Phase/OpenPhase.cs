using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPhase : ShopPhaseBase
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        //Spawn at the door
        Customer newCustomer = Instantiate(gameMgr.testCustomer, 
            gameMgr.door.position,
            gameMgr.testCustomer.transform.rotation);
        newCustomer.door = gameMgr.door;
        newCustomer.costumerList = gameMgr.customers;

        gameMgr.customers.Add(newCustomer);


        gameMgr.phaseInitial.text = "<color=green>O</color>";
        gameMgr.mainUIManager.StartOpenPhase();
        gameMgr.openShopButton.Hide();
        gameMgr.sun.WorkingDays(gameMgr.shopOpenTime);

        gameMgr.radialTimer.StartTimerRadial(gameMgr.shopOpenTime, 
            () => gameMgr.stateMachine.SetTrigger("openShopClosing"));
    }
}
