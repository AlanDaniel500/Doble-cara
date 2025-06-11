using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private const string MasterPref = "MasterVolume";
    private const string MusicPref = "MusicVolume";
    private const string SFXPref = "SFXVolume";

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Cargar valores guardados o asignar por defecto
        float masterVolume = PlayerPrefs.GetFloat(MasterPref, 1f);
        float musicVolume = PlayerPrefs.GetFloat(MusicPref, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFXPref, 1f);

        masterSlider.value = masterVolume;
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;

        masterSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    private void OnMasterVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat(MasterPref, value);
        PlayerPrefs.Save();
        Debug.Log("Master Volume: " + value);
    }

    private void OnMusicVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat(MusicPref, value);
        PlayerPrefs.Save();
        Debug.Log("Music Volume: " + value);
    }

    private void OnSFXVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat(SFXPref, value);
        PlayerPrefs.Save();
        Debug.Log("SFX Volume: " + value);
    }
}
