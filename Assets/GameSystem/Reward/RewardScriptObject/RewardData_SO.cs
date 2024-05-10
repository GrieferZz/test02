using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Reward",menuName ="RewardData/Data")]
public class RewardData_SO : ScriptableObject
{
    public string rewardId;
    public string rewardName;

     [TextArea(3, 10)]
    public string effectDescription;
    public string backgroundDescription;
    public Sprite rewardicon;
    public GameObject rewardPrefab;
    public BuffData rewardExert;

    public static implicit operator GameObject(RewardData_SO v)
    {
        throw new NotImplementedException();
    }
}
