using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class EnermyInformation : MonoBehaviour
{
    public enum EnermyType{};
    public float MaxHealth;
    public float Attack;
    public float Defend;
    public float Speed;

    public float NowHealth;
    void Awake()
    {
        NowHealth=MaxHealth;
    }
}
