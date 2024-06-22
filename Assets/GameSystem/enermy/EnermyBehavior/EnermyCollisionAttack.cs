
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyCollisionAttack : Action
{
    public Collider collider;
    public float interval;
    public float strength;
    private float lastShootTime;
    bool cancheck;
    public CharacterStates InitiatorStates;
    public CharacterStates TargetStates;
    public AttackInfo attackInfo=new AttackInfo();

    public  override  void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
          if(cancheck)
            {
                attackInfo.strength=strength;
                Hit(other.gameObject);
                cancheck=false;

            }
            

        }
    }
    public override TaskStatus OnUpdate()
    {
        IntervalChcek();
        return TaskStatus.Running;
    }
    private void IntervalChcek()
    {
        if (Time.time - lastShootTime >=interval)
        {
            cancheck=true;
            lastShootTime = Time.time;
        }

    }
     IEnumerator CheckFalse(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); // 等待waitTime秒

        cancheck=false;
    }
    public void Hit(GameObject target)
    {
        if(target!=null&&InitiatorStates!=null&&target.GetComponent<CharacterStates>()!=null)
        {
             Debug.Log("攻击发起者"+InitiatorStates);
             TargetStates=target.GetComponent<CharacterStates>();
             TargetStates.ExecuteAttack(InitiatorStates,TargetStates,attackInfo);
             AttackManager.instance.AttackEvent(InitiatorStates.gameObject,target,null);
             GameEventSystem.instance.HealthBarUpdate(target);
        }
        else
        {
            Debug.Log("无目标");
        }
       


    }
    
}
