using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Attack",menuName ="CharacterStates/Attac Data")]
public class AttackData_SO : ScriptableObject
{
    public float BasicAttackRange;
    public float currentAttackRange;
    public float BaseAttack;
    public float currentAttack;
    public float CriticalMulplier;
    public float currentCriticalMulplier;
    public float CriticalChance;
    public float currentCriticalChance;
    public float AttackSpeed;
    public float currentAttackSpeed;
    public float currentAttackAddition;
    public float currentAttackMagnification;
    
    
}
