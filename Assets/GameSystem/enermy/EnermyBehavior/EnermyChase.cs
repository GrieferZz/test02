
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class EnermyChase : Action
{
   public NavMeshAgent agent;
   public CharacterStates characterStates;

   public SharedGameObject attackTarget;
   public override void OnAwake()
    {
       
        
    }

    public override TaskStatus OnUpdate()
    {
        if (attackTarget.Value != null)
        {
            // 设置移动速度
            agent.speed = characterStates.characterData.currentSpeed;

            // 设置目标位置
            agent.destination = attackTarget.Value.transform.position;

            // 计算目标方向
            Vector3 direction = attackTarget.Value.transform.position - transform.position;
            direction.y = 0; // 保持水平面上的方向

            // 计算旋转目标
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // 平滑旋转至目标方向
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * agent.angularSpeed);

            return TaskStatus.Running;
        }
        
                  
                 
        return TaskStatus.Failure;
    }
    
}
