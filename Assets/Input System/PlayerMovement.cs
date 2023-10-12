using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // �ƶ��ٶ�
    public float moveSpeed = 5f;

    // б���ϵĶ�������
    public float slopeForce = 5f;

    // ���������
    public float groundRayDistance = 0.2f;

    // �Ƿ��ڵ�����
    private bool isGrounded;

    // Rigidbody���
    private Rigidbody rb;

    // ����ϵͳ
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
        // ��ȡRigidbody���
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // ����Ƿ��ڵ�����
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundRayDistance);
    }

    void Move(Vector2 moveDirection)
    {
        // �����ƶ�����
        Vector3 move = new Vector3(moveDirection.x, 0f, moveDirection.y).normalized;

        // б���ϵ�ƽ���ƶ�
        if (isGrounded && move != Vector3.zero)
        {
            rb.AddForce(Vector3.down * slopeForce);
        }

        // �ƶ�
        MoveCharacter(move);
    }

    void MoveCharacter(Vector3 move)
    {
        // �����ƶ�����
        Vector3 movement = move * moveSpeed * Time.fixedDeltaTime;

        // �ƶ���ɫ
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



//old input system��
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