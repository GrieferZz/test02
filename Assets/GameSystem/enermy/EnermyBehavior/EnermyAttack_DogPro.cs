
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnermyAttack_DogPro : Action
{
    public NavMeshAgent agent;
    
    public EnermyAttack enermyAttack;
       // 子弹预制体
    private float lastShootTime;
    
    public SharedGameObject attackTarget; 
     public CharacterStates characterStates; 
    public int bulletAmount;
    public float angle;        // 玩家的Transform
    

    public override void OnAwake()
    {
        
        
    }
    public override TaskStatus OnUpdate()
    {
        agent.speed=0;
        agent.destination = attackTarget.Value.transform.position;
         Vector3 direction = attackTarget.Value.transform.position - transform.position;
            direction.y = 0; // 保持水平面上的方向

            // 计算旋转目标
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // 平滑旋转至目标方向
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * agent.angularSpeed);
  
        if(enermyAttack.nowBulletPrefab!=null)
        {
            if (Time.time - lastShootTime >= (1/characterStates.attackData.currentAttackSpeed)*enermyAttack.nowBulletPrefab.shootInterval)
        {
            
            enermyAttack.ShootPro(bulletAmount,angle);
            lastShootTime = Time.time;
        }

        }
        return TaskStatus.Running;
    }
    
}
