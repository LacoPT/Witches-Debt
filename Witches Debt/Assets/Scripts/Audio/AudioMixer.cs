using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AudioMixer : MonoBehaviour
{
    [SerializeField] private Slider generalSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundEffectsSlider;
    // Should those field be saved? idk
    //[SerializeField, Range(0, 1)] private float GeneralVolume;
    //[SerializeField, Range(0, 1)] private float MusicVolume;
    //[SerializeField, Range(0, 1)] private float SoundEffectsVolume;
    public static AudioMixer Instance { get; private set; }
    public UnityEvent<float> GeneralVolumeChanged;
    public UnityEvent<float> MusicVolumeChanged;
    public UnityEvent<float> SoundEffectsVolumeChanged;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        generalSlider.onValueChanged.AddListener(OnGeneralVolumeChanged);
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        soundEffectsSlider.onValueChanged.AddListener(OnSoundEffectsVolumeChanged);
    }

    private void OnGeneralVolumeChanged(float value)
    {
        //GeneralVolume = value;
        GeneralVolumeChanged?.Invoke(value);
    }
    
    private void OnMusicVolumeChanged(float value)
    {
        //MusicVolume = value;
        MusicVolumeChanged?.Invoke(value);
    }
    
    private void OnSoundEffectsVolumeChanged(float value)
    {
        //SoundEffectsVolume = value;
        SoundEffectsVolumeChanged?.Invoke(value);
    }
}
