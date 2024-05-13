using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class AttackManager : Singleton<AttackManager>

{
    public static AttackManager instance;
    public event Action<GameObject,GameObject,Bullet> onAttackEvent;
    public event Action<GameObject,GameObject,AttackInfo> onHurtEvent;
    public event Action<GameObject,GameObject> onKillEvent;
    public event Action onShoot;
    public event Action onTest;
    protected override void Awake()
    {
        base.Awake();
        if(instance==null)
        {
            instance =this;
        }
        else
        {
            if(instance!=this)
            {
                Destroy(gameObject);
            }
        }
        
    }
    public void AttackEvent(GameObject creator,GameObject target,Bullet bullet)
    {
         if(onAttackEvent!=null)
        onAttackEvent(creator,target,bullet);
    }
    public void HurtEvent(GameObject creator,GameObject target,AttackInfo attackInfo)
    {
         if(onHurtEvent!=null)
        onHurtEvent(creator,target,attackInfo);
    }
    public void KillEvent(GameObject creator,GameObject target)
    {
         if(onKillEvent!=null)
        onKillEvent(creator,target);
    }
    public void Shoot()
    {
         if(onShoot!=null)
        onShoot();
    }
    public void Test()
    {
        Debug.Log("测试");
         if(onTest!=null)
         {
             onTest();
             

         }
       
    }
}
