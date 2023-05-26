using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the behavior of the player character in the game.
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// The forward speed of the player.
    /// </summary>
    public float forwardSpeed = 10f;

    /// <summary>
    /// The maximum speed of the player.
    /// </summary>
    public float maxSpeed = 20f;

    /// <summary>
    /// The distance between two lanes.
    /// </summary>
    public float laneDistance = 3f;

    /// <summary>
    /// Determines if the player is grounded.
    /// </summary>
    public bool isGrounded;

    /// <summary>
    /// The layer mask for the ground.
    /// </summary>
    public LayerMask groundLayer;

    /// <summary>
    /// The transform used for checking if the player is grounded.
    /// </summary>
    public Transform groundCheck;

    /// <summary>
    /// The animator component for controlling animations.
    /// </summary>
    public Animator animator;

    /// <summary>
    /// The gravity value affecting the player.
    /// </summary>
    public float gravity = -20f;

    /// <summary>
    /// The height of the player's jump.
    /// </summary>
    public float jumpHeight = 1f;

    /// <summary>
    /// The duration of the sliding animation.
    /// </summary>
    public float slideDuration = 1.5f;

    /// <summary>
    /// The speed increase per point scored.
    /// </summary>
    public float speedIncreasePerPoint = 0.1f;

    /// <summary>
    /// Reference to the *CharacterController* component.
    /// </summary>
    private CharacterController controller;

    /// <summary>
    /// The final value by which the player will have to move.
    /// </summary>
    private Vector3 move;

    /// <summary>
    /// Since the game plane is divided in three lines, at start, the player will be at the number one.
    /// </summary>
    private int desiredLane = 1;

    /// <summary>
    /// The vertical velocity.
    /// </summary>
    private Vector3 velocity;

    /// <summary>
    /// Boolean for checking if the player is sliding.
    /// </summary>
    private bool isSliding = false;

    private void Start() => controller = GetComponent<CharacterController>();

    /// <summary>
    /// Here the player it's constantly moved forward each frame.
    /// </summary>
    private void Update()
    {
        if (!GameManager.instance.gameStarted || GameManager.instance.gameOver)
        {
            animator.SetBool("isGameStarted", false);
            return;
        }

        animator.SetBool("isGameStarted", true);
        move.z = forwardSpeed;

        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 0.17f, groundLayer);

        animator.SetBool("isGrounded", isGrounded);
        if (isGrounded && velocity.y < 0)
            velocity.y = -1f;

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                Jump();

            if (Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) && !isSliding)
                StartCoroutine(Slide());
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
            if (Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) && !isSliding)
            {
                StartCoroutine(Slide());
                velocity.y = -10;
            }
        }
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
            targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * laneDistance;

        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = 30 * Time.deltaTime * diff.normalized;
            if (moveDir.sqrMagnitude < diff.magnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
        }

        controller.Move(move * Time.deltaTime);

        if (transform.position.y < -5)
        {
            Die();
        }
    }

    /// <summary>
    /// Makes the player character jump.
    /// </summary>
    private void Jump()
    {
        StopCoroutine(Slide());
        animator.SetBool("isSliding", false);
        animator.SetTrigger("jump");
        controller.center = Vector3.zero;
        controller.height = 2f;
        isSliding = false;

        velocity.y = Mathf.Sqrt(jumpHeight * 2 * -gravity);
    }

    /// <summary>
    /// Here it's checked when the player collides with obstacles or enemies.
    /// </summary>
    /// <param name="hit">The game object that has collided with this game object.</param>
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Obstacle"))
        {
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            animator.SetTrigger("die");
            Die();
        }
        if (hit.transform.CompareTag("Enemy"))
        {
            StartCoroutine(EnemyCollision(hit));
        }
    }

    /// <summary>
    /// Coroutine for the player's slide.
    /// </summary>
    /// <returns>Starts various count downs for the animations.</returns>
    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);
        yield return new WaitForSeconds(0.15f / Time.timeScale);

        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;
        yield return new WaitForSeconds((slideDuration - 0.25f) / Time.timeScale);

        animator.SetBool("isSliding", false);
        controller.center = Vector3.zero;
        controller.height = 2;
        isSliding = false;
    }

    /// <summary>
    /// Coroutine for when the player collides with enemies.
    /// </summary>
    /// <param name="hit"></param>
    /// <returns>Starts various count downs for the animations.</returns>
    private IEnumerator EnemyCollision(ControllerColliderHit hit)
    {
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        animator.SetTrigger("die");
        hit.gameObject.GetComponent<EnemyController>().Attack();

        yield return new WaitForSeconds(0.3f);

        Die();
    }

    /// <summary>
    /// Ends the game when the player dies.
    /// </summary>
    public void Die()
    {
        GameManager.instance.gameOver = true;
    }
}
