using UnityEngine;

/// <summary>
/// Represents an obstacle in the game.
/// </summary>
public class Obstacle : MonoBehaviour
{
    /// <summary>
    /// Reference to the PlayerController script
    /// </summary>
    private PlayerController playerController;

    /// <summary>
    /// Flag to indicate if this is a crouch obstacle or not
    /// </summary>
    public bool isCrouchObstacle = false;

    private void Start() => playerController = FindObjectOfType<PlayerController>();

    /// <summary>
    /// Called when the controller hits a collider while performing a Move operation.
    /// </summary>
    /// <param name="hit">The controller collider hit information.</param>
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            playerController.Die();
        }
    }

    /// <summary>
    /// Called when this collider/rigidbody has begun touching another rigidbody/collider.
    /// </summary>
    /// <param name="collision">The collision information.</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController.Die();
        }
    }
}
