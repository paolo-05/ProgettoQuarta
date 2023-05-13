using System;
using UnityEngine;
using UnityEngine.UI;

public class Purchase : MonoBehaviour
{
    // this code is used for the purchase zone and its quite boring.
    // we use UI elements for managing the upgrades and the player prefs to store them

    // Serialized fields can be set in the Unity editor
    [SerializeField] Button upgradeBulletSpeed;
    [SerializeField] Text bulletSpeedText;
    [SerializeField] Text bulletSpeedCostText;

    [SerializeField] Button upgradeBulletDamage;
    [SerializeField] Text bulletDamageText;
    [SerializeField] Text bulletDamageCostText;

    [SerializeField] Button upgradeBulletRange;
    [SerializeField] Text bulletRangeText;
    [SerializeField] Text bulletRangeCostText;

    [SerializeField] Button upgradeFireRate;
    [SerializeField] Text fireRateText;
    [SerializeField] Text fireRateCostText;


    [SerializeField] GunController gunController;

    private int coins; // The number of coins the player has

    private float bulletSpeed; // The current speed of the bullets
    private float bulletSpeedCost; // The cost to upgrade the bullet speed

    private float bulletDamage; // The current damage of the bullet
    private float bulletDamageCost; // the cost for upgrading it

    private float bulletRange; // The current range of the bullet
    private float bulletRangeCost; // the cost for upgrading it

    private float fireRate;// The current fire rate
    private float fireRateCost; // the cost for upgrading it

    // Upgrade cost multiplier
    public float upgradeCostMultiplier = 1.2f;

    // Fire rate decrease amount
    public float fireRateDecreaseAmount = 0.05f;

    void Start()
    {
        // When the player loads the game the first time
        if (!PlayerPrefs.HasKey("BulletSpeed"))
        {
            PlayerPrefs.SetFloat("BulletSpeed", 100f);
            PlayerPrefs.SetFloat("BulletRange", 20f);
            PlayerPrefs.SetFloat("BulletDamage", 30f);
            PlayerPrefs.SetFloat("FireRate", 0.5f);
        }

        // Set the initial fire rate
        fireRate = 0.5f;
        gunController.fireRate = fireRate;
        fireRateText.text = "Fire rate: " + fireRate;

        // Set the initial fire rate cost
        fireRateCost = 10f;
        fireRateCostText.text = "Upgrade cost: " + Math.Round(fireRateCost);
    }

