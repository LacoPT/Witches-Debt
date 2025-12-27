using System;

public class AudioMixer
{
    public float GeneralVolume { get; private set; } = 1f;
    public float MusicVolume {get; private set; } = 1f;
    public float SoundEffectsVolume { get; private set; } = 1f;

    public event Action<float> GeneralVolumeChanged;
    public event Action<float> MusicVolumeChanged;
    public event Action<float> SoundEffectsVolumeChanged;

    public void OnGeneralVolumeChanged(float value)
    {
        GeneralVolume = value;
        GeneralVolumeChanged?.Invoke(value);
    }

    public void OnMusicVolumeChanged(float value)
    {
        MusicVolume = value;
        MusicVolumeChanged?.Invoke(value);
    }

    public void OnSoundEffectsVolumeChanged(float value)
    {
        SoundEffectsVolume = value;
        SoundEffectsVolumeChanged?.Invoke(value);
    }
}