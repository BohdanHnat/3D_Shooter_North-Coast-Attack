using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioSystemManager : MonoBehaviour
{
    [SerializeField] private string key;
    [SerializeField] private AudioPlayer _sfxPrefab;
    [SerializeField] private AudioPlayer _musicPrefab;

    [SerializeField] private AudioClip[] _sfx;
    [SerializeField] private AudioClip[] _music;

    private AudioPlayer _currentMusic;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        var audioSystem = Context.Instance.AudioSystem;

        audioSystem.OnMusic += OnPlayMusic;
        audioSystem.OnSFX += OnPlaySFX;
    }

    [ContextMenu("PlaySFX")]
    public void PlaySFX()
    {
        OnPlaySFX(new AudioData(key));
    }
    public void OnPlaySFX(AudioData audioData)
    {
        var clip = _sfx.FirstOrDefault(s => s.name == audioData.key);

        if (clip == null)
        {
            Debug.LogAssertion($"There is no clip with name {audioData.key}");
            return;
        }

        var audioPlayer = Instantiate(_sfxPrefab, transform);
        audioPlayer.Set(audioData, clip);
    }
    public void OnPlayMusic(AudioData audioData)
    {
        var clip = _music.FirstOrDefault(s => s.name == audioData.key);
        if (clip == null)
        {
            Debug.LogAssertion($"There is no clip with name {audioData.key}");
            return;
        }

        _currentMusic?.Fade(false, 1f);
        _currentMusic = Instantiate(_musicPrefab, transform);
        _currentMusic.Set(audioData, clip);
        _currentMusic.Fade(true, 1f);
    }
}
