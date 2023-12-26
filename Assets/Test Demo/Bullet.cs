using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class Bullet : MonoBehaviour
{   public CharacterStates InitiatorStates;
    public CharacterStates TargetStates;
    public GameObject Target;
    public enum AttackObject{ForEnermy,ForPlayer,ForOther}
    public AttackObject attackObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) 
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
                    Destroy(gameObject);
                    Target=other.gameObject;
                    Hit();
                    
                }
                 break;
            case AttackObject.ForPlayer:
                if(other.gameObject.CompareTag("Player"))
                {
                    gameObject.GetComponent<SphereCollider>().enabled=false;
                    Destroy(gameObject);
                    Target=other.gameObject;
                    Hit();
                    Debug.Log("击中");
                   
                }
                 break;
            case AttackObject.ForOther:
                if(other.gameObject.CompareTag("Other"))
                {
                    gameObject.GetComponent<SphereCollider>().enabled=false;
                    Destroy(gameObject);
                    Target=other.gameObject;
                    Hit();
                    
                }
                 break;
        }
        
    }
   
    void Hit()
    {
        if(Target!=null&&InitiatorStates!=null)
        {
             TargetStates=Target.GetComponent<CharacterStates>();
             TargetStates.ExecuteAttack(InitiatorStates,TargetStates);
             GameEventSystem.instance.HealthBarUpdate(Target);
        }
       


    }
}
