using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using JetBrains.Annotations;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement_1 : MonoBehaviour
{

    
    
    private CharacterController cr;
    private Animator anim;

    public float MoveSpeed;
    public PlayerInput inputControl;
    public Vector3  MoveInput;
    public Vector2  MoveInput0;
    private int combo = 0;
    private bool one = true;

    //冲刺
    public float sprintSpeed = 10.0f; // 冲刺速度
    public float sprintDuration = 1.0f; // 冲刺持续时间
    private float sprintTime = 0.0f;
    private Vector3 moveDirection = Vector3.zero;
    private  Vector3 SprintDistance = Vector3.zero;
    
    public float sprintCooldown = 2.0f; // 冷却时间
    private bool canSprint=true;
    private void Awake()
    {
        cr = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        inputControl=new PlayerInput();
    }
    private void OnEnable() 
    {
        inputControl.Enable();
    }
    private void OnDisable() 
    {
        inputControl.Disable();
    }
    void Start() 
    {
        MoveSpeed=Player.Instance.Speed;
        
    }
    private void Update() 
     {
        if( MoveInput0.magnitude!=0f)
        {
            Player.Instance.NowState=Player.PlayerState.Move;
            Debug.Log( MoveInput0.magnitude);
        }
        else  if(MoveInput0.magnitude==0f)
        {
            Player.Instance.NowState=Player.PlayerState.Idle;
        }
        Animate();
         Sprint();
     }
    private void FixedUpdate() 
    {
        Move();
       
    }

    private void Move()
    {
        MoveInput0=inputControl.GamePlay.Move.ReadValue<Vector2>();
        float horizontal1 = inputControl.GamePlay.Move.ReadValue<Vector2>().x;
        float vertical1 = inputControl.GamePlay.Move.ReadValue<Vector2>().y;

        if (Mathf.Approximately(horizontal1, 0) && Mathf.Approximately(vertical1, 0))
        {
            
            anim.SetBool("IsMoving", false); // No input, set IsMoving to false
            return;
        }
        
       
        cr.Move(new Vector3(MoveInput0.x*MoveSpeed*Time.deltaTime,-9.81f*Time.deltaTime,MoveInput0.y*MoveSpeed*Time.deltaTime));
        anim.SetBool("IsMoving", true); // There is input, set IsMoving to true
        moveDirection = new Vector3(horizontal1, 0f, vertical1).normalized;
        
    }

    private void Animate()
    {
        anim.SetFloat("MovementX", MoveInput0.x);
        anim.SetFloat("MovementZ", MoveInput0.y);
    }
    private void Sprint()
    {
        if(Player.Instance.NowState==Player.PlayerState.Move||Player.Instance.NowState==Player.PlayerState.Idle)
        {
         if(inputControl.GamePlay.Skill.IsPressed()&& sprintTime <= 0&&canSprint)
         {
            StartCoroutine(SprintCd());
            SprintDistance = moveDirection*sprintSpeed;
            sprintTime = sprintDuration;
         }
         if (sprintTime > 0)
        {
            Player.Instance.NowState=Player.PlayerState.Sprint;
            cr.Move(SprintDistance * Time.deltaTime);
            sprintTime -= Time.deltaTime;
        }
        else if(sprintTime==0&&Player.Instance.NowState==Player.PlayerState.Sprint)
        {
            Player.Instance.NowState=Player.PlayerState.Move;
        }
        }
    }
    IEnumerator SprintCd()
    {
       canSprint=false;

       yield return new WaitForSeconds(sprintCooldown);// 等待冷却时间
       canSprint=true;

    }
    


}
