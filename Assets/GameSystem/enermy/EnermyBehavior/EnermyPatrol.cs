
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class EnermyPatrol : Action
{
    public CharacterStates characterStates;
    public SharedFloat PatrolRange;
    public SharedFloat LookAtTime;
    public  SharedVector3 GuardPos;
    public NavMeshAgent agent;
    private float RemainLootAtTime;
    private Vector3 WayPoint;

    public override void OnAwake()
    {
        RemainLootAtTime=LookAtTime.Value;
        GuardPos.Value=transform.position;
        
    }
    public override TaskStatus OnUpdate()
    {
        agent.speed=characterStates.GetComponent<CharacterStates>().characterData.currentSpeed*0.5f;
        if(Vector3.Distance(WayPoint,transform.position)<=agent.stoppingDistance*1.5f)
        {
                    if(RemainLootAtTime>0)
                    {
                        RemainLootAtTime-=Time.deltaTime;
                    }
                    else
                    {
                        GetNewWayPoint();

                    }
                    
        }
        else
        {
                    agent.destination=WayPoint;
        }
        return TaskStatus.Running;
    }
    void GetNewWayPoint()
    {
        RemainLootAtTime=LookAtTime.Value;
        float randomX=Random.Range(-PatrolRange.Value,PatrolRange.Value);
        float randomZ=Random.Range(-PatrolRange.Value,PatrolRange.Value);
        Vector3 randomPoint=new Vector3(GuardPos.Value.x+randomX,transform.position.y,GuardPos.Value.z+randomZ);
        
        NavMeshHit hit;
        WayPoint=NavMesh.SamplePosition(randomPoint,out hit,PatrolRange.Value,1)?hit.position:transform.position;


    }
}
