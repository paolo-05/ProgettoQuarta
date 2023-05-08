using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 100f;
    PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }
    private void Update()
    {
        // Get a reference to the material
        Material material = gameObject.GetComponent<Renderer>().material;
        switch (health)
        {
            case 100f:
                // Change the albedo color
                material.color = new Color32(255, 0, 0, 255);
                break;
            case 80f:
                // Change the albedo color
                material.color = new Color32(255, 60, 60, 255);
                break;
            case 60f:
                // Change the albedo color
                material.color = new Color32(255, 120, 120, 255);
                break;
            case 40f:
                // Change the albedo color
                material.color = new Color32(255, 180, 180, 255);
                break;
            case 20f:
                // Change the albedo color
                material.color = new Color32(255, 255, 255, 255);
                break;
            default:
                break;
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Hit");
        health -= damage;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.GetComponent<CoinPickup>() != null)
        {
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.name == "Player")
        {
            playerMovement.Die();
        }
    }
}
