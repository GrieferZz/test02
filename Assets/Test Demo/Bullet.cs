using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class Bullet : MonoBehaviour
{   public Bullet bullet;
    public CharacterStates InitiatorStates;
    public CharacterStates TargetStates;
    public WeaponStates weaponStates;
    public GameObject Target;
    public enum AttackObject{ForEnermy,ForPlayer,ForOther}
    public AttackObject attackObject;
   
    
    public AttackInfo attackInfo;
    // Start is called before the first frame update
    void Start()
    {
        bullet=this;
        weaponStates=GetComponent<WeaponStates>();
        attackInfo=new AttackInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) 
    {
      if(weaponStates.weaponStates.weaponType!=WeaponStates_SO.WeaponType.Sticky)
      {

      
        if (other.gameObject.CompareTag("Wall"))
        {
            // 子弹碰撞到玩家时销毁子弹对象
            Destroy(gameObject);
        }
        switch(attackObject)
        {
            case AttackObject.ForEnermy:
                if(other.gameObject.CompareTag("Enermy"))
                {
                    gameObject.GetComponent<SphereCollider>().enabled=false;
                    
                    Target=other.gameObject;
                    Hit();
                    Destroy(gameObject);
                    
                }
                 break;
            case AttackObject.ForPlayer:
                if(other.gameObject.CompareTag("Player"))
                {
                    gameObject.GetComponent<SphereCollider>().enabled=false;
                    
                    Target=other.gameObject;
                    Hit();
                    Debug.Log("击中");
                    Destroy(gameObject);
                   
                }
                 break;
            case AttackObject.ForOther:
                if(other.gameObject.CompareTag("Other"))
                {
                    gameObject.GetComponent<SphereCollider>().enabled=false;
                    
                    Target=other.gameObject;
                    Hit();
                    Destroy(gameObject);
                    
                }
                 break;
        }
      }
    }
    public void StickyBulletHit(GameObject target)
    {
        if(weaponStates.weaponStates.weaponType==WeaponStates_SO.WeaponType.Sticky)
        {
            switch(attackObject)
        {
            case AttackObject.ForEnermy:
                if(target.CompareTag("Enermy"))
                {
                    gameObject.GetComponent<SphereCollider>().enabled=false;
                    
                    Target=target;
                    Hit();
                    Debug.Log("爆炸击中");
                    //Destroy(gameObject);
                    
                }
                 break;
            case AttackObject.ForPlayer:
                if(target.CompareTag("Player"))
                {
                    gameObject.GetComponent<SphereCollider>().enabled=false;
                    //Destroy(gameObject);
                    Target=target.gameObject;
                    Hit();
                    
                    //Destroy(gameObject);
                   
                }
                 break;
            case AttackObject.ForOther:
                if(target.CompareTag("Other"))
                {
                    gameObject.GetComponent<SphereCollider>().enabled=false;
                    //Destroy(gameObject);
                    Target=target.gameObject;
                    Hit();
                    //Destroy(gameObject);
                    
                }
                 break;
        }

        }

    }
   
    void Hit()
    {
        if(Target!=null&&InitiatorStates!=null)
        {
            Debug.Log("攻击发起者"+InitiatorStates);
             TargetStates=Target.GetComponent<CharacterStates>();
             Debug.Log("当前爆炸倍率"+attackInfo.singleAttackMagnification);
             TargetStates.ExecuteAttack(InitiatorStates,TargetStates,attackInfo);
             AttackManager.instance.AttackEvent(InitiatorStates.gameObject,Target,bullet);
            
             GameEventSystem.instance.HealthBarUpdate(Target);
        }
        else
        {
            Debug.Log("无目标");
        }
       


    }
}
