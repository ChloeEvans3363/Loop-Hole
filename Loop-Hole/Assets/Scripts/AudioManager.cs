using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public Sound[] sounds;
    public float[] soundVolDefaults;

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Play();
    }

    public void Pause (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Stop();
    }

    public void SetVolumes()
    {
        float masterVol = PlayerPrefs.GetFloat("masterVolume");
        float sfxVol = PlayerPrefs.GetFloat("sfxVolume");
        float musVol = PlayerPrefs.GetFloat("musicVolume");
        if(sounds.Length == soundVolDefaults.Length)
        {
            for(int i = 0; i < sounds.Length; i++)
            {
               if(i < 5) //first 5 sounds are sfx, last two are music
               {
                    sounds[i].volume = soundVolDefaults[i] * sfxVol * masterVol;
               } 
               else
               {
                    sounds[i].volume = soundVolDefaults[i] * musVol * masterVol;
               }
               //sounds[i].source.volume = sounds[i].volume; //I don't actually know if this line works or is necessary
            }
        }
    }
}
