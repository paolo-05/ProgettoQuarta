// Import necessary packages
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Public variables
    public bool isUIElement = false;
    public float health = 100f;
    public Animator animator;

    // Private variables
    private PlayerController playerController;
    private GameManager gameManager;

    // Start is called before the first frame update
    private void Start()
    {
        // Find and store references to the PlayerController and GameManager
        playerController = GameObject.FindObjectOfType<PlayerController>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Function to be called when the enemy takes damage
    public void TakeDamage(float damage)
    {
        // Trigger the "Take Damage" animation
        animator.SetTrigger("Take Damage");

        // Subtract the damage from the enemy's health
        health -= damage;

        // Check if this enemy is a UI element and return if it is
        if (isUIElement) { return; }

        // Check if the enemy's health is zero or less, and if it is, trigger the "Die" animation and call the Die() function
        if (health <= 0f)
        {
            animator.SetTrigger("Die");
            Die();
        }
    }

    // Function to be called when the enemy dies
    void Die()
    {
        // Call the IncrementScore function of the GameManager to increment the score by 10
        gameManager.IncrementScore(10);

        // Destroy this enemy game object
        Destroy(gameObject);
    }

    // Function called when the enemy's collider hits something
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check if the collider is an obstacle, and if it is, destroy this enemy game object
        if (hit.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }

        // Check if the collider is a coin, and if it is, destroy this enemy game object
        if (hit.gameObject.GetComponent<CoinPickup>() != null)
        {
            Destroy(gameObject);
            return;
        }

        // Check if the collider is the player, and if it is, trigger the "Attack 01" animation and call the Die() function of the player
        if (hit.gameObject.name == "Player")
        {
            animator.SetTrigger("Attack 01");
            playerController.Die();
        }
    }

    // Function called when the enemy's collider collides with something
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collider is an obstacle, and if it is, destroy this enemy game object
        if (collision.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }

        // Check if the collider is a coin, and if it is, destroy this enemy game object
        if (collision.gameObject.GetComponent<CoinPickup>() != null)
        {
            Destroy(gameObject);
            return;
        }

        // Check if the collider is the player, and if it is, trigger the "Attack 01" animation and call the Die() function of the player
        if (collision.gameObject.name == "Player")
        {
            animator.SetTrigger("Attack 01");
            playerController.Die();
        }
    }

}
