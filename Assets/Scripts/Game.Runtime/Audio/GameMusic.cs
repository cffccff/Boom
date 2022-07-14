using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;
public class GameMusic : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;
    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioSource audioSource;
  //  public AudioMixerSnapshot audioMixerSnapshotDead;
  //  public AudioMixerSnapshot defaultSnapShot;
    const string MIXER_MUSIC = "MusicVolume";
    const string MIXER_SFX = "SFXVolume";
    [SerializeField] AudioClip[] audioClips;
    AudioClip clipPlay;
    public static GameMusic Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);

            
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
       
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
       // defaultSnapShot.TransitionTo(0.001f);
    }

    private void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC,Mathf.Log10(value)*20);
        if (musicSlider.value <= 0.005)
        {
            mixer.SetFloat(MIXER_MUSIC, -80);
        }
    }
    private void SetSFXVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
        if (SFXSlider.value <= 0.005)
        {
            mixer.SetFloat(MIXER_SFX, -80);
        }
    }
    void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
        else
        {
            musicSlider.value = musicSlider.maxValue;
            SFXSlider.value = SFXSlider.maxValue;
            PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
            PlayerPrefs.SetFloat("MusicVolume", SFXSlider.value);
        }
        SetMusicVolume(musicSlider.value);
        SetSFXVolume(SFXSlider.value);
        PlayMusicBackGround();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void PlayMusicBackGround()
    {
        audioSource.Stop();
        clipPlay = audioClips[0];
        audioSource.clip = clipPlay;
        audioSource.Play();
    }
    public void PlayMusicBattle()
    {
        audioSource.Stop();
        clipPlay = audioClips[1];
        audioSource.clip = clipPlay;
        audioSource.Play();

    }
}
