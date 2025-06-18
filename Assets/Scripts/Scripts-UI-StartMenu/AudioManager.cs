using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sliders de volumen")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer myMixer;

    [Header("Música por escena")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameMusic;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetMasterVolume();
            SetSFXVolume();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Asignar sliders desde la escena
        masterSlider = GameObject.Find("Master_Slider")?.GetComponent<Slider>();
        musicSlider = GameObject.Find("Music_Slider")?.GetComponent<Slider>();
        sfxSlider = GameObject.Find("SFX_Slider")?.GetComponent<Slider>();

        if (masterSlider != null)
        {
            masterSlider.onValueChanged.AddListener((v) => SetMasterVolume());
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        }

        if (musicSlider != null)
        {
            musicSlider.onValueChanged.AddListener((v) => SetMusicVolume());
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        }

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.AddListener((v) => SetSFXVolume());
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        }

        // Cambiar la música según la escena
        if (musicSource != null)
        {
            if (scene.name == "StartMenu" && musicSource.clip != menuMusic)
            {
                musicSource.clip = menuMusic;
                musicSource.Play();
            }
            else if (scene.name == "GAME" && musicSource.clip != gameMusic)
            {
                musicSource.clip = gameMusic;
                musicSource.Play();
            }
        }
    }

    public void SetMasterVolume()
    {
        if (masterSlider == null) return;

        float volume = Mathf.Clamp(masterSlider.value, 0.001f, 1f);
        myMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume()
    {
        if (musicSlider == null) return;

        float volume = Mathf.Clamp(musicSlider.value, 0.001f, 1f);
        myMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume()
    {
        if (sfxSlider == null) return;

        float volume = Mathf.Clamp(sfxSlider.value, 0.001f, 1f);
        myMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadVolume()
    {
        float master = PlayerPrefs.GetFloat("MasterVolume", 1f);
        float music = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfx = PlayerPrefs.GetFloat("SFXVolume", 1f);

        myMixer.SetFloat("MasterVolume", Mathf.Log10(master) * 20);
        myMixer.SetFloat("MusicVolume", Mathf.Log10(music) * 20);
        myMixer.SetFloat("SFXVolume", Mathf.Log10(sfx) * 20);
    }
}
