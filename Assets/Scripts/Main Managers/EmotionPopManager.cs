using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class Emoji {
    public string name;
    public string code;
}

public class EmotionPopManager : MonoBehaviour 
{
    public Canvas worldCanvas;
    public EmotionPop emoPopPrefabToUse;
    public float emotionChance = 0.25f;

    public static EmotionPop emoPopPrefab;

    [SerializeField] public Emoji[] indexEmoji;

    private void Start() {
        emoPopPrefab = emoPopPrefabToUse;
    }


    public void PopEmotion(string emojiName, Transform track, Vector2 offset, bool always = false) {
        if (!always) {
            float chance = UnityEngine.Random.Range(0, 1.0f);
            if (chance > emotionChance) return;
        }

        Emoji emo = Array.Find(indexEmoji, indexEmoji => indexEmoji.name == emojiName);
        if (emo == null) {
            Debug.LogWarning("Emoji: '" + emojiName + "' not found!");
        }
        EmotionPop EmoPop = Instantiate(emoPopPrefabToUse, worldCanvas.transform);
        EmoPop.Initialize(emo.code, track, offset);
    }
}
