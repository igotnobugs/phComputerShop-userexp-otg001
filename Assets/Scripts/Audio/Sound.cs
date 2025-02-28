﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//Reference:https://www.youtube.com/watch?v=6OT43pvUyfY&ab_channel=Brackeys
[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    public bool playOnAwake;

    [HideInInspector]
    public AudioSource source;
}
