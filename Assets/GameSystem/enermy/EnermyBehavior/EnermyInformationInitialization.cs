using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class EnermyInformationIn : Action
{
    public EnermyInformationBar enermyInformationBar;
    public bool visibility;
    public override void OnStart()
    {
        enermyInformationBar.InformationBarInitialization(visibility);
    }
    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
