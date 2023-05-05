using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpHeight = 5f;
    public float crouchHeight = 1f;

    private bool isGrounded = true;
    private bool isCrouching = false;

    private void Update()
    {
        // Move the player horizontally within the three lanes
        if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > -2f)
        {
            transform.position = new Vector3(transform.position.x - 2f, transform.position.y, transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < 2f)
        {
            transform.position = new Vector3(transform.position.x +2f, transform.position.y, transform.position.z);
        }
        
        // Check if the player is grounded
        if (Physics.Raycast(transform.position, Vector3.down, 1f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isCrouching)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            isGrounded = false;
        }

        // Crouch
        if (Input.GetKey(KeyCode.C))
        {
            transform.localScale = new Vector3(1, crouchHeight, 1);
            isCrouching = true;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            isCrouching = false;
        }

        // Freeze player rotation to prevent flipping
        GetComponent<Rigidbody>().freezeRotation = true;
    }
}