    private void Update()
    {
        // Get the number of coins the player has from player preferences
        coins = PlayerPrefs.GetInt("Coins", 0);

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
        if (gunController.bulletSpeed >= 500)
        {
            bulletSpeedCostText.text = "MAX";
            upgradeBulletSpeed.interactable = false;
        }

        // Doing the same for for all the upgrades

        // Get the current bullet damage from player preferences
        bulletDamage = PlayerPrefs.GetFloat("BulletDamage", 30f);
        // Set the bullet damage of the gun controller
        gunController.bulletRange = bulletDamage;
        // Update the bullet damage text to display the current bullet damage rounded to the nearest integer
        bulletDamageText.text = "Bullet damage: " + Math.Round(bulletDamage);

        // Calculate the cost to upgrade the bullet damage as 10% of the current bullet damage
        bulletDamageCost = PlayerPrefs.GetFloat("BulletDamage", 30) * 0.1f;
        // Update the bullet damage cost text to display the upgrade cost rounded to the nearest integer
        bulletDamageCostText.text = "Upgrade cost: " + Math.Round(bulletDamageCost);

        // If the player doesn't have enough coins to upgrade the bullet damage, disable the upgrade button
        if (coins < bulletDamageCost)
        {
            upgradeBulletDamage.interactable = false;
        }
        // Otherwise, enable the upgrade button
        else
        {
            upgradeBulletDamage.interactable = true;
        }
        if (gunController.bulletDamage >= 300)
        {
            bulletDamageCostText.text = "MAX";
            upgradeBulletDamage.interactable = false;
        }

        // Get the current bullet range from player preferences
        bulletRange = PlayerPrefs.GetFloat("BulletRange", 20f);
        // Set the bullet range of the gun controller
        gunController.bulletRange = bulletRange;
        // Update the bullet range text to display the current bullet range rounded to the nearest integer
        bulletRangeText.text = "Bullet range: " + Math.Round(bulletRange);

        // Calculate the cost to upgrade the bullet range as 20% of the current bullet range
        bulletRangeCost = PlayerPrefs.GetFloat("BulletRange", 20f) * 0.2f;
        // Update the bullet range cost text to display the upgrade cost rounded to the nearest integer
        bulletRangeCostText.text = "Upgrade cost: " + Math.Round(bulletRangeCost);
        // If the player doesn't have enough coins to upgrade the bullet range, disable the upgrade button
        if (coins < bulletRangeCost)
        {
            upgradeBulletRange.interactable = false;
        }
        // Otherwise, enable the upgrade button
        else
        {
            upgradeBulletRange.interactable = true;
        }
        if (gunController.bulletRange >= 200)
        {
            bulletRangeCostText.text = "MAX";
            upgradeBulletRange.interactable = false;
        }

        // fire rate controls
        if (coins > fireRateCost)
        {
            upgradeFireRate.interactable = true;
        }
        else
        {
            upgradeFireRate.interactable = false;
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

    public void UpgradeBulletDamage()
    {
        // Play the sound
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        // If the player has enough coins to upgrade the bullet damage, perform the upgrade
        if (coins > bulletDamageCost)
        {
            // Subtract the upgrade cost from the player's coins
            coins -= (int)bulletDamageCost;
            // Save the updated number of coins to player preferences
            PlayerPrefs.SetInt("Coins", coins);
            // Increase the bullet damage of the gun controller by the upgrade cost
            gunController.bulletDamage += bulletDamageCost;
            // Save the updated bullet damage to player preferences
            PlayerPrefs.SetFloat("BulletDamage", gunController.bulletDamage);
        }
    }

    public void UpgradeBulletRange()
    {
        // Play the sound
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        // If the player has enough coins to upgrade the bullet range, perform the upgrade
        if (coins > bulletRangeCost)
        {
            // Subtract the upgrade cost from the player's coins
            coins -= (int)bulletRangeCost;
            // Save the updated number of coins to player preferences
            PlayerPrefs.SetInt("Coins", coins);
            // Increase the bullet damage of the gun controller by the upgrade cost only if it's less than 200
            gunController.bulletRange += bulletRangeCost;
            if (gunController.bulletRange > 200)
            {
                gunController.bulletRange = 200;
            }
            // Save the updated bullet damage to player preferences
            PlayerPrefs.SetFloat("BulletDamage", gunController.bulletRange);
        }
    }

    public void UpgradeFireRate()
    {
        // Play the sound
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        // Deduct the cost of the upgrade from the player's coins
        coins -= (int)fireRateCost;
        PlayerPrefs.SetInt("Coins", coins);

        // Increase the fire rate cost
        fireRateCost *= upgradeCostMultiplier;
        fireRateCostText.text = "Upgrade cost: " + Math.Round(fireRateCost);

        // Decrease the fire rate
        fireRate -= fireRateDecreaseAmount;
        if (fireRate <= 0.05f)
        {
            fireRate = 0.05f;
            fireRateCost = 0;
            fireRateCostText.text = "MAX";
            upgradeFireRate.interactable = false;

        }
        PlayerPrefs.SetFloat("FireRate", fireRate);
        gunController.fireRate = fireRate;
        fireRateText.text = "Fire rate: " + fireRate;



        // Disable the upgrade button if the player doesn't have enough coins
        if (coins < fireRateCost)
        {
            upgradeFireRate.interactable = false;
        }
    }
}
