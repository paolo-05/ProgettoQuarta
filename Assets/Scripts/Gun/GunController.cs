// Include necessary Unity Engine libraries
using UnityEngine;

/// <summary>
/// Controls the behavior of a gun in the game.
/// </summary>
public class GunController : MonoBehaviour
{
    /// <summary>
    /// Determines if the gun is part of a UI element.
    /// </summary>
    public bool isUIElement = false;

    /// <summary>
    /// The time interval between two consecutive shots.
    /// </summary>
    public float fireRate = 0.1f;

    /// <summary>
    /// The damage inflicted by each bullet.
    /// </summary>
    public float bulletDamage = 30f;

    /// <summary>
    /// The maximum distance the bullet can travel.
    /// </summary>
    public float bulletRange = 20f;

    /// <summary>
    /// The speed at which the bullet travels.
    /// </summary>
    public float bulletSpeed = 1000f;

    /// <summary>
    /// The prefab for the bullet.
    /// </summary>
    public GameObject bulletPrefab;

    /// <summary>
    /// The spawn point for the bullet.
    /// </summary>
    public Transform bulletSpawnPoint;

    /// <summary>
    /// The effect played when the gun shoots.
    /// </summary>
    public GameObject shootingEffectPrefab;

    /// <summary>
    /// The trigger of the gun
    /// </summary>
    [SerializeField] GameObject trigger;

    private float nextFire = 0;

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        if (isUIElement) return;

        if (!GameManager.instance.gameStarted || GameManager.instance.gameOver) return;

        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    /// <summary>
    /// Shoots a bullet from the gun.
    /// </summary>
    public void Shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            trigger.transform.rotation = Quaternion.Euler(0f, 0f, 50f);
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);

            // Instantiate the shooting effect prefab
            GameObject shootingEffect = Instantiate(shootingEffectPrefab, bulletSpawnPoint.position, transform.rotation);
            Destroy(shootingEffect, 1f); // Destroy the shooting effect after 1 second

            if (isUIElement) { bullet.transform.localScale = new Vector3(150f, 150f, 150f); }

            BulletController bulletController = bullet.GetComponent<BulletController>();
            bulletController.range = bulletRange;
            bulletController.speed = bulletSpeed;
            bulletController.damage = bulletDamage;

            FindObjectOfType<AudioManager>().Play("Shoot");
        }

        Invoke(nameof(ResetTrigger), .3f);
    }

    /// <summary>
    /// Resets the trigger animation.
    /// </summary>
    private void ResetTrigger() => trigger.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
}
