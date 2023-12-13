using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player :Singleton<Player>
{
    
    public enum PlayerState{Move,Sprint,Fight,Idle,Hurt,Ban}
    public PlayerState NowState;
    public float Health;
    public float Attack;
    public float Defence;
    public float Speed;
    public bool canMove=true;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    private void Start() 
    {
        NowState=PlayerState.Idle;
    }
    private void Update() 
    {
        StateChange();
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
        canMove=false;
        // 执行Attack状态的代码
        break; // 退出switch语句
    case PlayerState.Ban:
        canMove=false;
        // 执行Attack状态的代码
        break; // 退出switch语句

    default:
        // 如果没有匹配的状态
        break; // 退出switch语句
}
    }
}
