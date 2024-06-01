using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "SpeedUP", menuName = "BuffSystem/WeaponAddition", order = 0)]
public class WeaponAddition :  BaseBuffModule
{
    public enum WeaponType {Follow,Track,Sticky};
    public WeaponType weaponType;


    public float addition;
    public void Start()
    {
        

    }
    public override void Apply(BuffInfo buffInfo, DamageInfo damageInfo = null)
    {
        switch(weaponType)
        {
            case WeaponType.Follow:
            buffInfo.target.GetComponent<PlayerAttck>().bulletPrefab[0].attackMultiplier+=addition;
            break;
            case WeaponType.Track:
            buffInfo.target.GetComponent<PlayerAttck>().bulletPrefab[1].attackMultiplier+=addition;
            break;
            case WeaponType.Sticky:
            buffInfo.target.GetComponent<PlayerAttck>().bulletPrefab[2].attackMultipliers[0]+=addition;
            buffInfo.target.GetComponent<PlayerAttck>().bulletPrefab[2].attackMultipliers[1]+=addition;
            buffInfo.target.GetComponent<PlayerAttck>().bulletPrefab[2].attackMultipliers[2]+=addition;
            break;
        }
        
        
    }

    public override void Remove()
    {
        
    }
}
