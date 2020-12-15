using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : ShopPhaseBase
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        InitialDialogue();
    }

    private void InitialDialogue() {
        gameMgr.introDialogue.TriggerDialogue(() => CameraControlPopUp());
    }

    private void CameraControlPopUp() {
        PopUpSet[] popupSets = new PopUpSet[1];
        popupSets[0] = new PopUpSet(
            new Vector2(0.5f, 0.5f),
            new Vector2(0, 0),
            "Camera Controls",
            "Drag the camera around by holding the Middle Mouse button." +
            "\nPress Space to return to the center." +
            "\nClose the Pop Up to advance.",
            gameMgr.mainUIManager.clipboard.GetComponent<ActiveUI>()
            );

        PopUpManager.StartPopups(popupSets, () => UISequenceStart());

    }

    private void UISequenceStart() {
        gameMgr.mainUIManager.StartSequence(() => MainTutorialPopups());
    }

    private void MainTutorialPopups() {
        PopUpSet[] mainPopupSets = new PopUpSet[4]; // Set 0 <- MAIN SET

        //--------------------------------------------------
        PopUpSet[] managementPopupSets = new PopUpSet[1]; //Set 1
        //Management Popup - Follow Up
        managementPopupSets[0] = new PopUpSet(
            new Vector2(0, 0),
            new Vector2(420, 280),
            "Management",
            "Higher prices may make customers look for another shop, " +
            "while lower wages will drain the motivation out of your staff quicker." +
            "\nIgnore this for now",
            gameMgr.mainUIManager.management.GetComponent<ActiveUI>(),
            true
            );

        //Main Management Popup
        mainPopupSets[0] = new PopUpSet(
            new Vector2(0, 0),
            new Vector2(180, 130),
            "Management",
            "This allows you to change prices, staff and the shop." +
            "\n" +
            "\nClick <sprite index=0> for more info.",
            gameMgr.mainUIManager.management.GetComponent<ActiveUI>(),
            false,
            () => {
                PopUpManager.StartPopups(managementPopupSets, null, 1);
            }
            );

        //--------------------------------------------------
        PopUpSet[] ledgerPopupSets = new PopUpSet[1];
        //Ledger Popup - Follow Up
        ledgerPopupSets[0] = new PopUpSet(
            new Vector2(0, 0),
            new Vector2(420, 280),
            "Ledger",
            "This contains records of all profits you earned each day.",// +
            //"\n" +
            //"\nHover each item to view a more detailed info.",
            gameMgr.mainUIManager.ledger.GetComponent<ActiveUI>(),
            true
            );

        //Ledger Popup
        mainPopupSets[1] = new PopUpSet(
            new Vector2(0, 0),
            new Vector2(240, 130),
            "Ledger",
            "Contains all the previous transactions." +
            "\n" +
            "\nClick <sprite index=1> for more info.",
            gameMgr.mainUIManager.ledger.GetComponent<ActiveUI>(),
            false,
            () => {
                PopUpManager.StartPopups(ledgerPopupSets, null, 1);
            }
            );

        //--------------------------------------------------
        PopUpSet[] shopPopupSets = new PopUpSet[1];
        //Shop Popup - Follow Up
        shopPopupSets[0] = new PopUpSet(
            new Vector2(0, 0),
            new Vector2(420, 280),
            "Shop",
            "This allows you to buy more furnitures or replacement of current ones. " +
            "You can buy uprades for the shop as well." +
            "\nItems are unlocked as you progress.",
            gameMgr.mainUIManager.shop.GetComponent<ActiveUI>(),
            true
            );

        //Shop Popup
        mainPopupSets[2] = new PopUpSet(
            new Vector2(0, 0),
            new Vector2(300, 130),
            "Store",
            "Lets you buy furnitures and upgrades. " +
            "It disappears when you start opening and will not be available until the next day." +
            "\nClick <sprite index=2> for more info.",
            gameMgr.mainUIManager.shop.GetComponent<ActiveUI>(),
            false,
            () => {
                PopUpManager.StartPopups(shopPopupSets, null, 1);
            }
            );

        //--------------------------------------------------
        PopUpSet[] clipboardPopupSets = new PopUpSet[1];
        //Shop Popup - Follow Up
        clipboardPopupSets[0] = new PopUpSet(
            new Vector2(1, 0),
            new Vector2(-390, 170),
            "Clipboard",
            "<sprite index=3> High energy = more efficiency. Decreases with every successful task." +
            "\n<sprite index=4> Social deals with talking to people." +
            "\n<sprite index=5> Tech is for technical problems.",
            gameMgr.mainUIManager.clipboard.GetComponent<ActiveUI>(),
            true
            );

        //Clipboard Popup
        mainPopupSets[3] = new PopUpSet(
            new Vector2(1, 0),
            new Vector2(-180, 130),
            "Clipboard",
            "Shows the attributes of a selected staff." +
            "\nIt won't show anything if a staff is not selected.",
            gameMgr.mainUIManager.clipboard.GetComponent<ActiveUI>(),
            false,
            () => {
                PopUpManager.StartPopups(clipboardPopupSets, null, 1);
            }
            );

        PopUpManager.StartPopups(mainPopupSets, () => {
            shopStateMachine.SetBool("tutorialDone", true);
            });
    }
}
