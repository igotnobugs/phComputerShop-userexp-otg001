using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

/* Rushed
 * WIP, CLEAN UP
 * 
 * 3 Positions 
 * 
 * Dawn Position 
 * Set up Phase Position
 * Night time Position
 */


public class OutsideLight : MonoBehaviour 
{
    public Color dawnColor;
    public Light2D globalLight;
    public Color nightColor;

    public Light2D sunLight1;
    public Light2D sunLight2;

    public Light2D areaLight;

    public Transform dawn;
    public LeanTweenType morningEase = LeanTweenType.linear;
    public Transform setUp;
    public LeanTweenType eveningEase = LeanTweenType.linear;
    public Transform nightTime;

    private void Start() {
        transform.position = dawn.position;
    }

    // Dawn Position to Set up Phase Position
    public void EarlyMorning(float time, Action completeFunction = null) {
        LTDescr id = LeanTween.move(gameObject, setUp, time).setEase(morningEase);

        LeanTween.value(gameObject, nightColor, dawnColor, time).setOnUpdate((Color val) => {
            globalLight.color = val;
            sunLight1.color = val;
            sunLight2.color = val;
            areaLight.color = val;
        });

        LeanTween.value(gameObject, 0.1f, 0.4f, time).setOnUpdate((float val) => {
            globalLight.intensity = val;
        });

        LeanTween.value(gameObject, 0.2f, 1.6f, time).setOnUpdate((float val) => {
            areaLight.intensity = val;
        });

        if (completeFunction != null)
            id.setOnComplete(completeFunction);
    }

    // Set up Phase to Night time Position
    // Usually when the timer is counting down
    public void WorkingDays(float time, Action completeFunction = null) {
        LTDescr id = LeanTween.move(gameObject, nightTime, time).setEase(eveningEase);

        LeanTween.value(gameObject, dawnColor, nightColor, time).setOnUpdate((Color val) => {
            globalLight.color = val;
            sunLight1.color = val;
            sunLight2.color = val;
        });

        LeanTween.value(gameObject, 0.4f, 0.1f, time).setOnUpdate((float val) => {
            globalLight.intensity = val;
        });

        LeanTween.value(gameObject, 1.6f, 0.2f, time).setOnUpdate((float val) => {
            areaLight.intensity = val;
        });

        if (completeFunction != null)
            id.setOnComplete(completeFunction);
    }

    // Change position to Dawn 
    public void GoToNextDay() {
        transform.position = dawn.position;
    }

}