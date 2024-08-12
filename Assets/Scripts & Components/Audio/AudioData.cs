using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioData
{
    public string key;
    public float pitch;
    public float volume;
    public Vector3 position;
    public AudioData(string key, Vector3 position = default, float pitch = 1, float volume = 1)
    {
        this.key = key;
        this.pitch = pitch;
        this.volume = volume;
        this.position = position;
    }
}
