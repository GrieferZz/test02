using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public enum EnermyStates{Guard,Patrol,Chase,Attack,Dead}
public enum EnermyType{CloseCombat,RemoteCombat}
public class enermybehaviour : MonoBehaviour
{
    public EnermyStates enermyStates;
    public EnermyType enermyType;

    public NavMeshAgent agent;
    
    private GameObject AttackTarget;
    public float SightRadius;
    public float Speed;
    public float LookAtTime;
    private float RemainLootAtTime;
    
    public float PatrolRange;
    private Vector3 WayPoint;
    private Vector3 GuardPos;

    void Awake()
    {
        agent=gameObject.GetComponent<NavMeshAgent>();
        agent.speed=Speed;
        GuardPos=transform.position;
        RemainLootAtTime=LookAtTime;
    }
    private void Start() 
    {
        enermyStates=EnermyStates.Patrol;
        GetNewWayPoint();
    }
    private void Update() 
    {
        SwitchStates();
        
    }
    // Update is called once per frame
    
    void GetNewWayPoint()
    {
        RemainLootAtTime=LookAtTime;
        float randomX=Random.Range(-PatrolRange,PatrolRange);
        float randomZ=Random.Range(-PatrolRange,PatrolRange);
        Vector3 randomPoint=new Vector3(GuardPos.x+randomX,transform.position.y,GuardPos.z+randomZ);
        
        NavMeshHit hit;
        WayPoint=NavMesh.SamplePosition(randomPoint,out hit,PatrolRange,1)?hit.position:transform.position;


    }
    void SwitchStates()
    {
        if(FoundPlayer())
        {
            enermyStates=EnermyStates.Chase;
        }
        switch(enermyStates)
        {
            case EnermyStates.Guard:
                 break;
            case EnermyStates.Patrol:
                  agent.speed=Speed*0.5f;
                  if(Vector3.Distance(WayPoint,transform.position)<=agent.stoppingDistance)
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
                  break;
            case EnermyStates.Chase:
                  agent.speed=Speed;
                  if(!FoundPlayer())
                  {
                    if(RemainLootAtTime>0)
                    {
                        agent.destination=transform.position;
                        RemainLootAtTime-=Time.deltaTime;

                    }
                    else if(enermyType==EnermyType.CloseCombat)
                    {
                        enermyStates=EnermyStates.Patrol;
                        agent.destination=GuardPos;
                    }
                      

                  }
                  else
                  {

                    agent.destination=AttackTarget.transform.position;
                  }
                  break;
            case EnermyStates.Attack:
                  break;
            case EnermyStates.Dead:
                  break;
        }
    }
    bool FoundPlayer()
    {
        var colliders=Physics.OverlapSphere(transform.position,SightRadius);
        foreach(var target in colliders)
        {
            if(target.CompareTag("Player"))
            {
                AttackTarget=target.gameObject;
                return true;
            }
        }
        AttackTarget=null;
        return false;
    }
   private void OnDrawGizmos() 
   {
       Gizmos.color=Color.blue;
       Gizmos.DrawWireSphere(transform.position,PatrolRange);
       Gizmos.color=Color.red;
       Gizmos.DrawWireSphere(transform.position,SightRadius);
    
   }
  
}
