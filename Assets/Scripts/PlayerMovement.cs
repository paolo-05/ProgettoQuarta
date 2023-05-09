using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public bool alive = true;
    public float speed = 10f;
    public float speedIncreasePerPoint = 0.1f;

    [SerializeField] float jumpHeight = 7.5f;
    [SerializeField] float crouchHeight = .5f;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator animator;

    private bool isGrounded = false;
    private bool isCrouching = false;

    private void FixedUpdate()
    {
        if (!alive) return;
        if (!GameManager.instance.gameStarted) return;
        animator.SetTrigger("Running");
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
        if (Physics.Raycast(transform.position, Vector3.down, 1.5f))
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
            animator.SetTrigger("Jump");
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            isGrounded = false;
        }

        // Crouch
        if (Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            animator.SetTrigger("Roll");
            transform.localScale = new Vector3(1.5f, crouchHeight, 1.15f);
            isCrouching = true;
        }
        else
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
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
