using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AudioMixer : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField, Range(0, 1)] public float GeneralVolume;
    public float MusicVolume;
    public float SoundEffectsVolume;
    public static AudioMixer Instance { get; private set; }
    public UnityEvent<float> GeneralVolumeChanged;
    public UnityEvent<float> SFXVolumeChanged;
    public UnityEvent<float> MusicVolumeChanged;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(float value)
    {
        GeneralVolume = value;
        GeneralVolumeChanged?.Invoke(value);
    }
    
}
