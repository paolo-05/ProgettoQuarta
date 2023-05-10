using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public bool alive = true;
    public float speed = 10f;
    public float speedIncreasePerPoint = 0.1f;

    [SerializeField] float jumpHeight = 5f;
    [SerializeField] float crouchHeight = .5f;
    [SerializeField] Rigidbody rb;

    private bool isGrounded = false;
    private bool isCrouching = false;

    private void FixedUpdate()
    {
        if (!alive) return;
        if (!GameManager.instance.gameStarted) return;
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMove);
    }

    void Update()
    {
        if (!GameManager.instance.gameStarted) return;
        if (!alive) return;
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && transform.position.x > -3f)
        {
            transform.position = new Vector3(transform.position.x - 3f, transform.position.y, transform.position.z);
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && transform.position.x < 3f)
        {
            transform.position = new Vector3(transform.position.x + 3f, transform.position.y, transform.position.z);
        }

        // Check if the player is grounded
        if (Physics.Raycast(transform.position, Vector3.down, 2f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        // Jump
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isGrounded && !isCrouching)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            isGrounded = false;
        }

        // Crouch
        if (Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
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

    public void Die()
    {
        alive = false;
        Debug.Log(GameManager.instance.thisGameCoins);
        Invoke(nameof(Restart), 2);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}