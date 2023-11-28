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
public class CharacaterMove : MonoBehaviour
{
     private CharacterController cr;
      public float MoveSpeed;
    public PlayerInput inputControl;
    public Vector3  MoveInput;
    public Vector2  MoveInput0;
    // Start is called before the first frame update
     private void Awake()
    {
        cr = GetComponent<CharacterController>();
       
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
        
    }
    private void FixedUpdate() 
    {
        Move();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     private void Move()
    {
        
        MoveInput0=inputControl.GamePlay.Move.ReadValue<Vector2>();
        float horizontal1 = inputControl.GamePlay.Move.ReadValue<Vector2>().x;
        float vertical1 = inputControl.GamePlay.Move.ReadValue<Vector2>().y;

       
         Vector3 moveDirection = new Vector3(horizontal1, 0f, vertical1).normalized;
        cr.Move(new Vector3(MoveInput0.x*MoveSpeed*Time.deltaTime,-9.81f*Time.deltaTime,MoveInput0.y*MoveSpeed*Time.deltaTime));
       if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * 1000f);
        }

        
        
        
    }

   
}
