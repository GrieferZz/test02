using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttck : MonoBehaviour
{
    private Animator anim;

    public PlayerInput inputControl;
    private int combo;
    private void Awake()
    {
        inputControl=new PlayerInput();
        inputControl.GamePlay.Attack.started+=BasicAttack;
    }

    

    // Start is called before the first frame update
    void Start()
    {
        anim=this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void BasicAttack(InputAction.CallbackContext context)
    {
        combo++;
        anim.SetTrigger("attack1");
        
    }
}
