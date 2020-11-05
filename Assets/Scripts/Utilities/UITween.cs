using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/* Bunch of UI Tweening Function
 * 
 * 
 */ 


public class UITween : MonoBehaviour 
{
    public bool startEnabled = false;
    public bool setDefaultPosition = false;
    public Vector3 defaultPosition; 
    public Vector3 relativePosition; 
    public float timeToTween;
    public LeanTweenType tweenType;

    private RectTransform rect;
    private bool isToggled;

    private void Awake() {
        rect = GetComponent<RectTransform>();
    }

    private void Start() {
        if (setDefaultPosition) {
            rect.anchoredPosition = defaultPosition;
        }
        else {
            defaultPosition = rect.anchoredPosition;
        }
        gameObject.SetActive(startEnabled);
        isToggled = startEnabled;
    }

    public void GoToDefaultPosition(bool disableWhenDone) {
        gameObject.SetActive(true);
        LTDescr id = LeanTween.move(rect, defaultPosition, timeToTween).setEase(tweenType);

        if (disableWhenDone) {
            void endFunction() { DisableWhenDone(); }
            id.setOnComplete(endFunction);
        }
    }

    public void GoToDefaultPosition(Action completeFunction = null) {
        gameObject.SetActive(true);
        LTDescr id = LeanTween.move(rect, defaultPosition, timeToTween).setEase(tweenType);

        if (completeFunction != null) 
            id.setOnComplete(completeFunction);
    }

    public void GoToRelativePosition(bool disableWhenDone) {
        gameObject.SetActive(true);
        Vector3 newPosition = defaultPosition + relativePosition;
        LTDescr id = LeanTween.move(rect, newPosition, timeToTween).setEase(tweenType);

        if (disableWhenDone) {
            void endFunction() { DisableWhenDone(); }
            id.setOnComplete(endFunction);
        }
    }

    public void GoToRelativePosition(Action completeFunction = null) {
        gameObject.SetActive(true);
        Vector3 newPosition = defaultPosition + relativePosition;
        LTDescr id = LeanTween.move(rect, newPosition, timeToTween).setEase(tweenType);

        if (completeFunction != null) 
            id.setOnComplete(completeFunction);
    }

    private void DisableWhenDone() {
        gameObject.SetActive(false);
    }

    // For other tweening
    public void TweenTo(Vector3 relDest, float time, LeanTweenType type, Action onComplete = null) {
        LTDescr id = LeanTween.move(rect, defaultPosition + relDest, time).setEase(type);

        if (onComplete != null)
            id.setOnComplete(onComplete);
    }

    public void TweenToX(float relDest, float time, LeanTweenType type, Action onComplete = null) {
        LTDescr id = LeanTween.moveX(rect, defaultPosition.x + relDest, time).setEase(type);

        if (onComplete != null)
            id.setOnComplete(onComplete);
    }

    public void Toggle() {
        if (isToggled) {
            GoToDefaultPosition(true);
            isToggled = false;
        } else {
            GoToRelativePosition(false);          
            isToggled = true;
        }
    }


    public void PeekOut() {
        Vector3 newPosition = defaultPosition + relativePosition;
        LTDescr id = LeanTween.move(rect, newPosition, timeToTween).setEase(tweenType);
    }

    public void PeekIn() {
        LTDescr id = LeanTween.move(rect, defaultPosition, timeToTween).setEase(tweenType);
    }

}
