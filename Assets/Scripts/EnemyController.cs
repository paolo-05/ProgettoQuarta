using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool isUIElement = false;

    public float health = 100f;
    public Animator animator;
    PlayerController playerController;
    GameManager gameManager;

    private void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("Take Damage");
        health -= damage;
        if (isUIElement) { return; }
        if (health <= 0f)
        {
            animator.SetTrigger("Die");
            Die();
        }
    }

    void Die()
    {
        gameManager.IncrementScore(10);
        Destroy(gameObject);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }

        if (hit.gameObject.GetComponent<CoinPickup>() != null)
        {
            Destroy(gameObject);
            return;
        }

        if (hit.gameObject.name == "Player")
        {
            animator.SetTrigger("Attack 01");
            playerController.Die();
           
        }
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
            animator.SetTrigger("Attack 01");
            playerController.Die();
        }
    }
}
