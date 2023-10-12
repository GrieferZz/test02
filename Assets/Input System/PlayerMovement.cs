using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
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
        playerInput.GamePlay.Enable();
        playerInput.GamePlay.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        playerInput.GamePlay.Move.canceled += ctx => Move(Vector2.zero);
    }

    void Start()
    {
        // 获取Rigidbody组件
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 检测是否在地面上
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundRayDistance);
    }

    void Move(Vector2 moveDirection)
    {
        // 计算移动向量
        Vector3 move = new Vector3(moveDirection.x, 0f, moveDirection.y).normalized;

        // 斜坡上的平稳移动
        if (isGrounded && move != Vector3.zero)
        {
            rb.AddForce(Vector3.down * slopeForce);
        }

        // 移动
        MoveCharacter(move);
    }

    void MoveCharacter(Vector3 move)
    {
        // 计算移动距离
        Vector3 movement = move * moveSpeed * Time.fixedDeltaTime;

        // 移动角色
        transform.Translate(movement);
    }
}

//public class PlayerMove : MonoBehaviour
//{
//    public PlayerInput playerInput;
//    public Vector2 inputDirection;

//    public Rigidbody rb;

//    public float speed;


//    private void Awake()
//    {
//       rb = GetComponent<Rigidbody>();
//       playerInput = new PlayerInput();
//    }

//    private void OnEnable()
//    {
//        playerInput.Enable();
//    }

//    private void OnDisable()
//    {
//        playerInput.Disable(); 
//    }

//    private void Update()
//    {
//      //                 InputSystem entity.Action map.Action.attribute
//        inputDirection = playerInput.GamePlay.Move.ReadValue<Vector2>();
//    }

//    private void FixedUpdate()
//    {
//        Move();
//    }

//    public void Move()
//    {
//        rb.velocity = new Vector3(inputDirection.x * speed * Time.deltaTime, rb.velocity.y, inputDirection.y * speed * Time.deltaTime);
//    }
//}



//old input system：
//public class PlayerMove : MonoBehaviour
//{
//    private Rigidbody rb;
//    private Animator anim;

//    public float MoveSpeed;
//    private Vector3 MoveInput;
//    private int combo=0;
//    private bool one=true;

//    private void Awake()
//    {
//        rb = GetComponent<Rigidbody>();
//        anim = GetComponent<Animator>();
//    }

//    private void Update()
//    {
//        Move();
//        Animate();
//        if(Input.GetMouseButtonDown(0))
//        {
//            anim.SetBool("isAttack",true);
//            if(combo>=1)
//            anim.SetTrigger("attack1");
//            combo++;
//            Debug.Log("1");

//        }
//    }


//    private void Move()
//    {
//        float horizontal1 = Input.GetAxisRaw("Horizontal");
//        float vertical1 = Input.GetAxisRaw("Vertical");

//        if (Mathf.Approximately(horizontal1, 0) && Mathf.Approximately(vertical1, 0))
//        {
//            rb.velocity = Vector3.zero;
//            anim.SetBool("IsMoving", false); // No input, set IsMoving to false
//            return;
//        }

//        MoveInput = new Vector3(horizontal1, 0, vertical1).normalized;
//        rb.velocity = MoveInput * MoveSpeed * Time.fixedDeltaTime ;
//        anim.SetBool("IsMoving", true); // There is input, set IsMoving to true
//    }

//    private void Animate()
//    {
//        anim.SetFloat("MovementX", MoveInput.x);
//        anim.SetFloat("MovementZ", MoveInput.z);
//    }

//}