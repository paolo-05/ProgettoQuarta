using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Manages audio playback and volume control.
/// </summary>
public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds; // Array of sound effects
    [SerializeField] private Sound[] musics; // Array of music tracks

    [SerializeField] private AudioMixer audioMixer; // Reference to the audio mixer

    [SerializeField] private AudioMixerGroup music; // Audio mixer group for music
    [SerializeField] private AudioMixerGroup SFX; // Audio mixer group for sound effects

    private float volume; // Current volume level

    [SerializeField] private Slider masterSlider; // Slider for master volume
    [SerializeField] private Slider musicSlider; // Slider for music volume
    [SerializeField] private Slider SFXSlider; // Slider for sound effects volume

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

    /// <summary>
    /// Plays a sound effect by name.
    /// </summary>
    /// <param name="name">The name of the sound effect.</param>
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Play();
    }

    /// <summary>
    /// Plays a random music track.
    /// </summary>
    public void PlayMusic()
    {
        int index = UnityEngine.Random.Range(0, musics.Length);
        Sound s = musics[index];
        s.source.Play();
    }

    /// <summary>
    /// Sets the master volume level.
    /// </summary>
    /// <param name="volume">The volume level (range: -80 to 0).</param>
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    /// <summary>
    /// Sets the music volume level.
    /// </summary>
    /// <param name="volume">The volume level (range: -80 to 0).</param>
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    /// <summary>
    /// Sets the sound effects volume level.
    /// </summary>
    /// <param name="volume">The volume level (range: -80 to 0).</param>
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
}
