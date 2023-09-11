using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBaseState : State
{
    //the duration of the state
    public float duration;

    protected Animator animator;

    //move on to next move or stick with the current one
    protected bool shouldCombo;

    //index of current attack
    protected int attackIndex;
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        animator = GetComponent<Animator>();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if(Input.GetMouseButtonDown(0))
        {
            shouldCombo = true;
        }
    }

    public override void OnExit()
    {
        base.OnExit(); 
    }
}
