using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    public void Set(AudioData settings, AudioClip clip)
    {
        _source.clip = clip;
        _source.volume = settings.volume;
        _source.pitch = settings.pitch;
        transform.position = settings.position;
        _source.Play();
        if (!_source.loop)
            StartCoroutine(DisableAfterFinish());
    }
    private IEnumerator DisableAfterFinish()
    {
        yield return new WaitForSeconds(_source.clip.length);
        gameObject.SetActive(false);
    }
    public void Fade(bool isIn, float duration)
    {
        StartCoroutine(FadeRoutine(isIn, duration));
    }
    private IEnumerator FadeRoutine(bool isIn, float duration)
    {
        float counter = duration;
        while (counter > 0)
        {
            counter -= Time.deltaTime;
            var normalized = isIn ? 1 - counter / duration : counter / duration;
            _source.volume = normalized;
            yield return null;
        }

        if (!isIn)
            gameObject.SetActive(false);
    }
}
