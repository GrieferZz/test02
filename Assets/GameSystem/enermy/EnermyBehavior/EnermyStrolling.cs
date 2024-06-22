
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;


public class EnermyStrolling : Action
{
     public CharacterStates characterStates;
    public SharedFloat patrolRange;
    public SharedFloat LookAtTime;
    public  SharedVector3 GuardPos;
    public NavMeshAgent agent;
    private float RemainLootAtTime;
    
    public float chaseMagnification;

    public override void OnAwake()
    {
        RemainLootAtTime=LookAtTime.Value;
        GuardPos.Value=transform.position;
        
    }
    public override TaskStatus OnUpdate()
    {
        if (agent == null || !agent.isActiveAndEnabled)
    {
        return TaskStatus.Failure; // 返回任务失败状态，停止进一步操作
    }

    // 确保 characterStates 不为 null
    if (characterStates == null)
    {
        return TaskStatus.Failure; // 返回任务失败状态，停止进一步操作
    }
        {
             agent.speed=characterStates.GetComponent<CharacterStates>().characterData.currentSpeed*chaseMagnification;
        if(!agent.pathPending && agent.remainingDistance <=agent.stoppingDistance*1.5f)
        {
                    if(RemainLootAtTime>0)
                    {
                        RemainLootAtTime-=Time.deltaTime;
                    }
                    else
                    {
                        SetNewRandomDestination();

                    }
                    
        }
        
        

        }
        return TaskStatus.Running;
       
    }
     void SetNewRandomDestination()
    {
        RemainLootAtTime=LookAtTime.Value;
        Vector3 randomPoint = GetRandomPointOnNavMesh();
        if (randomPoint != Vector3.zero)
        {
            agent.SetDestination(randomPoint);
        }
    }
     Vector3 GetRandomPointOnNavMesh()
    {
        RemainLootAtTime=LookAtTime.Value;
        for (int i = 0; i < 30; i++) // 尝试30次
        {
            Vector3 randomDirection = Random.insideUnitSphere * patrolRange.Value;
            randomDirection += transform.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, patrolRange.Value, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return Vector3.zero; // 未找到有效点，返回Vector3.zero


    }
}
