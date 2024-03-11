using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using Cinemachine;
using JetBrains.Annotations;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement_1 : MonoBehaviour
{

    
    private CharacterStates characterStates;
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
        characterStates=GetComponent<CharacterStates>();
        
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
       

    }
    private void Update() 
     {

       
        Animate();
        Sprint();
 

       
    }
    private void FixedUpdate() 
    {
        Move();
       
    }

    private void Move()
    {
        MoveSpeed=characterStates.currentSpeed;
        if(Player.Instance.canMove)
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
        if( MoveInput0.magnitude!=0f)
        {
            Player.Instance.NowState=Player.PlayerState.Move;
            UnityEngine.Debug.Log( MoveInput0.magnitude);
        }
         else  if(MoveInput0.magnitude==0f)
        {
            Player.Instance.NowState=Player.PlayerState.Idle;
        }

        }
        else if(!Player.Instance.canMove)
        {
            MoveInput0=new Vector2(0,0);
             anim.SetBool("IsMoving", false); // No input, set IsMoving to false
        }
        
        
    }

    private void Animate()
    {
        //if(Mathf.Abs(MoveInput0.x)>0.0001f)
        anim.SetFloat("MovementX", MoveInput0.x);
         //if( Mathf.Abs(MoveInput0.y)>0.0001f)
        anim.SetFloat("MovementZ", MoveInput0.y);
        if(Mathf.Abs(MoveInput0.x)>0.0001f)
         anim.SetFloat("IdleX", MoveInput0.x);
         if( Mathf.Abs(MoveInput0.y)>0.0001f)
         anim.SetFloat("IdleZ", MoveInput0.y);
         if(MoveInput0.y!=1&&MoveInput0.x!=0)
         {
             anim.SetFloat("IdleZ", -1f);
         }
    }
    private void Sprint()
    {
        if(Player.Instance.NowState==Player.PlayerState.Move||Player.Instance.NowState==Player.PlayerState.Idle)
        {
         if(inputControl.GamePlay.Skill.IsPressed()&& sprintTime <= 0&&canSprint)
         {
            StartCoroutine(SprintCd());
            anim.SetTrigger("Roll");
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
