using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EmotionPop : MonoBehaviour 
{

    public TextMeshProUGUI content;
    public Transform transformTotrack;
    public Vector3 positionOffset;

    public void Initialize(string emotionCode, Transform track, Vector2 offset, float time = 2.0f) {
        content.text = emotionCode;
        transformTotrack = track;
        positionOffset = offset;
        Destroy(gameObject, time);
    }

    private void Update() {
        if (transformTotrack == null) {
            Destroy(gameObject);
            return;
        }
        transform.position = transformTotrack.position + positionOffset;

    }
}
