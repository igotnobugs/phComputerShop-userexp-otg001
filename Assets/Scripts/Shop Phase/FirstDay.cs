using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDay : ShopPhaseBase
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);



        Counter counter = FindObjectOfType<Counter>();
        counter.CalculateEfficiencyAgain();

        for (int i = 0; i < gameMgr.staffs.Length; i++) {
            StaffPanelManager.CreateStaffButton(gameMgr.staffs[i]);
            gameMgr.staffs[i].AllowSelection = true;
        }

        //Phase has exit time set to 1 second

        gameMgr.phaseInitial.text = "<color=red>C</color>";
    }
}
