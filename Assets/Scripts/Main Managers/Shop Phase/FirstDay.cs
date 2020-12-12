using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDay : ShopPhaseBase
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        for (int i = 0; i < gameMgr.staffs.Length; i++) {
            gameMgr.staffPanel.CreateStaffButton(gameMgr.staffs[i]);
            gameMgr.staffs[i].EnableSelection();
        }

        //Phase has exit time set to 1 second

        gameMgr.phaseInitial.text = "<color=red>C</color>";
    }
}
