using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttck : MonoBehaviour
{
    private Animator anim;

    public PlayerInput inputControl;
    public int combo=0;
    private bool hasIncreased = false;
    private bool cd;
    private void Awake()
    {
        inputControl=new PlayerInput();
        inputControl.GamePlay.Attack.started+=BasicAttack;
        inputControl.GamePlay.HeavyAttack.started+=Hold;
        inputControl.GamePlay.HeavyAttack.canceled += HoldRelease;
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
      Charging();
      ChargeRelease();
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
     private void Hold(InputAction.CallbackContext context)
    {
            Player.Instance.NowState=Player.PlayerState.Fight;
    
        
             anim.SetBool("IsHold",true);
             anim.SetBool("IsCharging",true);
             Debug.Log("蓄力");

        
       

    }
    private void HoldRelease(InputAction.CallbackContext context)
    {
        Player.Instance.NowState=Player.PlayerState.Idle;
        anim.SetBool("IsHold",false);
        anim.SetBool("IsCharging",false);
    }
    private void Charging()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Hold"))
        {
            // 在这里执行动画未播放完毕时的操作

        }
        else
        {
            anim.SetBool("IsHold",false);
            // 在这里执行动画播放完毕时的操作
            Debug.Log("动画播放完毕！");
        }
    }
    public  void ChargIncrease()
     {
         anim.SetInteger("Charge",  anim.GetInteger("Charge")+1);
        
        
     }
     private void ChargeRelease()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            // 在这里执行动画未播放完毕时的操作

        }
        else
        {
             anim.SetInteger("Charge", 0);

           
            
            // 在这里执行动画播放完毕时的操作
            Debug.Log("动画播放完毕！");
        }

    }
}
