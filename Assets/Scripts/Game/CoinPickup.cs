using UnityEngine;

/// <summary>
/// Represents a coin that can be picked up by the player.
/// </summary>
public class CoinPickup : MonoBehaviour
{
    /// <summary>
    /// The value of the coin.
    /// </summary>
    public int coinValue = 1;

    /// <summary>
    /// The coin's rotation speed.
    /// </summary>
    public float rotationSpeed = 800f;

    /// <summary>
    /// Here we rotate the coin.
    /// </summary>
    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.up, Space.World);
    }

    /// <summary>
    /// Handles the collision event when the coin collides with another collider.
    /// </summary>
    /// <param name="collision">The collider the coin has collided with.</param>
    void OnTriggerEnter(Collider collision)
    {
        // Check if the coin collided with an obstacle, destroy the coin if it has
        if (collision.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }

        // Check if the coin collided with the player, increment the player's coins and destroy the coin
        if (collision.CompareTag("Player"))
        {
            // Play the sound
            FindObjectOfType<AudioManager>().Play("CoinPickup");

            // increment the score
            GameManager.instance.IncrementCoins(coinValue);

            // destroy the coin
            Destroy(gameObject);
        }
    }
}