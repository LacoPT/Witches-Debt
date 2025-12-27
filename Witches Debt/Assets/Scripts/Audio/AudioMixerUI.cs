using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class AudioMixerUI : MonoBehaviour
{
    [SerializeField] private Slider generalSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundEffectsSlider;
    private AudioMixer audioMixer;

    [Inject]
    public void Construct(AudioMixer audioMixer)
    {
        this.audioMixer = audioMixer;
        Initialize();
    }

    private void Initialize()
    {
        generalSlider.value = audioMixer.GeneralVolume;
        musicSlider.value = audioMixer.MusicVolume;
        soundEffectsSlider.value = audioMixer.SoundEffectsVolume;
        generalSlider.onValueChanged.AddListener(audioMixer.OnGeneralVolumeChanged);
        musicSlider.onValueChanged.AddListener(audioMixer.OnMusicVolumeChanged);
        soundEffectsSlider.onValueChanged.AddListener(audioMixer.OnSoundEffectsVolumeChanged);
    }
}
