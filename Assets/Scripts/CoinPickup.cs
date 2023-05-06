using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinValue = 1;
    void update()
    {
        transform.Rotate(new Vector3(0f, 1f, 0f) * Time.deltaTime * 100f, Space.World);
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameManager.instance.IncrementCoins(coinValue);
            Destroy(gameObject);
        }
    }
}