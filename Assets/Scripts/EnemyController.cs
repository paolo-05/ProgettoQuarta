// Import necessary packages
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Public variables
    public bool isUIElement = false;
    public float health = 100f;
    public Animator animator;

    // Function to be called when the enemy takes damage
    public void TakeDamage(float damage)
    {
        // Trigger the "Take Damage" animation
        animator.SetTrigger("Take Damage");

        // Subtract the damage from the enemy's health
        health -= damage;

        // Check if this enemy is a UI element and return if it is
        if (isUIElement) { return; }

        // Play the sound
        FindObjectOfType<AudioManager>().Play("EnemyDamage");

        // Check if the enemy's health is zero or less, and if it is, trigger the "Die" animation and call the Die() function
        if (health <= 0f)
        {
            StartCoroutine(Die());
        }
    }

    // Function to be called when the enemy dies
    private IEnumerator Die()
    {
        // play the die animation
        animator.SetTrigger("Die");

        // remove the collider
        BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            Destroy(boxCollider);
        }

        // Call the IncrementScore function of the GameManager to increment the score by 10
        GameManager.instance.IncrementScore(10);

        yield return new WaitForSeconds(1.2f);

        // Destroy this enemy game object
        Destroy(gameObject);
    }

    // Function called when the enemy's collider collides with something
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collider is an obstacle, and if it is, destroy this enemy game object
        if (collision.transform.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            return;
        }

        // Check if the collider is a coin, and if it is, destroy this enemy game object
        if (collision.transform.CompareTag("Coin"))
        {
            Destroy(gameObject);
            return;
        }
    }
    public void Attack()
    {
        animator.SetTrigger("Attack 01");
    }
}
