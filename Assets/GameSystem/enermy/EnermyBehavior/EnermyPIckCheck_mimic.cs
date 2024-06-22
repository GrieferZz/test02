using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class EnermyPickCheck_mimic : Conditional
{
    public Animator animator;
    public override TaskStatus OnUpdate()
    {
        if(animator.GetBool("Open"))
        {
            GameEventSystem.instance.HealthBarUpdate(gameObject);
            return TaskStatus.Success;
        }
        else
        return TaskStatus.Failure;
       
    }
}
