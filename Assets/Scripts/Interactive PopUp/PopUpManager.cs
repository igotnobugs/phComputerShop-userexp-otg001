using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* Branching Popups System
 * Only two sets of PopUpSets
 * set 0 = Main Popup Chain
 * set 1 = Branching Popups
 */

public class PopUpSet {
    public Vector2 pivot;
    public Vector2 position;
    public string title;
    public string content;
    public BaseUI listener;
    public bool listenerOpposite;
    public Action actionOnFollow;

    public PopUpSet(Vector2 piv, Vector2 pos, 
        string t, string c, 
        BaseUI ui, bool isOpp = false, 
        Action onFollow = null) {
        pivot = piv;
        position = pos;
        title = t;
        content = c;
        listener = ui; //Listen onActive
        listenerOpposite = isOpp; //Listen onDeactive
        actionOnFollow = onFollow;
    }
}

public class PopUpManager : MonoBehaviour 
{
    [SerializeField] private PopUp popUpPrefab = null;
    public Canvas gameUI;

    private static PopUp popUpPrefabToUse;
    private static Canvas gameUIToUse;

    private static Queue<PopUpSet>[] queuedPopups;
    private static Action[] onPopupsEnd;

    private void Awake() {
        queuedPopups = new Queue<PopUpSet>[2]; 
        onPopupsEnd = new Action[2];
        popUpPrefabToUse = popUpPrefab;
        gameUIToUse = gameUI;
    }

    public static void StartPopups(PopUpSet[] popups, Action onEnd = null, int set = 0) {
        queuedPopups[set] = new Queue<PopUpSet>();
        for (int i = 0; i < popups.Length; i++) {
            queuedPopups[set].Enqueue(popups[i]);
        }
        DisplayNextPopup(set);
        onPopupsEnd[set] = onEnd;

    }

    private static void DisplayNextPopup(int set = 0) {
        if (queuedPopups[set].Count <= 0) {
            onPopupsEnd[set]?.Invoke();
            if (set != 0) {
                ContinueSet(0);
            }
            return;
        }

        PopUpSet popUpSet = queuedPopups[set].Dequeue();
        PopUp newPopup = Instantiate(popUpPrefabToUse, gameUIToUse.transform);
        newPopup.MoveTo(popUpSet.pivot, popUpSet.position);
        newPopup.Init(popUpSet.title, popUpSet.content);
        newPopup.SetListener(popUpSet.listener, 
            popUpSet.listenerOpposite,
            popUpSet.actionOnFollow);
        newPopup.SetOnIgnored(() => {
            DisplayNextPopup(0);
            Destroy(newPopup);         
        });
    }  

    private static void ContinueSet(int set) {
        DisplayNextPopup(set);
    }
}
