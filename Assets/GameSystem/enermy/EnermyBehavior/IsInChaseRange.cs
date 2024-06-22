using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class IsInChaseRange : Conditional
{
    public SharedGameObject attackTarget;
    public SharedFloat sightRadius;

   

    public override void OnAwake()
    {
        
    }

    public override TaskStatus OnUpdate()
    {
        var colliders=Physics.OverlapSphere(transform.position,sightRadius.Value);
        foreach(var target in colliders)
        {
            if(target.CompareTag("Player"))
            {
                EnermyAttack.player=target.gameObject.transform;
                attackTarget.Value=target.gameObject;
                return TaskStatus.Success;
                
            }
        }
        attackTarget.Value=null;
        return TaskStatus.Failure;
    }
}
