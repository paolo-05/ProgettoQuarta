using UnityEngine;

/// <summary>
/// Follows the player's position with a camera.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    /// <summary>
    /// Reference to the player's transform.
    /// </summary>
    [SerializeField] Transform player;

    private Vector3 offset; // Vector representing the distance between the camera and the player

    /// <summary>
    /// Calculates the initial offset between the camera and the player.
    /// </summary>
    private void Start()
    {
        offset = transform.position - player.position;
    }

    /// <summary>
    /// Updates the camera's position based on the player's position.
    /// </summary>
    private void Update()
    {
        if (GameManager.instance.gameOver) { return; }

        if (!GameManager.instance.gameStarted)
        {
            // If the game hasn't started yet, move the camera to a fixed position
            transform.position = new Vector3(0, 5, 0);
        }

        Vector3 targetPos = player.position + offset;

        // Lock the camera's position on the x-axis
        targetPos.x = 0;

        // Update the position of the camera
        transform.position = targetPos;
    }
}
