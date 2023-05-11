using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    private void Start()
    {
        // gather the volume in the player prefs
    }

    private void Update()
    {
        // update the volume in player prefs
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", volume);
    }

    public void SetMusicVolume(float volume) 
    {
        audioMixer.SetFloat("Music", volume);
    }

    public void SetSFXVolume(float volume) 
    {
        audioMixer.SetFloat("SFX", volume);
    }
}
