using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomStates : MonoBehaviour
{
    public RoomStates_SO RoomData;
    public GameObject TerrainPrefab;
    void OnEnable()
    {
        GameEventSystem.instance.onRoomCombatFinish+=RoomStateUpdate;
        
    }
    void OnDisable()
    {
        GameEventSystem.instance.onRoomCombatFinish-=RoomStateUpdate;
    }
    
    void RoomStateUpdate(RoomStates_SO enermyState)
    {
        if(enermyState==RoomData)
        RoomData.roomState=RoomStates_SO.RoomState.Finish;
    }
    public void TerrainSelect()
    {
        TerrainPrefab=RoomData.TerrainPool[UnityEngine.Random.Range(0,RoomData.TerrainPool.Count)];
    }
}
