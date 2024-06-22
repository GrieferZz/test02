using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;

public class EnermyRigister : Action
{
    public CharacterStates characterStates;

    

    public override void OnStart()
    {
        GameManager.Instance.RigisterEnermy(characterStates);
    }

    public override TaskStatus OnUpdate()
    {
        
        return TaskStatus.Success;
    }
}