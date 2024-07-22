using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float jumpForce = 5f;
    public LayerMask groundLayers;
    private Vector3 customGravity;
    private Vector3 customUp;

    private Rigidbody rb;
    private CapsuleCollider col;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        customGravity = Vector3.down;
        customUp = Vector3.up;
        Physics.gravity = customGravity;
    }

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        CheckFalling();

        // Game over condition for free fall
        if (rb.velocity.magnitude > 50f && !IsGrounded()) // Adjust the velocity threshold as needed
        {
            GameOver();
        }
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Vector3 transformedDirection = transform.TransformDirection(moveDirection);

        rb.velocity = transformedDirection * speed + customGravity * Time.deltaTime;
    }

    public void SetGravity(Vector3 newGravityDirection)
    {
        customGravity = newGravityDirection;
        customUp = -newGravityDirection;
        Physics.gravity = customGravity;
        transform.rotation = Quaternion.LookRotation(transform.forward, customUp);
    }

    void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(customUp * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center,
                                    new Vector3(col.bounds.center.x, col.bounds.center.y - col.height / 2 + col.radius, col.bounds.center.z),
                                    col.radius * 0.9f, groundLayers);
    }

    private void CheckFalling()
    {
        if (!IsGrounded() && rb.velocity.y < 0)
        {
            animator.SetBool("IsFalling", true);
        }
        else
        {
            animator.SetBool("IsFalling", false);
        }
    }

    void GameOver()
    {
        // Implement your game over logic here
        Debug.Log("Game Over! Player fell freely!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart the scene for now
    }
}
