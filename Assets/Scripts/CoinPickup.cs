using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int coinValue = 1;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.name == "Player")
        {
            GameManager.instance.IncrementCoins(coinValue);
            Destroy(gameObject);
        }
    }
}