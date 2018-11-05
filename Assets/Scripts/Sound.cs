using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable] //Do this so the script shows up in the Inspector
public class Sound {

    public string name;

    public AudioClip clip;

    public float volume;

    [HideInInspector]
    public AudioSource source;
}
