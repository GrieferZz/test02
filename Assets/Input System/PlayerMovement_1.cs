using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private void Update() 
     {
        
        Animate();
        
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
    }

    private void Animate()
    {
        anim.SetFloat("MovementX", MoveInput0.x);
        anim.SetFloat("MovementZ", MoveInput0.y);
    }

}
