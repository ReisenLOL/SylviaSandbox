using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    #region Statication

    public static SettingsMenu instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    #endregion
    [SerializeField] private TextMeshProUGUI sfxLabel;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TextMeshProUGUI musicLabel;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private TextMeshProUGUI screenShakeLabel;
    [SerializeField] private Slider screenShakeSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }

        if (PlayerPrefs.HasKey("ScreenShake"))
        {
            screenShakeSlider.value = PlayerPrefs.GetFloat("ScreenShake");
        }
        UpdateSFX();
        UpdateMusic();
        UpdateScreenShake();
    }

    public void UpdateSFX()
    {
        sfxLabel.text = $"SFX ({(sfxSlider.value * 100).ToString("F1")}%);";
        mainMixer.SetFloat("SFXVolume", Mathf.Log10(sfxSlider.value) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }
    public void UpdateMusic()
    {
        musicLabel.text = $"Music ({(musicSlider.value * 100).ToString("F1")}%);";
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(musicSlider.value) * 20);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void UpdateScreenShake()
    {
        screenShakeLabel.text = $"Screen Shake: ({(screenShakeSlider.value * 100).ToString("F1")}%);";
        PlayerPrefs.SetFloat("ScreenShake", screenShakeSlider.value);
    }
}
