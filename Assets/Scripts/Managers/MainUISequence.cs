using System;
using System.Collections;
using UnityEngine;

/* Sequences the UI animation
 * 
 */

public class MainUISequence : Singleton<MainUISequence> 
{
    public TransistionUI[] transistionInterfaces;
    public TransistionUI shopUI;

    private Action onCompleteFunction;
    private bool isUIShown;


    public void StartSequence(Action onComplete, bool showUI = true) {
        onCompleteFunction = onComplete;
        if (showUI) {
            StartCoroutine(StartShowSequence());
        }
        else {
            StartCoroutine(StartHideSequence());
        }

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
}
