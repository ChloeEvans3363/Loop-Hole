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
    [SerializeField] private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUI()
    {
        if (PlayerPrefs.HasKey("screenshakeEnabled"))
        {
            if (PlayerPrefs.GetInt("screenshakeEnabled") != 0)
            {
                screenshakeToggle.isOn = true;
            }
            else
            {
                screenshakeToggle.isOn = false;
            }   
        } else 
        {
            Debug.Log("screenshakeEnabled key doesn't exist!");
        }
        
        if(PlayerPrefs.HasKey("sfxVolume"))
        {
            sfxVolume.value = PlayerPrefs.GetFloat("sfxVolume");
        }

        if(PlayerPrefs.HasKey("musicVolume"))
        {
            musicVolume.value = PlayerPrefs.GetFloat("musicVolume");
        }

        if(PlayerPrefs.HasKey("masterVolume"))
        {
            masterVolume.value = PlayerPrefs.GetFloat("masterVolume");
            Debug.Log(PlayerPrefs.GetFloat("masterVolume"));
        }
    }

    public void SetMasterVolume(float value)
    {
        PlayerPrefs.SetFloat("masterVolume", value);
        audioManager.SetVolumes();
    }

    public void SetSFXVolume(float value)
    {
        PlayerPrefs.SetFloat("sfxVolume", value);
        audioManager.SetVolumes();
    }

    public void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("musicVolume", value);
        audioManager.SetVolumes();
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
        SetUI();
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
