using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "SpeedUP", menuName = "BuffSystem/SpeedUP", order = 0)]
public class SpeedUP :  BaseBuffModule
{
    public float speedAddition;
    public void Start()
    {
        

    }
    public override void Apply(BuffInfo buffInfo, DamageInfo damageInfo = null)
    {
        
        buffInfo.target.GetComponent<CharacterStates>().characterData.currentSpeedAddition+=speedAddition;
        
    }

    public override void Remove()
    {
        
    }
}
