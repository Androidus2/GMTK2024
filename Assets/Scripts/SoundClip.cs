using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundClip
{

    public string name;
    public bool loop;
    public AudioClip clip;
    public AudioSource source;
    public float volume;
    public float pitch;

    public SoundClip(string name, AudioClip clip, bool loop)
    {
        this.name = name;
        this.clip = clip;
        this.loop = loop;
    }

}
