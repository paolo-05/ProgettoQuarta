using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

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

    public void ResetProgress()
    {
        // Display confirmation alert
        //bool confirmed = EditorUtility.DisplayDialog("Reset Progress", "Are you sure you want to reset your progress?", "Yes", "No");
        //if (!confirmed)
        //{
        //    return;
        //}

        // Reset player prefs
        PlayerPrefs.SetFloat("BulletSpeed", 100f);
        PlayerPrefs.SetFloat("BulletSpeedCost", 20f);

        PlayerPrefs.SetFloat("BulletRange", 20f);
        PlayerPrefs.SetFloat("BulletRangeCost", 20f);

        PlayerPrefs.SetFloat("BulletDamage", 30f);
        PlayerPrefs.SetFloat("BulletDamageCost", 20f);
        
        PlayerPrefs.SetFloat("FireRate", 0.5f);
        PlayerPrefs.SetFloat("FireRateCost", 20f);

        PlayerPrefs.SetInt("Coins", 0);
        PlayerPrefs.SetInt("PersonalBest", 0);
    }
}
