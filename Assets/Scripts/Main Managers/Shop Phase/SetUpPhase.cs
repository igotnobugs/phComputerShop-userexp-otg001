using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpPhase : ShopPhaseBase
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        gameMgr.openShopButton.Show();

        //Start Popup Popup
        Vector2 uiPosition = gameMgr.mainUIManager.clipboard.transform.position + new Vector3(0, 20);
        Vector2 pivotPosition = new Vector2(1, 0);
        PopUp tutorialPopUp = gameMgr.popUpManager.CreatePopUp(pivotPosition, uiPosition);
        tutorialPopUp.Init("Start", "Before you open the shop, make sure the counter is manned by clicking one of your staffs and Right-Click the Counter then choose Interact.");
        tutorialPopUp.SetListener(gameMgr.openShopButton.GetComponent<TransistionUI>(), true);
        tutorialPopUp.SetOnComplete(() => Destroy(tutorialPopUp));
    }
}
