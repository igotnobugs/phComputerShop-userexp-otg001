using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Computer : Furniture 
{
    [Header("Computer Settings")]
    public Light2D computerLight;
    public Color onColor;
    public Color idleColor;
    public Color offColor;
    public Color brokenColor;


    private PersonalAudio audioManager;

    protected override void Awake() {
        base.Awake();
        audioManager = GetComponent<PersonalAudio>();
    }

    public void Start() {
        computerLight.color = idleColor;
        mat.SetColor("_EmissionColor", idleColor);
    }

    protected override void Occupied() {
        if (isBroken) return;
        base.Occupied();
        computerLight.color = onColor;
        mat.SetColor("_EmissionColor", onColor);
    }

    protected override void Unoccupied() {
        if (isBroken) return;
        base.Unoccupied();
        computerLight.color = idleColor;
        mat.SetColor("_EmissionColor", idleColor);
    }

    public override void SetBroken() {
        if (isBroken) return;
        base.SetBroken();
        audioManager.Play("Broken");
        computerLight.color = brokenColor;
        mat.SetColor("_EmissionColor", brokenColor);
    }

    public override void SetFixed() {
        base.SetFixed();
        audioManager.Play("Fixed");
        computerLight.color = idleColor;
        mat.SetColor("_EmissionColor", idleColor);
    }
}


