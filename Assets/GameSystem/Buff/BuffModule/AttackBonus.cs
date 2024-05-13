using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HealthBonus", menuName = "BuffSystem/AttackBonus", order = 0)]
public class AttackBonus : BaseBuffModule
{
    public float attackBonus;
    public void Start()
    {
        

    }
    public override void Apply(BuffInfo buffInfo, DamageInfo damageInfo = null)
    {
        
        buffInfo.target.GetComponent<CharacterStates>().attackData.currentAttackAddition+=attackBonus;
            
            
        
        
        
    }
    public override void Remove()
    {
        
    }
}
