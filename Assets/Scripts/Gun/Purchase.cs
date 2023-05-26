using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the behavior of the purchase zone.
/// </summary>
public class Purchase : MonoBehaviour
{
    // Serialized fields can be set in the Unity editor

    /// <summary>
    /// Button for upgrading bullet speed.
    /// </summary>
    [SerializeField] Button upgradeBulletSpeed;

    /// <summary>
    /// Text displaying the current bullet speed.
    /// </summary>
    [SerializeField] Text bulletSpeedText;

    /// <summary>
    /// Text displaying the cost of upgrading bullet speed.
    /// </summary>
    [SerializeField] Text bulletSpeedCostText;

    /// <summary>
    /// Button for upgrading bullet damage.
    /// </summary>
    [SerializeField] Button upgradeBulletDamage;

    /// <summary>
    /// Text displaying the current bullet damage.
    /// </summary>
    [SerializeField] Text bulletDamageText;

    /// <summary>
    /// Text displaying the cost of upgrading bullet damage.
    /// </summary>
    [SerializeField] Text bulletDamageCostText;

    /// <summary>
    /// Button for upgrading bullet range.
    /// </summary>
    [SerializeField] Button upgradeBulletRange;

    /// <summary>
    /// Text displaying the current bullet range.
    /// </summary>
    [SerializeField] Text bulletRangeText;

    /// <summary>
    /// Text displaying the cost of upgrading bullet range.
    /// </summary>
    [SerializeField] Text bulletRangeCostText;

    /// <summary>
    /// Button for upgrading fire rate.
    /// </summary>
    [SerializeField] Button upgradeFireRate;

    /// <summary>
    /// Text displaying the current fire rate.
    /// </summary>
    [SerializeField] Text fireRateText;

    /// <summary>
    /// Text displaying the cost of upgrading fire rate.
    /// </summary>
    [SerializeField] Text fireRateCostText;

    /// <summary>
    /// Reference to the GunController.
    /// </summary>
    [SerializeField] GunController gunController;

    /// <summary>
    /// The number of coins the player has.
    /// </summary>
    private int coins;

    /// <summary>
    /// The current speed of the bullets.
    /// </summary>
    private float bulletSpeed;

    /// <summary>
    /// The cost to upgrade the bullet.
    /// </summary>
    private float bulletSpeedCost;

    /// <summary>
    /// The current damage of the bullet.
    /// </summary>
    private float bulletDamage;

    /// <summary>
    /// The cost for upgrading bullet damage.
    /// </summary>
    private float bulletDamageCost;

    /// <summary>
    /// The current range of the bullet.
    /// </summary>
    private float bulletRange;

    /// <summary>
    /// The cost for upgrading bullet range.
    /// </summary>
    private float bulletRangeCost;

    /// <summary>
    /// The current fire rate.
    /// </summary>
    private float fireRate;

    /// <summary>
    /// The cost for upgrading fire rate.
    /// </summary>
    private float fireRateCost = 20;

    /// <summary>
    /// Upgrade cost multiplier.
    /// </summary>
    private readonly float upgradeCostMultiplier = 1.2f;

    /// <summary>
    /// Constant used for upgrading the bullet speed.
    /// </summary>
    private readonly float bulletSpeedIncreaseAmount = 50f;

    /// <summary>
    /// The maximum upgrade for bullet speed.
    /// </summary>
    private readonly float bulletSpeedMax = 500f;

    /// <summary>
    /// Constant used for upgrading the bullet damage.
    /// </summary>
    private readonly float bulletDamageIncreaseAmount = 5f;

    /// <summary>
    /// The maximum upgrade for bullet damage.
    /// </summary>
    private readonly float bulletDamageMax = 150f;

    /// <summary>
    /// Constant used for upgrading the bullet range.
    /// </summary>
    private readonly float bulletRangeIncreaseAmount = 10f;

    /// <summary>
    /// The maximum upgrade for bullet range.
    /// </summary>
    private readonly float bulletRangeMax = 200f;

    /// <summary>
    /// Constant used for upgrading the fire rate.
    /// </summary>
    private readonly float fireRateDecreaseAmount = 0.05f;

    /// <summary>
    /// The maximum upgrade for fire rate.
    /// </summary>
    private readonly float fireRateMin = 0.05f;

    /// <summary>
    /// Getting from the *PlayerPrefs* the past updates of the weapon.
    /// </summary>
    void Start()
    {
        // Get the bullet stats and calculate the upgrade costs, set the current level of upgrades
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

    /// <summary>
    /// Here it's checked the number of coins and the cost of the upgrades.
    /// </summary>
    private void Update()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);

