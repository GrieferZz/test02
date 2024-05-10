using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Room",menuName ="RoomsStates/Data")]
public class RoomStates_SO : ScriptableObject
{
    public enum RoomType{Origin,Basic,Store,Reward,Boss};
    public enum RoomState{Unload,Finish};
    public RoomType roomType;
    public RoomState roomState;
    public List<GameObject> TerrainPool=new List<GameObject>();
    public List<GameObject> EnermyPool=new List<GameObject>();
    public int  EnermyWaveNumber;
    public int EnermyNumber;
    //public List<RewardData_SO> Properties =new List<RewardData_SO>();
}
