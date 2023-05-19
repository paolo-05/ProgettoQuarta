using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerController playerController;    // Reference to the PlayerController script
    public bool isCrouchObstacle = false; // Flag to indicate if this is a crouch obstacle or not

    void Start()
    {
        // Find the PlayerController script in the scene
        playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    // Called when the controller hits a collider while performing a Move operation
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check if the collided object is the player
        if (hit.gameObject.name == "Player")
        {
            // Call the Die() method on the PlayerController script
            playerController.Die();
        }
    }

    // Called when this collider/rigidbody has begun touching another rigidbody/collider
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is the player
        if (collision.gameObject.name == "Player")
        {
            // Call the Die() method on the PlayerController script
            playerController.Die();
        }
    }
}
