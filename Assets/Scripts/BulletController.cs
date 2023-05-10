using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 20f;
    public float range = 20f;
    public float damage = 10f;

    private float distanceTravelled = 0f;
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Time.timeScale = 1.2f;
    }

    private void Update()
    {
        // Move the bullet forward
        Vector3 moveDirection = transform.TransformDirection(Vector3.left);
        controller.Move(moveDirection * speed * Time.deltaTime);

        // Check if the bullet has reached its maximum range
        distanceTravelled += speed * Time.deltaTime;
        if (distanceTravelled >= range)
        {
            Destroy(gameObject);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check if the bullet has hit an enemy
        EnemyController enemy = hit.collider.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
