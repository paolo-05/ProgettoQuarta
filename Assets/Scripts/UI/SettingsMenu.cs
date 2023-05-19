using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] ModalDialog alert;

    public void ShowAlert()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        FindObjectOfType<ModalDialog>().ShowDialog();
    }

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
    public void OpenDialog()
    {
        alert.ShowDialog();
    }
}
