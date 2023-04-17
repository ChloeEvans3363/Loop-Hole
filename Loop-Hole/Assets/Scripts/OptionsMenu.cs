using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider sfxVolume;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Toggle screenshakeToggle;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("screenshakeEnabled") != 0)
            screenshakeToggle.isOn = true;
        else 
            screenshakeToggle.isOn = false;

        sfxVolume.value = PlayerPrefs.GetFloat("sfxVolume");
        musicVolume.value = PlayerPrefs.GetFloat("musicVolume");
        masterVolume.value = PlayerPrefs.GetFloat("masterVolume");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMasterVolume(float value)
    {
        PlayerPrefs.SetFloat("masterVolume", value);
    }

    public void SetSFXVolume(float value)
    {
        PlayerPrefs.SetFloat("sfxVolume", value);
    }

    public void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("musicVolume", value);
    }

    public void ReturnToMain()
    {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
        PlayerPrefs.Save();
    }

    public void GoToOptions()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void ToggleScreenshake(bool shake)
    {
        if(shake)
        {
            PlayerPrefs.SetInt("screenshakeEnabled", 1);
        } else
        {
            PlayerPrefs.SetInt("screenshakeEnabled", 0);
        }
    }
}
