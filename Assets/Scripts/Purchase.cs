using System;
using UnityEngine;
using UnityEngine.UI;

public class Purchase : MonoBehaviour
{
    [SerializeField] Button upgradeBulletSpeed;
    [SerializeField] Text bulletSpeedText;
    [SerializeField] Text bulletSpeedCostText;
    [SerializeField] GunController gunController;

    private int coins;
    private float bulletSpeed;
    private float bulletSpeedCost;
    void Start()
    {
    }

    private void Update()
    {
        bulletSpeed = PlayerPrefs.GetFloat("BulletSpeed", 100);
        gunController.bulletSpeed = bulletSpeed;
        bulletSpeedText.text = "Bullet speed: " + Math.Round(bulletSpeed);

        bulletSpeedCost = PlayerPrefs.GetFloat("BulletSpeed", 100) * 0.2f;
        bulletSpeedCostText.text = "Upgrade cost: " + Math.Round(bulletSpeedCost);

        coins = PlayerPrefs.GetInt("Coins", 0);

        if (coins < bulletSpeedCost)
        {
            upgradeBulletSpeed.interactable = false;
        }
        else
        {
            upgradeBulletSpeed.interactable = true;
        }
    }

    public void UpgradeBulletSpeed()
    {
        if (coins > bulletSpeedCost)
        { 
            coins -= (int)bulletSpeedCost;
            PlayerPrefs.SetInt("Coins", coins);
            gunController.bulletSpeed += bulletSpeedCost;
            PlayerPrefs.SetFloat("BulletSpeed", gunController.bulletSpeed);
        }
    }
}
