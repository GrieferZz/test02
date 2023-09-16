using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 15f;
    public float dashRange = 2f;
    public float dashCooldown = 1f;
    public float dashDuration = 0.5f;

    public float acceleration = 5f; // Adjust this for smooth acceleration/deceleration

    private Vector3 moveDirection;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float currentSpeed;
    private Vector3 currentVelocity;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && dashTimer <= 0f)
        {
            StartDash();
        }

        if (isDashing)
        {
            // Dash logic is handled by applying a force in StartDash()
        }
        else
        {
            Move();
        }

        dashTimer -= Time.deltaTime;
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Calculate the target velocity based on moveDirection and moveSpeed
        Vector3 targetVelocity = moveDirection * moveSpeed;

        // Interpolate the current velocity towards the target velocity for smooth acceleration
        currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, Time.deltaTime * acceleration);

        // Apply the current velocity to the Rigidbody
        rb.velocity = new Vector3(currentVelocity.x, rb.velocity.y, currentVelocity.z);
    }

    private void StartDash()
    {
        isDashing = true;
        dashTimer = dashCooldown;

        // Calculate the dash direction and distance
        Vector3 dashDirection = moveDirection;
        float dashDistance = dashRange;

        // Apply a force to the Rigidbody for dashing
        rb.AddForce(dashDirection * dashSpeed, ForceMode.VelocityChange);

        // Stop the dash after a duration
        Invoke("StopDash", dashDuration);
    }

    private void StopDash()
    {
        isDashing = false;
        rb.velocity = Vector3.zero;
    }
}