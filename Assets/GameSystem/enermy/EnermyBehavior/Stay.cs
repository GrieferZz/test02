using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class Stay : Action
{
    public override TaskStatus OnUpdate()
    {
        return  TaskStatus.Running;
    }
}
