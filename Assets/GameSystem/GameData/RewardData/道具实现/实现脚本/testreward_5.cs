using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "效果", menuName = "道具效果/攻击倍率", order = 0)]
public class testreward_5 : BaseBuffModule
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
        AttackAddition();
       
       
        }
        
        
    }

    public override void Remove()
    {
         //AttackManager.instance.onKillEvent-=TriggerCheck;
    }

    public void AttackAddition()
    {
        
        {
             Debug.Log("道具效果检测"+testBuffInfo.buffData.OnCreate);
             testBuffInfo.target.GetComponent<BuffHandler>()?.AddBuff(testBuffInfo);
             Debug.Log("道具效果检测"+testBuffInfo.buffData.OnCreate);

        }

    }

    
}
