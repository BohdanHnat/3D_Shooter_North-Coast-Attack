using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IAudioSystem
{
    float SFXVolume { get; set; }
    float MusicVolume { get; set; }
    
    event Action<float> SFXVolumeChanged;
    event Action<float> MusicVolumeChanged;
    
    event Action<AudioData> OnSFX;
    event Action<AudioData> OnMusic;

    void PlaySFX(string key);
    void PlaySFX(AudioData audioData);
    void PlayMusic(string key);
    void PlayMusic(AudioData audioData);
}

public class AudioSystem : IAudioSystem
{
    public float SFXVolume { get; set; }
    public float MusicVolume { get; set; }
    
    public event Action<float> SFXVolumeChanged;
    public event Action<float> MusicVolumeChanged;
    
    public event Action<AudioData> OnSFX;
    public event Action<AudioData> OnMusic;

    public void PlaySFX(string key)
    {
        PlaySFX(new AudioData(key));
    }
    public void PlaySFX(AudioData settings)
    {
        OnSFX?.Invoke(settings);
    }
    public void PlayMusic(string key)
    {
        PlayMusic(new AudioData(key));
    }
    public void PlayMusic(AudioData settings)
    {
        OnMusic?.Invoke(settings);
    }
}
