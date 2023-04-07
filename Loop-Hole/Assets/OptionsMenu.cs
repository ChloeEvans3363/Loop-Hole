using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject optionsPanel;

    // Start is called before the first frame update
    void Start()
    {
        
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
