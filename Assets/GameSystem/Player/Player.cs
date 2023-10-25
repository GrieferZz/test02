using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player :Singleton<Player>
{
    
    public enum PlayerState{Move,Sprint,Fight,Idle}
    public PlayerState NowState;
    public float Health;
    public float Attack;
    public float Defence;
    public float Speed;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    private void Start() 
    {
        NowState=PlayerState.Idle;
    }
}
