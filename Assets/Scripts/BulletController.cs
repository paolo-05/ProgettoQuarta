// This script controls the behavior of a bullet in the game
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 20f; // The speed at which the bullet travels
    public float range = 20f; // The maximum distance the bullet can travel
    public float damage = 10f; // The amount of damage the bullet deals

    private float distanceTravelled = 0f; // The distance the bullet has travelled so far
    private CharacterController controller; // Reference to the CharacterController component attached to the bullet

    private void Start()
    {
        controller = GetComponent<CharacterController>(); // Get the CharacterController component from the bullet object
        // Time.timeScale = 1.2f; // Increase the game speed slightly
    }

    private void Update()
    {
        // Move the bullet forward
        Vector3 moveDirection = transform.TransformDirection(Vector3.left); // Get the forward direction of the bullet
        controller.Move(speed * Time.deltaTime * moveDirection); // Move the bullet in the forward direction

        // Check if the bullet has reached its maximum range
        distanceTravelled += speed * Time.deltaTime; // Add the distance travelled in this frame to the total distance travelled
        if (distanceTravelled >= range) // If the total distance travelled is greater than or equal to the maximum range
        {
            Destroy(gameObject); // Destroy the bullet object
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check the collision with an obstacle
        if (hit.transform.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
        
        // Check if the bullet has hit an enemy
        EnemyController enemy = hit.collider.GetComponent<EnemyController>(); // Get the EnemyController component from the collider of the object the bullet hit
        if (enemy != null) // If the object has an EnemyController component
        {
            enemy.TakeDamage(damage); // Call the TakeDamage() function of the EnemyController to deal damage to the enemy
            Destroy(gameObject); // Destroy the bullet object
        }
    }

}
