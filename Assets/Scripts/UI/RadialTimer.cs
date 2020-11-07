using System;
using UnityEngine;
using UnityEngine.UI;

/* Controls the radial timer ui
 * the radial timer is just for looks
 */

public class RadialTimer : MonoBehaviour 
{
    public Image radialImage;
    private bool radialTimerStarted = false;

    public void StartTimerRadial(float time, Action completeFunc = null) {
        if (radialTimerStarted) return;
        radialTimerStarted = true;

        LTDescr id;
        id = LeanTween.value(0.0f, 1.0f, time).setOnUpdate((float val) => {
            radialImage.fillAmount = val;
            if (val >= 1.0f) {
                radialTimerStarted = false;
            }
        });

        if (completeFunc != null) {         
            id.setOnComplete(completeFunc);         
        }
    }

    public bool IsRadialTimerOn() {
        return radialTimerStarted;
    }
}
