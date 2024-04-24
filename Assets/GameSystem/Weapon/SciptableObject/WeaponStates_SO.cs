using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Weapon",menuName ="WeaponStates/Data")]
public class WeaponStates_SO : ScriptableObject
{
     public enum WeaponType
     {
        Follow,Track,Sticky
     }
     public WeaponType weaponType;
     public GameObject weaponPrefab;
     public float attackMultiplier;
     public float shootInterval;
     public float flightSpeed;


     public float steerSpeed;
     public float trackRadius;

     public float[] attackMultipliers=new float[3];
     public int maxLayers;
     public float[] explosionRange =new float[3];
     
}
