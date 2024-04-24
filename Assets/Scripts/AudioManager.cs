using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {
        get {
            if (instance == null) {
                instance = Instantiate(new GameObject("AudioManager")).AddComponent<AudioManager>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
        private set { instance = value; }
    }
    public sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    private static AudioManager instance;

    public void OnApplicationResume() {
       
    }
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
    }
    public void PlayMusic(string name) {
        if (musicSounds == null) return;
        sound s = Array.Find(musicSounds, x => x.name == name);
        if(s!=null)
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlaySfx(string name) {
        if (sfxSounds == null) return;
        sound s = Array.Find(sfxSounds, x => x.name == name);
        if(s!=null&&s.clip!=null)
        {
            sfxSource.PlayOneShot(s.clip);
        }
        else Debug.LogError($"No sound found  {name}");
    }
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
        
    }
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}

