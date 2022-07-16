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
    public static GameMusic Instance;
    private void Awake()
    {
        //Singleton method
        if (Instance == null)
        {
            //First run, set the instance
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else if (Instance != this)
        {
            //Instance is not the same as the one we have, destroy old one, and reset to newest one
            Destroy(Instance.gameObject);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        AddEventListen();
       // defaultSnapShot.TransitionTo(0.001f);
    }
    public void AddEventListen()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }
    public void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC,Mathf.Log10(value)*20);
        if (musicSlider.value <= 0.005)
        {
            mixer.SetFloat(MIXER_MUSIC, -80);
        }
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
    }
    public void SetSFXVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
        if (SFXSlider.value <= 0.005)
        {
            mixer.SetFloat(MIXER_SFX, -80);
        }
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();
    }
    void Start()
    {

        DisplaySliderValue();
        SetValueSliderMixer();
        PlayMusicBackGround();
    }

    // Update is called once per frame
   public void DisplaySliderValue()
    {
       
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
            Debug.Log("1");
        }
        else
        {
            Debug.Log("2");
            musicSlider.value = musicSlider.maxValue;
            SFXSlider.value = SFXSlider.maxValue;
            PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
            PlayerPrefs.SetFloat("SFXVolume", SFXSlider.value);
            PlayerPrefs.Save();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("MusicVolume: " + PlayerPrefs.GetFloat("MusicVolume"));
        }
        
    }
    public void SetValueSliderMixer()
    {
        SetMusicVolume(musicSlider.value);
        SetSFXVolume(SFXSlider.value);
    }
   public void PlayMusicBackGround()
    {
        audioSource.Stop();
        clipPlay = audioClips[0];
        audioSource.clip = clipPlay;
        audioSource.Play();
        audioSource.loop = true;
    }
    public void PlayMusicBattle()
    {
        audioSource.Stop();
        clipPlay = audioClips[1];
        audioSource.clip = clipPlay;
        audioSource.Play();
        audioSource.loop = true;
    }
    public void GetSlider()
    {
        try
        {
            musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
            SFXSlider = GameObject.Find("SFXSlider").GetComponent<Slider>();
        }
        catch (NullReferenceException)
        {

        }
    }
}
