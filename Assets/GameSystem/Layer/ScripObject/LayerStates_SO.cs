using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
[CreateAssetMenu(fileName ="New Layer",menuName ="LayerStates/Data")]
public class LayerStates_SO : ScriptableObject
{
    public int LayerNumber;
    public int RoomNumber;
    public List<RoomStates_SO> OriginRoomTypes=new List<RoomStates_SO>();
    public List<RoomStates_SO> BasicRoomTypes=new List<RoomStates_SO>();
    public List<RoomStates_SO> StoreRoomTypes=new List<RoomStates_SO>();
    public List<RoomStates_SO> RewardRoomTypes=new List<RoomStates_SO>();
    public List<RoomStates_SO> BossRoomTypes=new List<RoomStates_SO>();
}
