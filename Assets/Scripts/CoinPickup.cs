using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int coinValue = 1;
    public float rotationSpeed = 200f;

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
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