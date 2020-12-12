using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPhaseBase : StateMachineBehaviour 
{
    protected GameManager gameMgr;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        gameMgr = animator.GetComponent<GameManager>();
    }
}
