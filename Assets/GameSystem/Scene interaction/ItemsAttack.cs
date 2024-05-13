using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsAttack : MonoBehaviour
{
    public CharacterStates InitiatorStates;
    public CharacterStates TargetStates;
    public AttackInfo attackInfo=new AttackInfo();
    private Bullet bullet;

    // Start is called before the first frame update
    void Start()
    {
        InitiatorStates=GetComponent<CharacterStates>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     
    public void Hit(GameObject target)
    {
        if(target!=null&&InitiatorStates!=null&&target.GetComponent<CharacterStates>()!=null)
        {
             Debug.Log("攻击发起者"+InitiatorStates);
             TargetStates=target.GetComponent<CharacterStates>();
             attackInfo.singleAttackMagnification=1f;
             TargetStates.ExecuteAttack(InitiatorStates,TargetStates,attackInfo);
             AttackManager.instance.AttackEvent(InitiatorStates.gameObject,target,bullet);
             GameEventSystem.instance.HealthBarUpdate(target);
        }
        else
        {
            Debug.Log("无目标");
        }
       


    }
}
