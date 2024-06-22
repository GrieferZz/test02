using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Data",menuName ="CharacterStates/Data")]
public class CharacterData_SO : ScriptableObject
{
    [Header("Stats Info")]
    public int MaxHealth;
    public int BaseHealth;
    public int currentHealth;
    public int BaseDefence;
    public int currentDefence;
    public float BaseSpeed;
    public float currentSpeed;
    public float currentHealthAddition;
    public float currentDefenceAddition;
    public float currentSpeedAddition;

}
