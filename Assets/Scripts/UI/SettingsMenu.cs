using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// This class manages mostly the reset of the player's progress.
/// </summary>
public class SettingsMenu : MonoBehaviour
{
    /// <summary>
    /// The AudioMixer used for adjusting audio settings.
    /// </summary>
    [SerializeField] AudioMixer audioMixer;

    /// <summary>
    /// The ModalDialog used for displaying alerts.
    /// </summary>
    [SerializeField] ModalDialog alert;

    /// <summary>
    /// Shows an alert by playing a button click sound and displaying the dialog.
    /// </summary>
    public void ShowAlert()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        FindObjectOfType<ModalDialog>().ShowDialog();
    }

    /// <summary>
    /// Resets the progress by resetting player preferences for various settings and values.
    /// </summary>
    public void ResetProgress()
    {
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

    /// <summary>
    /// Opens the dialog by showing the associated alert.
    /// </summary>
    public void OpenDialog()
    {
        alert.ShowDialog();
    }
}
