using System;
using UnityEngine;

public class Timer
{
    public LTDescr StartTimer(GameObject go, float time) {
        return LeanTween.value(go, 0.0f, 1.0f, time);
    }
}
