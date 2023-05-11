// Include necessary Unity Engine libraries
using UnityEngine;

// GunController class
public class GunController : MonoBehaviour
{
    // Public variables
    public bool isUIElement = false; // Determines if the gun is part of a UI element
    public float fireRate = 0.1f; // The time interval between two consecutive shots
    public float bulletDamage = 10f; // The damage inflicted by each bullet
    public float bulletRange = 20f; // The maximum distance the bullet can travel
    public float bulletSpeed = 20f; // The speed at which the bullet travels
    public GameObject bulletPrefab; // The prefab for the bullet
    public Transform bulletSpawnPoint; // The spawn point for the bullet
    [SerializeField] GameObject trigger; // The trigger GameObject used for animation

    // Private variables
    private float nextFire = 0; // The time at which the next shot can be fired

    // Update is called once per frame
    void Update()
    {
        // Return if the gun is part of a UI element
        if (isUIElement) return;
        // Return if the game is not started yet or the the game is over
        if (!GameManager.instance.gameStarted || GameManager.instance.gameOver) return;
        // If the player pressed the Fire1 button (usually left mouse button), call the Shoot() function
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    // Shoot function
    public void Shoot()
    {
        // If enough time has passed since the last shot, fire a new bullet
        if (Time.time > nextFire)
        {
            // Set the time for the next shot
            nextFire = Time.time + fireRate;
            // Set the rotation of the trigger GameObject for animation
            trigger.transform.rotation = Quaternion.Euler(0f, 0f, 50f);
            // Spawn a new bullet prefab at the bullet spawn point
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
            // If the gun is part of a UI element, set the scale of the bullet to be larger
            if (isUIElement) { bullet.transform.localScale = new Vector3(150f, 150f, 150f); }
            // Get the BulletController component from the spawned bullet and set its variables
            BulletController bulletController = bullet.GetComponent<BulletController>();
            bulletController.range = bulletRange;
            bulletController.speed = bulletSpeed;
            bulletController.damage = bulletDamage;
            // Play the shooting sound effect
            FindObjectOfType<AudioManager>().Play("Shoot");
        }
        // Call the ResetTrigger function after 0.3 seconds for animation
        Invoke(nameof(ResetTrigger), .3f);
    }

    // ResetTrigger function for resetting trigger animation
    private void ResetTrigger() => trigger.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
}
