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
        for(int i = 0; i < sounds.Length; i++)
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
            i++;
        }
    }
}
