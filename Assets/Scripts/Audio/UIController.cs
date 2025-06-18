using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider Music_Slider, SFX_Slider;

    private void Start()
    {

        // Cargar valores al iniciar el panel
        if (Music_Slider != null)
        {
            float musicVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
            Music_Slider.value = musicVol;
            if (AudioManager.Instance != null)
                AudioManager.Instance.SetMusicVolume(musicVol);
        }

        /*if (SFX_Slider != null)
        {
            float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 1f);
            SFX_Slider.value = sfxVol;
            AudioManager.Instance.SetSFXVolume(sfxVol);
        }*/
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        //AudioManager.Instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMusicVolume(Music_Slider.value);
        }
    }

    /*public void SFXVolume()
    {
        if (SFX_Slider != null)
            AudioManager.Instance.SetSFXVolume(SFX_Slider.value);
    }*/
}

