using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Manages audio playback and volume control.
/// </summary>
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// Array of sounds effects.
    /// </summary>
    [SerializeField] private Sound[] sounds;
    /// <summary>
    /// Array of music tracks.
    /// </summary>
    [SerializeField] private Sound[] musics;

    /// <summary>
    /// Reference to the audio mixer.
    /// </summary>
    [SerializeField] private AudioMixer audioMixer;

    /// <summary>
    /// Audio mixer group for music.
    /// </summary>
    [SerializeField] private AudioMixerGroup music;

    /// <summary>
    /// Audio mixer group for sound effects.
    /// </summary>
    [SerializeField] private AudioMixerGroup SFX;

    /// <summary>
    /// Current volume level.
    /// </summary>
    private float volume;

    /// <summary>
    /// Slider for master volume.
    /// </summary>
    [SerializeField] private Slider masterSlider;

    /// <summary>
    /// Slider for music volume.
    /// </summary>
    [SerializeField] private Slider musicSlider;

    /// <summary>
    /// Slider for sound effects volume.
    /// </summary>
    [SerializeField] private Slider SFXSlider;

    /// <summary>
    /// This method is called right before the Start method. Here are loaded all the sound clips.
    /// </summary>
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

    /// <summary>
    /// This method gets from the PlayerPrefs the master, music and SFX volumes.
    /// </summary>
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
