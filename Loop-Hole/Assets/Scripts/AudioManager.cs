using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public Sound[] sounds;
    public AudioMixerGroup sfxMixer;
    public AudioMixerGroup musicMixer;
    public AudioMixerGroup masterMixer;

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

        //This Doesn't Work for some reason.
        for (int i = 0; i < sounds.Length; i++)
        {
            //values less than 5 are sound effects
            if (i < 5)
            {
                sounds[i].source.outputAudioMixerGroup = sfxMixer;
            }
            else
            {
                sounds[i].source.outputAudioMixerGroup = musicMixer;
            }
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
        float sfxVol = 3.42f;
        if(PlayerPrefs.HasKey("sfxVolume"))
        {
            sfxVol = PlayerPrefs.GetFloat("sfxVolume");
        }

        float musVol = 3.42f; 
        if(PlayerPrefs.HasKey("musicVolume"))
        {
            musVol = PlayerPrefs.GetFloat("musicVolume");
        }

        float masterVol = 3.42f; 
        if(PlayerPrefs.HasKey("masterVolume"))
        {
            masterVol = PlayerPrefs.GetFloat("masterVolume");
        }
        //"snap" to no audio if at the minimum volume for a group
        //Music and sfx slider minimums are 1.5
        if(sfxVol > 1.51f)
        {
            sfxMixer.audioMixer.SetFloat("sfxVol", Mathf.Pow(sfxVol, 3) - 40);
        } else
        {
            sfxMixer.audioMixer.SetFloat("sfxVol", -80f);
        }
        if(musVol > 1.51f)
        {
            musicMixer.audioMixer.SetFloat("musicVol", Mathf.Pow(musVol, 3) - 40);
        } else
        {
            musicMixer.audioMixer.SetFloat("musicVol", -80f);
        }
        //Master volume slider minimum is 1.5
        if(masterVol > 1.51f)
        {
            masterMixer.audioMixer.SetFloat("masterVol", Mathf.Pow(masterVol, 3) - 40);
        } else
        {
            masterMixer.audioMixer.SetFloat("masterVol", -80f);
        }
        //Debug.Log(Mathf.Pow(PlayerPrefs.GetFloat("masterVolume"), 3) - 40);
    }
}
