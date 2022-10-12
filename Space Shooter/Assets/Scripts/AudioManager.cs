using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup soundEffectMixerGroup;
    [SerializeField] private Sound[] sounds;

    // Start is called before the first frame update 
    void Awake()
    {
        instance = this;

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            s.source.loop = s.isLoop;
            s.source.volume = s.volume;

            switch (s.audioType)
            {
                case Sound.AudioTypes.music:
                s.source.outputAudioMixerGroup = musicMixerGroup;
                break;
                case Sound.AudioTypes.soundEffect:
                s.source.outputAudioMixerGroup = soundEffectMixerGroup;
                break;
            }
        }
    }

    public void Play (string clipname)
    {
        Sound s = Array.Find(sounds, dummySound => dummySound.clipName == clipname);
        if(s == null)
        {
            Debug.LogError("Sound" + clipname + " does not exist!");
            return;
        }
        s.source.Play();
    }

    public void Stop (string clipname)
    {
        Sound s = Array.Find(sounds, dummySound => dummySound.clipName == clipname);
        if(s == null)
        {
            Debug.LogError("Sound" + clipname + " does not exist!");
        }
        s.source.Stop();
    }

    public void UpdateMusicMixerVolume()
    {
        musicMixerGroup.audioMixer.SetFloat("Music Volume", Mathf.Log10(AudioOptionsManager.musicVolume) * 20);
    }
    public void UpdateSoundEffectMixerVolume()
    {
        soundEffectMixerGroup.audioMixer.SetFloat("Sound Effect Volume", Mathf.Log10(AudioOptionsManager.soundEffectVolume) * 20); 
    }
}