        // Check if the player has maxed out bullet speed
        if (bulletSpeed == bulletSpeedMax)
        {
            bulletRangeCostText.text = "MAX";
            upgradeBulletSpeed.interactable = false;
        }
        else
        {
            bulletSpeedCostText.text = $"Upgrade cost: {Math.Round(bulletSpeedCost)}";
        }

        // Check if the player can upgrade bullet speed
        if (coins > bulletSpeedCost)
        {
            upgradeBulletSpeed.interactable = true;
        }
        else
        {
            upgradeBulletSpeed.interactable = false;
        }

        // Check if the player has maxed out bullet damage
        if (bulletDamage == bulletDamageMax)
        {
            bulletDamageCostText.text = "MAX";
            upgradeBulletDamage.interactable = false;
        }
        else
        {
            bulletDamageCostText.text = $"Upgrade cost: {Math.Round(bulletDamageCost)}";
        }

        // Check if the player can upgrade bullet damage
        if (coins > bulletDamageCost)
        {
            upgradeBulletDamage.interactable = true;
        }
        else
        {
            upgradeBulletDamage.interactable = false;
        }

        // Check if the player has maxed out bullet range
        if (bulletRange == bulletRangeMax)
        {
            bulletRangeCostText.text = "MAX";
            upgradeBulletRange.interactable = false;
        }
        else
        {
            bulletRangeCostText.text = $"Upgrade cost: {Math.Round(bulletRangeCost)}";
        }

        // Check if the player can upgrade bullet range
        if (coins > bulletRangeCost)
        {
            upgradeBulletRange.interactable = true;
        }
        else
        {
            upgradeBulletRange.interactable = false;
        }

        // Check if the player has maxed out fire rate
        if (fireRate == fireRateMin)
        {
            fireRateCostText.text = "MAX";
            upgradeFireRate.interactable = false;
        }
        else
        {
            fireRateCostText.text = $"Upgrade cost: {Math.Round(fireRateCost, 3)}";
        }

        // Check if the player can upgrade fire rate
        if (coins > bulletRangeCost)
        {
            upgradeFireRate.interactable = true;
        }
        else
        {
            upgradeFireRate.interactable = false;
        }
    }

    /// <summary>
    /// Upgrades the bullet speed.
    /// </summary>
    public void UpgradeBulletSpeed()
    {
        // Play the sound
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        // Increase bullet speed
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

        // Disable the upgrade button if the player doesn't have enough coins or if the player has maxed out the possible upgrades
        if (coins < bulletSpeedCost || bulletSpeed == bulletSpeedMax)
        {
            upgradeBulletSpeed.interactable = false;
        }
    }

    /// <summary>
    /// Upgrades the bullet damage.
    /// </summary>
    public void UpgradeBulletDamage()
    {
        // Play the sound
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        // Increase bullet damage
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

        // Disable the upgrade button if the player doesn't have enough coins or if the player has maxed out the possible upgrades
        if (coins < bulletDamageCost || bulletDamage == bulletDamageMax)
        {
            upgradeBulletDamage.interactable = false;
        }
    }

    /// <summary>
    /// Upgrades the bullet range.
    /// </summary>
    public void UpgradeBulletRange()
    {
        // Play the sound
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        // Increase bullet range
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

        // Disable the upgrade button if the player doesn't have enough coins or if the player has maxed out the possible upgrades
        if (coins < bulletRangeCost || bulletRange == bulletRangeMax)
        {
            upgradeBulletRange.interactable = false;
        }
    }

    /// <summary>
    /// Upgrades the fire rate.
    /// </summary>
    public void UpgradeFireRate()
    {
        // Play the sound
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        // Decrease fire rate
        fireRate -= fireRateDecreaseAmount;

        if (fireRate <= fireRateMin)
        {
            fireRate = fireRateMin;
            fireRateCostText.text = "MAX";
            upgradeFireRate.interactable = false;

            PlayerPrefs.SetFloat("FireRate", fireRate);
            gunController.fireRate = fireRate;
            fireRateText.text = "Fire Rate: " + Math.Round(fireRate, 3);
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

        // Increase the cost
        fireRateCost *= upgradeCostMultiplier;
        fireRateCostText.text = "Upgrade cost: " + Math.Round(fireRateCost);
        PlayerPrefs.SetFloat("FireRateCost", fireRateCost);

        PlayerPrefs.SetFloat("FireRate", fireRate);
        gunController.fireRate = fireRate;
        fireRateText.text = "Fire Rate: " + Math.Round(fireRate, 3);

        // Disable the upgrade button if the player doesn't have enough coins or if the player has maxed out the possible upgrades
        if (coins < fireRateCost || fireRate == fireRateMin)
        {
            upgradeFireRate.interactable = false;
        }
    }
}
