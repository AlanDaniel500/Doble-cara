using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private float masterVolume = 1f;
    private float musicVolume = 1f;
    private float sfxVolume = 1f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        masterSlider.value = masterVolume;
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;

        masterSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    private void OnMasterVolumeChanged(float value)
    {
        masterVolume = value;
        Debug.Log("Master Volume: " + masterVolume);
    }

    private void OnMusicVolumeChanged(float value)
    {
        musicVolume = value;
        Debug.Log("Music Volume: " + musicVolume);
    }

    private void OnSFXVolumeChanged(float value)
    {
        sfxVolume = value;
        Debug.Log("SFX Volume: " + sfxVolume);
    }
}
