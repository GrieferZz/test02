using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnermyAttack_BombDog : Action
{
    public GameObject stickyBombPrefab;
    public GameObject stickyBomb;
    public GameObject self;
    public float explosionRange;
    public float attackMultiplier;
    public InstantiationHelper instantiationHelper;
    public NavMeshAgent agent;

    public override void OnStart()
    {
        if(stickyBomb==null)
        {
        stickyBomb=instantiationHelper.HelperInstantiate(stickyBombPrefab);
        stickyBomb.transform.position=transform.position;
        stickyBomb.transform.parent=transform;
        
        
        
            
        }
        
    }
    public override TaskStatus OnUpdate()
    {
        agent.speed=0;
        if(stickyBomb.GetComponent<Bullet>().attackInfo!=null)
        {
        stickyBomb.GetComponent<Bullet>().InitiatorStates=self.GetComponent<CharacterStates>();
        stickyBomb.GetComponent<Bullet>().attackObject=Bullet.AttackObject.ForPlayer;
        stickyBomb.gameObject.transform.localScale= Vector3.one*explosionRange;
        stickyBomb.GetComponent<Bullet>().attackInfo.singleAttackMagnification=attackMultiplier;
        stickyBomb.GetComponent<StickyBomb>().StickyBulletExplode();
        return TaskStatus.Success;

        }
        
        // 返回成功状态，表示行为已经完成
        return TaskStatus.Running;
    }
}
