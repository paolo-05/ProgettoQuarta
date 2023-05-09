using UnityEngine;

public class GunController : MonoBehaviour
{
    public float fireRate = 0.1f;

    public float bulletDamage = 10f;
    public float bulletRange = 20f;
    public float bulletSpeed = 20f;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    //public AudioClip shootSound;

    private AudioSource audioSource;
    private float nextFire = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!GameManager.instance.gameStarted) return;
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
            BulletController bulletController = bullet.GetComponent<BulletController>();
            bulletController.range = bulletRange;
            bulletController.speed = bulletSpeed;
            bulletController.damage = bulletDamage;

            // AudioSource.PlayClipAtPoint(shootSound, transform.position);
        }
    }
}
