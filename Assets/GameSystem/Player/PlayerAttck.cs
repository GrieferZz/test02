using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttck : MonoBehaviour
{
    private Animator anim;

    public PlayerInput inputControl;
    public int combo=0;
    private bool cd;
    private void Awake()
    {
        inputControl=new PlayerInput();
        inputControl.GamePlay.Attack.started+=BasicAttack;
    }
     private void OnEnable() 
    {
        inputControl.Enable();
    }
    private void OnDisable() 
    {
        inputControl.Disable();
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
            Player.Instance.NowState=Player.PlayerState.Fight;
            if(combo>3)
            combo=0;
        
            combo++;
            StartCoroutine(ResetAttackState(1f));
            
            anim.SetTrigger("attack"+combo);
            Debug.Log("成功");

        
        
        
    }
    private IEnumerator ResetAttackState(float delay)
    {
        int recordcombo=combo;
        cd = true;
        yield return new WaitForSeconds(delay);
        cd = false;
        if(combo-recordcombo==0)
        {
            combo=0;
            Player.Instance.NowState=Player.PlayerState.Idle;
        }
    }
}
