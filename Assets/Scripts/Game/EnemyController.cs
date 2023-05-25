using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the behavior of an enemy in the game.
/// </summary>
public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// Determines if the enemy is a UI element.
    /// </summary>
    public bool isUIElement = false;

    /// <summary>
    /// The max health of the enemy.
    /// </summary>
    public int maxHealth = 100;

    /// <summary>
    /// The current health of the enemy
    /// </summary>
    public int currentHealth;

    /// <summary>
    /// The animator component for controlling animations.
    /// </summary>
    public Animator animator;

    /// <summary>
    /// The reference to the health bar.
    /// </summary>
    public HealthBar healthBar;

    /// <summary>
    /// Reference to the health bar game object.
    /// </summary>
    public GameObject healtBarGameObject;

    /// <summary>
    /// In this method, called only once the enemy is istantiated, the health is set.
    /// </summary>
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        if ( isUIElement ) healtBarGameObject.SetActive(false);
    }

    /// <summary>
    /// Called when the enemy takes damage.
    /// </summary>
    /// <param name="damage">The amount of damage to inflict on the enemy.</param>
    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Take Damage");
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (isUIElement)
        {
            return;
        }

        FindObjectOfType<AudioManager>().Play("EnemyDamage");

        if (currentHealth <= 0f)
        {
            StartCoroutine(Die());
        }
    }

    /// <summary>
    /// Coroutine that handles the death of the enemy.
    /// </summary>
    private IEnumerator Die()
    {
        animator.SetTrigger("Die");

        BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            Destroy(boxCollider);
        }

        GameManager.instance.IncrementScore(10);

        yield return new WaitForSeconds(1.2f);

        Destroy(gameObject);
    }

    /// <summary>
    /// Called when the enemy's collider collides with another collider.
    /// </summary>
    /// <param name="collision">The collision information.</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            return;
        }

        if (collision.transform.CompareTag("Coin"))
        {
            Destroy(gameObject);
            return;
        }
    }

    /// <summary>
    /// Triggers the enemy's attack animation.
    /// </summary>
    public void Attack()
    {
        animator.SetTrigger("Attack 01");
    }
}
