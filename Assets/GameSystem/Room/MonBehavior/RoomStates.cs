using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomStates : MonoBehaviour
{
    public RoomStates_SO RoomData;
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
}
