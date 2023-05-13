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
    private float fireRateCost = 20; // the cost for upgrading it

    // Upgrade cost multiplier
    private readonly float upgradeCostMultiplier = 1.2f;

    // constants used for upgrades
    private readonly float bulletSpeedIncreaseAmount = 50f;
    private readonly float bulletSpeedMax = 500f;
    private readonly float bulletDamageIncreaseAmount = 5f;
    private readonly float bulletDamageMax = 150f;
    private readonly float bulletRangeIncreaseAmount = 10f;
    private readonly float bulletRangeMax = 200f;
    private readonly float fireRateDecreaseAmount = 0.05f;
    private readonly float fireRateMin = 0.05f;
    

    void Start()
    {
        // When the player loads the game the first time
        if (!PlayerPrefs.HasKey("BulletSpeed"))
        {
            PlayerPrefs.SetFloat("BulletSpeed", 100f);
            PlayerPrefs.SetFloat("BulletSpeedCost", 20f);
            PlayerPrefs.SetFloat("BulletRange", 20f);
            PlayerPrefs.SetFloat("BulletRangeCost", 20f);
            PlayerPrefs.SetFloat("BulletDamage", 30f);
            PlayerPrefs.SetFloat("BulletDamageCost", 20f);
            PlayerPrefs.SetFloat("FireRate", 0.5f);
            PlayerPrefs.SetFloat("FireRateCost", 20f);
        }
        // get te bullet stats and calculate the upgrade costs, set the current level of upgrades
        bulletSpeed = PlayerPrefs.GetFloat("BulletSpeed");
        bulletSpeedCost = PlayerPrefs.GetFloat("BulletSpeedCost");
        bulletSpeedText.text = $"Bullet Speed: {bulletSpeed}";

        bulletRange = PlayerPrefs.GetFloat("BulletRange");
        bulletRangeCost = PlayerPrefs.GetFloat("BulletRangeCost");
        bulletRangeText.text = $"Bullet Range: {bulletRange}";
            
        bulletDamage = PlayerPrefs.GetFloat("BulletDamage");
        bulletDamageCost = PlayerPrefs.GetFloat("BulletDamageCost");
        bulletDamageText.text = $"Bullet Damage: {bulletDamage}";

        fireRate = PlayerPrefs.GetFloat("FireRate");
        fireRateCost = PlayerPrefs.GetFloat("FireRateCost");
        fireRateText.text = $"Fire rate: {Math.Round(fireRate, 3)}";
    }

    private void Update()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);

        // check if the player maxed out bullet speed
        if (bulletSpeed == bulletSpeedMax)
        {
            bulletRangeCostText.text = "MAX";
            upgradeBulletSpeed.interactable = false;
        }
        else
        {
            bulletSpeedCostText.text = $"Upgrade cost: {Math.Round(bulletSpeedCost)}";
            // check if the player can upgrade
            if (coins > bulletSpeedCost)
            {
                upgradeBulletSpeed.interactable = true;
            }
        }

        // check if the player maxed out bullet damage
        if (bulletDamage == bulletDamageMax)
        {
            bulletDamageCostText.text = "MAX";
            upgradeBulletDamage.interactable = false;
        }
        else
        {
            bulletDamageCostText.text = $"Upgrade cost: {Math.Round(bulletDamageCost)}";
            // check if the player can upgrade
            if (coins > bulletDamageCost)
            {
                upgradeBulletDamage.interactable = true;
            }
        }

        // check if the player maxed out bullet range
        if (bulletRange == bulletRangeMax)
        {
            bulletRangeCostText.text = "MAX";
            upgradeBulletRange.interactable = false;
        }
        else
        {
            bulletRangeCostText.text = $"Upgrade cost: {Math.Round(bulletRangeCost)}";
            // check if the player can upgrade
            if (coins > bulletRangeCost)
            {
                upgradeBulletRange.interactable = true;
            }
        }

        // check if the player maxed out fire rate
        if (fireRate == fireRateMin)
        {
            fireRateCostText.text = "MAX";
            upgradeFireRate.interactable = false;
        }
        else
        {
            fireRateCostText.text = $"Upgrade cost: {Math.Round(fireRateCost, 3)}";
            // check if the player can upgrade
            if (coins > bulletRangeCost)
            {
                upgradeFireRate.interactable = true;
            }
        }
    }

    public void UpgradeBulletSpeed()
    {
        // Play the sound
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        // Increase
        bulletSpeed += bulletSpeedIncreaseAmount;

        if (bulletSpeed >= bulletSpeedMax)
        {
            bulletSpeed = bulletSpeedMax;
            bulletSpeedCostText.text = "MAX";
            upgradeBulletSpeed.interactable = false;

            PlayerPrefs.SetFloat("BulletSpeed", bulletSpeed);
            gunController.bulletSpeed = bulletSpeed;
            bulletSpeedText.text = "Bullet Speed: " + bulletSpeed;
            return;
        }

        // Deduct the cost of the upgrade from the player's coins
        coins -= (int)bulletSpeedCost;
        if (coins < 0)
        {
            coins += (int)bulletSpeedCost;
            PlayerPrefs.SetInt("Coins", coins);
            return;
        }
        PlayerPrefs.SetInt("Coins", coins);

        // Increase the cost
        bulletSpeedCost *= upgradeCostMultiplier;
        bulletSpeedCostText.text = "Upgrade cost: " + Math.Round(bulletSpeedCost);
        PlayerPrefs.SetFloat("BulletSpeedCost", bulletSpeedCost);

        PlayerPrefs.SetFloat("BulletSpeed", bulletSpeed);
        gunController.bulletSpeed = bulletSpeed;
        bulletSpeedText.text = "Bullet Speed: " + bulletSpeed;

        // Disable the upgrade button if the player doesn't have enough coins or if the player maxed out the possible upgrades
        if (coins < bulletSpeedCost || bulletSpeed == bulletSpeedMax)
        {
            upgradeBulletSpeed.interactable = false;
        }
    }

    public void UpgradeBulletDamage()
    {
        // Play the sound
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        // Increase
        bulletDamage += bulletDamageIncreaseAmount;

        if (bulletDamage >= bulletDamageMax)
        {
            bulletDamage = bulletDamageMax;
            bulletDamageCostText.text = "MAX";
            upgradeBulletDamage.interactable = false;

            PlayerPrefs.SetFloat("BulletDamage", bulletDamage);
            gunController.bulletDamage = bulletDamage;
            bulletDamageText.text = "Bullet Damage: " + bulletDamage;
            return;
        }

        // Deduct the cost of the upgrade from the player's coins
        coins -= (int)bulletDamageCost;
        if (coins < 0)
        {
            coins += (int)bulletDamageCost;
            PlayerPrefs.SetInt("Coins", coins);
            return;
        }
        PlayerPrefs.SetInt("Coins", coins);

        // Increase the cost
        bulletDamageCost *= upgradeCostMultiplier;
        bulletDamageCostText.text = "Upgrade cost: " + Math.Round(bulletDamageCost);
        PlayerPrefs.SetFloat("BulletDamageCost", bulletDamageCost);

        PlayerPrefs.SetFloat("BulletDamage", bulletDamage);
        gunController.bulletDamage = bulletDamage;
        bulletDamageText.text = "Bullet Damage: " + bulletDamage;

        // Disable the upgrade button if the player doesn't have enough coins or if the player maxed out the possible upgrades
        if (coins < bulletDamageCost || bulletDamage == bulletDamageMax)
        {
            upgradeBulletDamage.interactable = false;
        }
    }

    public void UpgradeBulletRange()
    {
        // Play the sound
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        // Increase
        bulletRange += bulletRangeIncreaseAmount;

        if (bulletRange >= bulletRangeMax)
        {
            bulletRange = bulletRangeMax;
            bulletRangeCostText.text = "MAX";
            upgradeBulletRange.interactable = false;

            PlayerPrefs.SetFloat("BulletRange", bulletRange);
            gunController.bulletRange = bulletRange;
            bulletRangeText.text = "Bullet Range: " + bulletRange;
            return;
        }

        // Deduct the cost of the upgrade from the player's coins
        coins -= (int)bulletRangeCost;
        if (coins < 0)
        {
            coins += (int)bulletRangeCost;
            PlayerPrefs.SetInt("Coins", coins);
            return;
        }
        PlayerPrefs.SetInt("Coins", coins);

        // Increase the cost
        bulletRangeCost *= upgradeCostMultiplier;
        bulletRangeCostText.text = "Upgrade cost: " + Math.Round(bulletRangeCost);
        PlayerPrefs.SetFloat("BulletRangeCost", bulletRangeCost);

        PlayerPrefs.SetFloat("BulletRange", bulletRange);
        gunController.bulletRange = bulletRange;
        bulletRangeText.text = "Bullet Range: " + bulletRange;

        // Disable the upgrade button if the player doesn't have enough coins or if the player maxed out the possible upgrades
        if (coins < bulletRangeCost || bulletRange == bulletRangeMax)
        {
            upgradeBulletRange.interactable = false;
        }
    }

    public void UpgradeFireRate()
    {
        // Play the sound
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        // Decrease the fire rate
        fireRate -= fireRateDecreaseAmount;

        if (fireRate <= fireRateMin)
        {
            fireRate = fireRateMin;
            fireRateCostText.text = "MAX";
            upgradeFireRate.interactable = false;

            PlayerPrefs.SetFloat("FireRate", fireRate);
            gunController.fireRate = fireRate;
            fireRateText.text = "Fire rate: " + fireRate;
            return;
        }

        // Deduct the cost of the upgrade from the player's coins
        coins -= (int)fireRateCost;
        if (coins < 0)
        {
            coins += (int)fireRateCost;
            PlayerPrefs.SetInt("Coins", coins);
            return;
        }
        PlayerPrefs.SetInt("Coins", coins);

        // Increase the fire rate cost
        fireRateCost *= upgradeCostMultiplier;
        fireRateCostText.text = "Upgrade cost: " + Math.Round(fireRateCost);
        PlayerPrefs.SetFloat("FireRateCost", fireRateCost);

        PlayerPrefs.SetFloat("FireRate", fireRate);
        gunController.fireRate = fireRate;
        fireRateText.text = "Fire rate: " + Math.Round(fireRate, 3);

        // Disable the upgrade button if the player doesn't have enough coins or if the player maxed out the possible upgrades
        if (coins < fireRateCost || fireRate == fireRateMin)
        {
            upgradeFireRate.interactable = false;
        }
    }
}