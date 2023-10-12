using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_3 : MonoBehaviour
{
    // 移动速度
    public float moveSpeed = 5f;

    // 斜坡上的额外力度
    public float slopeForce = 5f;

    // 地面检测距离
    public float groundRayDistance = 0.2f;

    // 是否在地面上
    private bool isGrounded;

    // Rigidbody组件
    private Rigidbody rb;

    // 输入系统
    private PlayerInput playerInput;

    void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();
        playerInput.GamePlay.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        playerInput.GamePlay.Move.canceled += ctx => Move(Vector2.zero);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundRayDistance);
    }

    void Move(Vector2 moveDirection)
    {
        Vector3 move = new Vector3(moveDirection.x, 0f, moveDirection.y).normalized;

        Vector3 slopeNormal = GetSlopeNormal();

        if (isGrounded && move != Vector3.zero)
        {
            rb.AddForce(Vector3.down * slopeForce * Vector3.Dot(Vector3.up, slopeNormal));
        }

        MoveCharacter(move);
    }

    void MoveCharacter(Vector3 move)
    {
      
        Vector3 movement = move * moveSpeed * Time.fixedDeltaTime;

        Vector3 newVelocity = rb.velocity + movement;
        newVelocity = Vector3.ClampMagnitude(newVelocity, moveSpeed);

        rb.velocity = new Vector3(newVelocity.x, rb.velocity.y, newVelocity.z);
    }

    Vector3 GetSlopeNormal()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundRayDistance + 0.1f))
        {
            return hit.normal;
        }
        return Vector3.up;
    }
}