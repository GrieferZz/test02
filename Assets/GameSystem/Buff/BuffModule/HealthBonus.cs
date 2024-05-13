using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "HealthBonus", menuName = "BuffSystem/HealthBonus", order = 0)]
public class HealthBonus :  BaseBuffModule
{
    public enum BonusType {Plus,Addition};
    public BonusType  bonusType;
    public float healthBonus;
    public void Start()
    {
        

    }
    public override void Apply(BuffInfo buffInfo, DamageInfo damageInfo = null)
    {
        switch(bonusType)
        {
            case BonusType.Plus:
            buffInfo.target.GetComponent<CharacterStates>().characterData.MaxHealth+=(int)healthBonus;
            buffInfo.target.GetComponent<CharacterStates>().characterData.currentHealth+=(int)healthBonus;

            break;
            case BonusType.Addition:
            buffInfo.target.GetComponent<CharacterStates>().characterData.currentHealthAddition+=healthBonus;
            buffInfo.target.GetComponent<CharacterStates>().characterData.currentHealth+=(int)(buffInfo.target.GetComponent<CharacterStates>().characterData.BaseHealth*healthBonus);;
            break;
        }
        
        
    }

    public override void Remove()
    {
        
    }
}
