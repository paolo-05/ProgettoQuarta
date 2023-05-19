using System.Collections;
using UnityEngine;
public class PlayerController : MonoBehaviour
{

    public float forwardSpeed = 10f;
    public float maxSpeed = 20f;
    public float laneDistance = 3f;//The distance between tow lanes
    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Animator animator;
    public float gravity = -20f;
    public float jumpHeight = 1f;
    public float slideDuration = 1.5f;
    public float speedIncreasePerPoint = 0.1f;

    private CharacterController controller;
    private Vector3 move;
    private int desiredLane = 1;//0:left, 1:middle, 2:right
    private Vector3 velocity;
    private bool isSliding = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
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
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
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

        //Gather the inputs on which lane we should be
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

        //Calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
            targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * laneDistance;

        //transform.position = targetPosition;
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Obstacle"))
        {
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            GameManager.instance.gameOver = true;
        }
        if (hit.transform.CompareTag("Enemy"))
        {
            StartCoroutine(EnemyCollision(hit));
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);
        yield return new WaitForSeconds(0.25f / Time.timeScale);

        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;
        yield return new WaitForSeconds((slideDuration - 0.25f) / Time.timeScale);

        animator.SetBool("isSliding", false);
        controller.center = Vector3.zero;
        controller.height = 2;
        isSliding = false;
    }

    private IEnumerator EnemyCollision(ControllerColliderHit hit)
    {
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        hit.gameObject.GetComponent<EnemyController>().Attack();
        
        yield return new WaitForSeconds(0.3f);

        GameManager.instance.gameOver = true;
        Destroy(gameObject);
    }

    public void Die()
    {
        GameManager.instance.gameOver = true;
    }
}
