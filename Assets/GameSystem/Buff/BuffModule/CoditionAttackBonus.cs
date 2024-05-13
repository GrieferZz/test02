using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HealthBonus", menuName = "BuffSystem/ConditionAttackBonus", order = 0)]
public class CoditionAttackBonus : BaseBuffModule
{
    public enum Comparison {grearter_equal,smaller_equal}
    public Comparison comparison;
    public float attackBonus;
    public float condition;
    public BuffInfo Buffinfo=new BuffInfo();
    public void Start()
    {
        

    }
    public override void Apply(BuffInfo buffInfo, DamageInfo damageInfo = null)
    {
         Buffinfo.creator=buffInfo.target;
         
         AttackManager.instance.onAttackEvent+=AttackBonus;
        Debug.Log("增伤");
            
            
        
        
        
    }
    private void AttackBonus(GameObject creator, GameObject target, Bullet bullet)
    {
        
        if(Buffinfo.creator==creator&&target.CompareTag("Enermy"))
        {
            Debug.Log("增伤"+creator+target);
            if(bullet.weaponStates.weaponStates.weaponType==WeaponStates_SO.WeaponType.Follow)
            {
               
               switch(comparison)
            {
            case Comparison.grearter_equal:
            if(target.GetComponent<CharacterStates>().currentHealth/target.GetComponent<CharacterStates>().MaxHealth>=condition)
            {
                bullet.attackInfo.additionAttackMagnification=attackBonus;
            }
            

            break;
            case Comparison.smaller_equal:
             if(target.GetComponent<CharacterStates>().currentHealth/target.GetComponent<CharacterStates>().MaxHealth<=condition)
            {
                bullet.attackInfo.additionAttackMagnification=attackBonus;
            }

            break;
            }
               
               //AttackManager.instance.onAttackEvent-=AttackBonus;
               
            }
            
        }
       
    }
    public override void Remove()
    {
        AttackManager.instance.onAttackEvent-=AttackBonus;
    }
}
