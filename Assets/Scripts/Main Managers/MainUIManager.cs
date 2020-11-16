using System;
using System.Collections;
using UnityEngine;

/* Sequences the UI animation
 * 
 */

public class MainUIManager : Singleton<MainUIManager> 
{
    public BaseUI ledger;
    public BaseUI shop;
    public BaseUI staffPanel;
    public BaseUI clipboard;

    public TransistionUI[] transistionInterfaces;

    private Action onCompleteFunction;
    private bool isUIShown;
    private TransistionUI shopTransisition = null;

    public void StartSequence(Action onComplete, bool showUI = true) {
        onCompleteFunction = onComplete;
        if (showUI) {
            StartCoroutine(StartShowSequence());
        }
        else {
            StartCoroutine(StartHideSequence());
        }
        shopTransisition = shop.GetComponent<TransistionUI>();
    }

    private IEnumerator StartShowSequence() {
        if (isUIShown) yield break;
        isUIShown = true;

        for (int i = 0; i < transistionInterfaces.Length; i++) {
            transistionInterfaces[i].Show();
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(0.5f);
        
        onCompleteFunction?.Invoke();
        yield break;
    }

    private IEnumerator StartHideSequence() {
        if (!isUIShown) yield break;
        isUIShown = false;

        for (int i = transistionInterfaces.Length - 1; i >= 0; i--) {
            transistionInterfaces[i].Hide();
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(0.5f);

        onCompleteFunction?.Invoke();
        yield break;
    }

    // During set up
    public void StartSetUpPhase() {
        shopTransisition.Show();
    }

    // During open phase
    public void StartOpenPhase() {
        shopTransisition.Hide();
    }
}
