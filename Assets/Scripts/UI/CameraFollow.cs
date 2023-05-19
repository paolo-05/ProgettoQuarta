using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Reference to the player's transform
    [SerializeField] Transform player;

    // Vector representing the distance between the camera and the player
    private Vector3 offset;

    void Start()
    {
        // Calculate the initial offset between the camera and the player
        offset = transform.position - player.position;
    }

    void Update()
    {
        if (GameManager.instance.gameOver) { return; }
        // If the game hasn't started yet, move the camera to a fixed position
        if (!GameManager.instance.gameStarted)
        {
            transform.position = new Vector3(0, 5, 0);
        }

        // Calculate the new position of the camera based on the player's position and the offset
        Vector3 targetPos = player.position + offset;

        // Lock the camera's position on the x-axis
        targetPos.x = 0;

        // Update the position of the camera
        transform.position = targetPos;
    }
}
