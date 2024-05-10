using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;

public class RewardPick : MonoBehaviour
{
    private bool canPick;
    public PlayerInput inputControl;
    public void Awake()
    {
        inputControl=new PlayerInput();
        inputControl.GamePlay.Pick.started+=Pick;

    }
    public void OnEnable()
    {
        inputControl.Enable();
    }
    private void OnDisable() 
    {
         inputControl.GamePlay.Pick.started-=Pick;
         inputControl.Disable();
    }
    private void Pick(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(canPick)
        {
             GameEventSystem.instance.RewardPick();
             Debug.Log("拾取");

        }
       
        
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            canPick=true;
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            canPick=false;
        }
    }
    
}
