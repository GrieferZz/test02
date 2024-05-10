using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New RewardPool",menuName ="RewardPool/Data")]
public class RewardPool_SO : ScriptableObject
{
   public List<RewardData_SO> rewardPool=new List<RewardData_SO>();
}
