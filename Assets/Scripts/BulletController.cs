using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public float range = 20f;
    public float damage = 10f;

    private float distanceTravelled = 0f;

    void Update()
    {
        // Move the bullet forward
        transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);

        // Check if the bullet has reached its maximum range
        distanceTravelled += speed * Time.deltaTime;
        if (distanceTravelled >= range)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the bullet has hit an enemy
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
