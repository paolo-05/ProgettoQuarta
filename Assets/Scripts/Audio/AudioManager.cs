using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Sound[] sounds;
    [SerializeField]
    private Sound[] musics;

    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private AudioMixerGroup music;
    [SerializeField]
    private AudioMixerGroup SFX;

    private float volume;


    [SerializeField]
    private Slider masterSlider;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider SFXSlider;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = SFX;
        }
        foreach (Sound s in musics)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = music;
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            if (masterSlider != null) masterSlider.value = masterVolume;
            audioMixer.SetFloat("Master", masterVolume);
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            if (musicSlider != null) musicSlider.value = musicVolume;
            audioMixer.SetFloat("Music", musicVolume);
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            float sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
            if (SFXSlider != null) SFXSlider.value = sfxVolume;
            audioMixer.SetFloat("SFX", sfxVolume);
        }

        PlayMusic();
    }

    private void Update()
    {
        audioMixer.GetFloat("Master", out volume);
        PlayerPrefs.SetFloat("Master", volume);
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Play();
    }

    public void PlayMusic()
    {
        int index = UnityEngine.Random.Range(0, musics.Length);
        Sound s = musics[index];
        s.source.Play();
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
}
