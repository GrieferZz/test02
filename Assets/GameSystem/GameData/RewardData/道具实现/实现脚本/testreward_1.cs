using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "效果", menuName = "道具效果/移速加成", order = 0)]
public class testreward_1 : BaseBuffModule
{
    
    public BuffData buffData;
    public BuffInfo testBuffInfo;
   
    public override void Apply(BuffInfo buffInfo, DamageInfo damageInfo = null)
    {
        if(buffInfo!=null)
        {
        testBuffInfo=new BuffInfo();
        testBuffInfo.creator=buffInfo.creator;
        testBuffInfo.target=GameObject.Find("Player");
        testBuffInfo.buffData=Instantiate(buffData);
        AttackManager.instance.onHurtEvent+=TriggerCheck;
       
       
        }
        
        
    }

    public override void Remove()
    {
         AttackManager.instance.onHurtEvent-=TriggerCheck;
    }

    public void TriggerCheck(GameObject attacker,GameObject defender,AttackInfo attackInfo)
    {
        if(attacker==testBuffInfo.target)
        {
             Debug.Log("道具效果检测"+testBuffInfo.buffData.OnCreate);
             testBuffInfo.target.GetComponent<BuffHandler>()?.AddBuff(testBuffInfo);
             Debug.Log("道具效果检测"+testBuffInfo.buffData.OnCreate);

        }

    }

    
}
