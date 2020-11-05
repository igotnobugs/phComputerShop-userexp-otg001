using System;
using UnityEngine;
using UnityEngine.UI;

/* Handles the timer
 * and the radial timer UI
 */

public class TimerControl : MonoBehaviour
{
    public Image timerImage;

    private bool radialTimerStarted = false;

    // Standard timer, runs a given function after it ends
    public void StartTimer(float time, Action completeFunc = null) {
        LTDescr id;
        id = LeanTween.value(gameObject, 0.0f, 1.0f, time);

        if (completeFunc != null)
            id.setOnComplete(completeFunc);
    }

    // Timer that also modifies the radial clock
    public void StartTimerRadial(float time, Action completeFunc = null) {
        if (radialTimerStarted) return;
        radialTimerStarted = true;

        LTDescr id;
        id = LeanTween.value(0.0f, 1.0f, time).setOnUpdate((float val) => {
            timerImage.fillAmount = val;
        });

        if (completeFunc != null) {
            radialTimerStarted = false;
            id.setOnComplete(completeFunc);
        }
    }
}
