using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "_Dot", menuName = "BuffSystem/Dot", order = 0)]
public class Dot: BaseBuffModule
{
    public float addition;
    public BuffInfo Buffinfo;
    public DotManager dotManager;

    public override void Apply(BuffInfo buffInfo, DamageInfo damageInfo = null)
    {
        Buffinfo=new BuffInfo();
        Buffinfo.creator=buffInfo.creator;
        Buffinfo.target=buffInfo.target;
        Debug.Log("Dot施加对象"+Buffinfo.target);
        dotManager=Buffinfo.target.AddComponent<DotManager>();
        dotManager.target=Buffinfo.target;
        dotManager.creator=Buffinfo.creator;
        dotManager.addition=addition;
        dotManager.i++;
        dotManager.DotUpdate();
    }
    public void DotUpdate()
    {
        
            // 每秒执行的代码
           Buffinfo.target.GetComponent<CharacterStates>().currentHealth+=(int)(addition*Buffinfo.creator.GetComponent<CharacterStates>().attackData.currentAttack);
           GameEventSystem.instance.HealthBarUpdate(Buffinfo.target);
           Debug.Log("Dot"+Buffinfo.target);

        

    }

    public override void Remove()
    {
        Debug.Log("Dot消除"+dotManager.gameObject);
       
        Destroy(dotManager);
        
    }
}

