using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Reward",menuName ="RewardData/Data")]
public class RewardSpawn : MonoBehaviour
{
    public string rewardName;
    public string effectDescription;
    public string backgroundDescription;
    public Sprite rewardicon;
    public GameObject rewardPrefab;
    public BuffData rewardBuffData;
}
