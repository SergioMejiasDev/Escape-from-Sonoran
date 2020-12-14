using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

/// <summary>
/// Script from which the sound settings are configured.
/// </summary>
public class AudioOptions : MonoBehaviour
{
    public static AudioOptions audioOptions;

    [SerializeField] Slider musicSlider = null, sfxSlider = null;
    [SerializeField] AudioMixerGroup musicMixer = null, sfxMixer = null;
    float musicVolume, sfxVolume;

    private void Start()
    {
        audioOptions = this;
        LoadOptions();
    }

    private void Update()
    {
        musicVolume = musicSlider.value;
        sfxVolume = sfxSlider.value;

        musicMixer.audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        sfxMixer.audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
    }

    /// <summary>
    /// Function that saves sound settings in PlayerPrefs.
    /// </summary>
    public void SaveOptions()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Function that loads the sound settings of the PlayerPrefs.
    /// </summary>
    void LoadOptions()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float musicVolumeLoaded = PlayerPrefs.GetFloat("MusicVolume");
            musicSlider.value = musicVolumeLoaded;
        }
        
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            float sfxVolumeLoaded = PlayerPrefs.GetFloat("SFXVolume");
            sfxSlider.value = sfxVolumeLoaded;
        }
    }
}



