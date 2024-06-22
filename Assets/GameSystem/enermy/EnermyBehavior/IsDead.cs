using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class IsDead : Conditional
{
    public CharacterStates characterStates;

   

    public override void OnAwake()
    {
        
    }

    public override TaskStatus OnUpdate()
    {
        if(characterStates.currentHealth<=0f)
        {
            
            if(characterStates!=null)
            {
                return TaskStatus.Success;

            }
            
        
        }
        return TaskStatus.Failure;
    }
}