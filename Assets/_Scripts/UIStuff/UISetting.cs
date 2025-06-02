using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UISetting : UICanvas
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private AudioMixer audioMixer;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    private void Start()
    {
        float volume = PlayerPrefs.GetFloat("musicVolume");
        if (volume != 0)
        {
            SetMusicVolume(volume);
            musicVolumeSlider.value = volume;
        }
        volume = PlayerPrefs.GetFloat("sfxVolume");
        if (volume != 0)
        {
            SetSFXVolume(volume);
            sfxVolumeSlider.value = volume;
        }
    }

    public void BackButton()
    {
        Close(0);
    }

    public void ToggleFullscreen()
    {
        if (toggle.isOn)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            Screen.SetResolution(1920 * 2 / 3, 1080 * 2 / 3, false);
        }

    }
    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log(value)*20);
        PlayerPrefs.SetFloat("musicVolume", Mathf.Log(value) * 20);
    }
    public void SetSFXVolume(float value)
    {
        audioMixer.SetFloat("sfxVolume", Mathf.Log(value) * 20);
        PlayerPrefs.SetFloat("sfxVolume", Mathf.Log(value) * 20);
    }
}
