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
        UpdateSFX();
        UpdateMusic();
    }

    public void UpdateSFX()
    {
        sfxLabel.text = $"SFX ({sfxSlider.value * 100}%);";
        mainMixer.SetFloat("SFXVolume", Mathf.Log10(sfxSlider.value) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }
    public void UpdateMusic()
    {
        musicLabel.text = $"Music ({musicSlider.value * 100}%);";
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(musicSlider.value) * 20);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }
}
