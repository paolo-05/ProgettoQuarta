using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    bool alive = true;

    public float speed = 5f;
    public float jumpHeight = 5f;
    public float crouchHeight = .5f;
    public Rigidbody rb;

    bool isGrounded = false;
    bool isCrouching = false;
    
    float horizontalInput;

    public float horizontalMultiplier = 2;

    private void FixedUpdate ()
    {
        if (!alive) return;

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if(!alive) return;
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
        if (transform.position.y < -5)
        {
            Die();
        }
    }

    public void Die(){
        alive = false;
        Invoke("Restart", 2);
    }
    
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
