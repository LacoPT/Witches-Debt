using UnityEngine;
using Zenject.SpaceFighter;

public abstract class AbstractAudioSource : MonoBehaviour 
{
    protected AudioSource source;
    protected AudioSourceType sourceType;
    protected float baseVolume;
    protected float generalVolume = 1f;
    protected float typeVolume = 1f;
    protected virtual void Start()
    {
        source = GetComponent<AudioSource>();
        baseVolume = source.volume;
        AudioMixer.Instance.GeneralVolumeChanged.AddListener(OnGeneralVolumeChanged);
        if (sourceType == AudioSourceType.SFX)
            AudioMixer.Instance.SFXVolumeChanged.AddListener(OnTypeVolumeChanged);
        else
            AudioMixer.Instance.MusicVolumeChanged.AddListener(OnTypeVolumeChanged);
            source.Play();
    }

    protected virtual void OnGeneralVolumeChanged(float newVolume)
    {
        generalVolume = newVolume;
        source.volume = baseVolume * generalVolume * typeVolume;
    }

    protected virtual void OnTypeVolumeChanged(float newVolume)
    {
        typeVolume = newVolume;
        source.volume = baseVolume * generalVolume * typeVolume;
    }

    protected virtual void PlaySound(AudioClip clip = null)
    {
        if (clip) source.PlayOneShot(clip);
        else source.Play();
    }
}