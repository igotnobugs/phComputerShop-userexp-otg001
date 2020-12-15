using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpPhase : ShopPhaseBase
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        gameMgr.storeData.GetTotalWages();

        gameMgr.openShopButton.GetComponent<TransistionUI>().Show();

        gameMgr.mainUIManager.managementTab.SetValues();

        for (int i = 0; i < gameMgr.staffs.Length; i++) {
            gameMgr.staffs[i].ReplenishEnergy();
        }

        shopStateMachine.SetInteger("day", shopStateMachine.GetInteger("day") + 1);

        ReminderPopup();

        gameMgr.openShopButton.onClick.AddListener(() => {
            shopStateMachine.SetTrigger("openShopClicked");
        });
    }

    private void ReminderPopup() {
        PopUpSet[] popupSets = new PopUpSet[1];
        popupSets[0] = new PopUpSet(
            new Vector2(1, 0),
            new Vector2(-170, 150),
            "Start",
            "Before you open the shop, make sure the counter is manned by clicking one of your staffs and Right - Click the Counter then choose Interact.",
            gameMgr.openShopButton.GetComponent<TransistionUI>(),
            true
            );

        PopUpManager.StartPopups(popupSets);
    }
}
