using UnityEngine;

public class GunController : MonoBehaviour
{
    public bool isUIElement = false;

    public float fireRate = 0.1f;

    public float bulletDamage = 10f;
    public float bulletRange = 20f;
    public float bulletSpeed = 20f;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject trigger;

    //public AudioClip shootSound;

    private AudioSource audioSource;
    private float nextFire = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerTransform == null)
        {
            return;
        }
        gameObject.transform.position = playerTransform.position + new Vector3(0, 0, .8f);
        if (!GameManager.instance.gameStarted) return;
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            trigger.transform.rotation = Quaternion.Euler(0f, 0f, 50f);
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
            if (isUIElement) { bullet.transform.localScale = new Vector3(150f, 150f, 150f); }
            BulletController bulletController = bullet.GetComponent<BulletController>();
            bulletController.range = bulletRange;
            bulletController.speed = bulletSpeed;
            bulletController.damage = bulletDamage;

            // AudioSource.PlayClipAtPoint(shootSound, transform.position);
        }
        Invoke(nameof(ResetTrigger), .3f);
    }
    private void ResetTrigger() => trigger.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

}
