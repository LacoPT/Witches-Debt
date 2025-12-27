using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

public abstract class AbstractAudioSource : MonoBehaviour 
{
    protected AudioSource source;
    protected AudioSourceType sourceType;
    protected float baseVolume;
    protected float generalVolume = 1f;
    protected float typeVolume = 1f;
    protected AudioMixer audioMixer;

    [Inject]
    public void Construct(AudioMixer audioMixer)
    {
        this.audioMixer = audioMixer;
        generalVolume = audioMixer.GeneralVolume;
    }

    protected virtual void Start()
    {
        source = GetComponent<AudioSource>();
        audioMixer.GeneralVolumeChanged += OnGeneralVolumeChanged;
        if (sourceType == AudioSourceType.SFX)
        {
            audioMixer.SoundEffectsVolumeChanged += OnTypeVolumeChanged;
        }
        else
        {
            audioMixer.MusicVolumeChanged += OnTypeVolumeChanged;
        }
        baseVolume = source.volume;
        typeVolume = (sourceType == AudioSourceType.SFX) ? audioMixer.SoundEffectsVolume : audioMixer.MusicVolume;
        source.volume = baseVolume * generalVolume * typeVolume;
        source.Play();
    }

    protected virtual void OnDisable()
    {
        audioMixer.GeneralVolumeChanged -= OnGeneralVolumeChanged;
        if (sourceType == AudioSourceType.SFX)
        {
            audioMixer.SoundEffectsVolumeChanged -= OnTypeVolumeChanged;
        }
        else
        {
            audioMixer.MusicVolumeChanged -= OnTypeVolumeChanged;
        }
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