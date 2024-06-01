using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "效果", menuName = "道具效果/燃烧Dot", order = 0)]
public class testreward_6 : BaseBuffModule
{
    
    public BuffData buffData;
    public BuffInfo testBuffInfo;
   
    public override void Apply(BuffInfo buffInfo, DamageInfo damageInfo = null)
    {
        
        
        
        GameEventSystem.instance.onTargetTransmission+=AttackAddition;
       
       
        
        
        
    }

    public override void Remove()
    {
         //AttackManager.instance.onKillEvent-=TriggerCheck;
          GameEventSystem.instance.onTargetTransmission-=AttackAddition;
    }

    public void AttackAddition(GameObject parent,GameObject target)
    {
        if(target.CompareTag("Enermy"))
        {
            
        {
             testBuffInfo=new BuffInfo();
             testBuffInfo.creator=GameObject.FindWithTag("Player");
             testBuffInfo.target=target;
             testBuffInfo.buffData=Instantiate(buffData);
             Debug.Log("道具效果检测"+testBuffInfo.buffData.OnCreate);
             target.GetComponent<BuffHandler>()?.AddBuff(testBuffInfo);
             Debug.Log("道具效果检测"+target);

        }

    }

    
}
}
