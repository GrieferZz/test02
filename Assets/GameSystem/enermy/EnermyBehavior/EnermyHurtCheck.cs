using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using BehaviorDesigner.Runtime;
using System;


public class EnermyHurtCheck : Conditional
{
     private bool isTaskSuccessful = false;
   public override void OnAwake()
    {
        AttackManager.instance.onAttackEvent+=HurtCheck;
    }

    private void HurtCheck(GameObject object1, GameObject object2, Bullet bullet)
    {
        if(object2==gameObject)
        isTaskSuccessful = true;
    }
    public override TaskStatus OnUpdate()
    {
        if (isTaskSuccessful)
        {
           
            return TaskStatus.Success;
        }
        else
        return TaskStatus.Failure;
        

           
    }
    public  void OnDisable()
    {
        // 确保在任务结束时解除事件绑定
        AttackManager.instance.onAttackEvent -= HurtCheck;
    }
}
