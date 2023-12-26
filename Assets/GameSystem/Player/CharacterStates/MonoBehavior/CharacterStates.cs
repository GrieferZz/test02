using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor.Media;
using UnityEngine;

public class CharacterStates : MonoBehaviour
{
    public CharacterData_SO characterData;
    public AttackData_SO attackData;
    [HideInInspector]
    public bool isCritical;
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
    public void ExecuteAttack(CharacterStates attacker,CharacterStates defender)
    {
       int damage=(int)Mathf.Max(attacker.CurrentDamage()*(1-defender.currentDefence/(defender.currentDefence+10f)),0.1f);
       currentHealth=Mathf.Max(currentHealth-damage,0);
       Debug.Log("造成伤害"+damage);

    }
    private void ExecutecurrentData()
    {
        attackData.currentAttack=attackData.BaseAttack*(1+attackData.currentAttackAddition);
        characterData.currentDefence=characterData.BaseDefence*(1+characterData.currentDefenceAddition);
        characterData.currentSpeed=characterData.BaseSpeed*(1+characterData.currentSpeedAddition);
    }

    private int CurrentDamage()
    {
        float finaldamage=attackData.currentAttack*(1+attackData.currentAttackMagnification);
        if(isCritical)
        {
            finaldamage*=attackData.currentCriticalMulplier;
        }
        return (int)finaldamage;
    }
    #endregion
    void Update()
    {
        ExecutecurrentData();
    }
    void Start() 
    {
        
    }
}

