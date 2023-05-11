using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int coinValue = 1;
    public float rotationSpeed = 800f;

    // Rotate the coin
    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.up, Space.World);
    }

    // Check if the coin has collided with something
    void OnTriggerEnter(Collider collision)
    {
        // Check if the coin collided with an obstacle, destroy the coin if it has
        if (collision.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }

        // Check if the coin collided with the player, increment the player's coins and destroy the coin
        if (collision.gameObject.name == "Player")
        {

            // Play the sound
            FindObjectOfType<AudioManager>().Play("CoinPickup");

            GameManager.instance.IncrementCoins(coinValue);
            Destroy(gameObject);
        }
    }
}