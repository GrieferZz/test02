using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor.Media;
using UnityEngine;

public class CharacterStates : MonoBehaviour
{
    public CharacterData_SO templateCharacterData;
    
    public CharacterData_SO characterData;
    
    public AttackData_SO attackData;
   public AttackData_SO templateattackData;
    [HideInInspector]
    public bool isCritical;
    void Awake()
    {
        if( templateCharacterData!=null)
        {
            characterData=Instantiate( templateCharacterData);
            attackData=Instantiate(templateattackData);
        }
        
    }
    #region region  Read from Data_SO
    public int MaxHealth
    {
        get
        {
            if(characterData!=null)
            
                return characterData.MaxHealth;
            
            else
            return 0;
        }
        set
        {
            characterData.MaxHealth=value;
        }
    }
     public int currentHealth
    {
        get
        {
            if(characterData!=null)
            
                return characterData. currentHealth;
            
            else
            return 0;
        }
        set
        {
            characterData. currentHealth=value;
        }
    }
     public int BaseDefence
    {
        get
        {
            if(characterData!=null)
            
                return characterData.BaseDefence;
            
            else
            return 0;
        }
        set
        {
            characterData.BaseDefence=value;
        }
    }
     public int currentDefence
    {
        get
        {
            if(characterData!=null)
            
                return characterData.currentDefence;
            
            else
            return 0;
        }
        set
        {
            characterData.currentDefence=value;
        }
    }
     public int BaseSpeed
    {
        get
        {
            if(characterData!=null)
            
                return characterData.BaseSpeed;
            
            else
            return 0;
        }
        set
        {
            characterData.BaseSpeed=value;
        }
    }
    public int currentSpeed
    {
        get
        {
            if(characterData!=null)
            
                return characterData.currentSpeed;
            
            else
            return 0;
        }
        set
        {
            characterData.currentSpeed=value;
        }
    }
    #endregion
    #region  Character Combat
    public void ExecuteAttack(CharacterStates attacker,CharacterStates defender,AttackInfo attackInfo)
    {
       int damage=(int)Mathf.Max(attacker.CurrentDamage(attackInfo)*(1-defender.currentDefence/(defender.currentDefence+10f))*(1+attackInfo.additionAttackMagnification),0.1f);
       if(damage>0)
       {
        AttackManager.instance.HurtEvent(attacker.gameObject,gameObject,attackInfo);
        
       }
       currentHealth=Mathf.Max(currentHealth-damage,0);
       if(currentHealth<=0f)
       {
        AttackManager.instance.KillEvent(attacker.gameObject,gameObject);
       }
       Debug.Log("单次倍率"+attackInfo.singleAttackMagnification);
       Debug.Log("造成伤害"+damage);

    }
    private void ExecutecurrentData()
    {
        attackData.currentAttack=attackData.BaseAttack*(1+attackData.currentAttackAddition);
        characterData.MaxHealth= (int)(characterData.BaseHealth*(1+characterData.currentHealthAddition));
        characterData.currentDefence= (int)(characterData.BaseDefence*(1+characterData.currentDefenceAddition));
        characterData.currentSpeed= (int)(characterData.BaseSpeed*(1+characterData.currentSpeedAddition));
    }

    private int CurrentDamage(AttackInfo attackInfo)
    {
        float finaldamage=attackData.currentAttack*(1+attackData.currentAttackMagnification)*(attackInfo.singleAttackMagnification);
        if(isCritical)
        {
            finaldamage*=attackData.currentCriticalMulplier;
        }
        return (int)finaldamage;
    }
    #endregion
    
    void DataInitialization()
    {
        characterData.currentHealth=characterData.MaxHealth;
        characterData.currentDefence=characterData.BaseDefence;
        characterData.currentSpeed=characterData.BaseSpeed;
        attackData.currentAttack=attackData.BaseAttack;
    }
    void Update()
    {
        ExecutecurrentData();
        
    }
    void Start() 
    {
        DataInitialization();
    }
}

