using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player :Singleton<Player>
{
    
    public enum PlayerState{Move,Sprint,Fight,Idle,Hurt,Ban,Dead}
    public PlayerState NowState;
    public CharacterStates playerStates;
    
    public bool canMove=true;
    protected override void Awake()
    {
        base.Awake();
        playerStates=GetComponent<CharacterStates>();
        DontDestroyOnLoad(this);
    }
    private void Start() 
    {
        NowState=PlayerState.Idle;
        GameManager.Instance.RigisterPlayer(playerStates);
        //AttackManager.instance.onAttackEvent+=beAttacked;
    }

    private void beAttacked(GameObject object1, GameObject object2, Bullet bullet)
    {
        throw new NotImplementedException();
    }

    private void Update() 
    {
        StateChange();
        StatesUpdate();
    }
     private void StateChange()
    {
         switch (NowState)
{
    case PlayerState.Idle:
        canMove=true;
        break; // 退出switch语句

    case PlayerState.Move:
        // 执行Move状态的代码
        break; // 退出switch语句

    case PlayerState.Fight:
        //canMove=false;
        // 执行Attack状态的代码
        break; // 退出switch语句
    case PlayerState.Hurt:
        canMove=false;
        break;
    case PlayerState.Ban:
        canMove=false;
        // 执行Attack状态的代码
        break; // 退出switch语句
    case PlayerState.Dead:
        canMove=false;
        GetComponent<PlayerAttck>().enabled=false;
        // 执行Attack状态的代码
        break; // 退出switch语句

    default:
        // 如果没有匹配的状态
        break; // 退出switch语句
}
    }
    void StatesUpdate()
    {
        if(playerStates.currentHealth<=0f)
        {
            
            if(playerStates!=null)
            {
                NowState=Player.PlayerState.Dead;
            }

        }
    }
    
    }
