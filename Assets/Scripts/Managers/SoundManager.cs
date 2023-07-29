using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private void Awake() { 
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AudioSource musicSource, sfxSource, busMotorSource;

    public AudioClip[] musics;
    public AudioClip[] sfxs;
    public AudioClip[] busMotorSfxs;

    public void PlayBusMotorSFX(AudioClip clip)
    {
        busMotorSource.PlayOneShot(clip);
    }
    public void StopBusMotorSFX()
    {
        busMotorSource.Stop();
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void PlayMusic(AudioClip music)
    {
        musicSource.clip = music;
        musicSource.Play();
    }
    public void StopSFX()
    {
        sfxSource.Stop();
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void MusicLoopOn()
    {
        musicSource.loop = true;
    }
    public void MusicLoopOff()
    {
        musicSource.loop = false;
    }
}
