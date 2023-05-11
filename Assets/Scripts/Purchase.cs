using System;
using UnityEngine;
using UnityEngine.UI;

public class Purchase : MonoBehaviour
{
    // Serialized fields can be set in the Unity editor
    [SerializeField] Button upgradeBulletSpeed;
    [SerializeField] Text bulletSpeedText;
    [SerializeField] Text bulletSpeedCostText;
    [SerializeField] GunController gunController;

    private int coins; // The number of coins the player has
    private float bulletSpeed; // The current speed of the bullets
    private float bulletSpeedCost; // The cost to upgrade the bullet speed
    void Start()
    {
        // This method is currently empty
    }

    private void Update()
    {
        // Get the current bullet speed from player preferences
        bulletSpeed = PlayerPrefs.GetFloat("BulletSpeed", 100);
        // Set the bullet speed of the gun controller
        gunController.bulletSpeed = bulletSpeed;
        // Update the bullet speed text to display the current bullet speed rounded to the nearest integer
        bulletSpeedText.text = "Bullet speed: " + Math.Round(bulletSpeed);

        // Calculate the cost to upgrade the bullet speed as 20% of the current bullet speed
        bulletSpeedCost = PlayerPrefs.GetFloat("BulletSpeed", 100) * 0.2f;
        // Update the bullet speed cost text to display the upgrade cost rounded to the nearest integer
        bulletSpeedCostText.text = "Upgrade cost: " + Math.Round(bulletSpeedCost);

        // Get the number of coins the player has from player preferences
        coins = PlayerPrefs.GetInt("Coins", 0);

        // If the player doesn't have enough coins to upgrade the bullet speed, disable the upgrade button
        if (coins < bulletSpeedCost)
        {
            upgradeBulletSpeed.interactable = false;
        }
        // Otherwise, enable the upgrade button
        else
        {
            upgradeBulletSpeed.interactable = true;
        }
    }

    public void UpgradeBulletSpeed()
    {
        // Play the sound
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        // If the player has enough coins to upgrade the bullet speed, perform the upgrade
        if (coins > bulletSpeedCost)
        {
            // Subtract the upgrade cost from the player's coins
            coins -= (int)bulletSpeedCost;
            // Save the updated number of coins to player preferences
            PlayerPrefs.SetInt("Coins", coins);
            // Increase the bullet speed of the gun controller by the upgrade cost
            gunController.bulletSpeed += bulletSpeedCost;
            // Save the updated bullet speed to player preferences
            PlayerPrefs.SetFloat("BulletSpeed", gunController.bulletSpeed);
        }
    }
}
