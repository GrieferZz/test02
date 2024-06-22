using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    public CharacterStates playerStates;
    public List<CharacterStates> EnermyStates=new List<CharacterStates>();
    public List<LayerStates_SO> LayerData= new List<LayerStates_SO>();
    public RewardPool_SO rewardPool;
    [HideInInspector]
    public RewardPool_SO currentrewardPool;
    public LayerStates_SO NowLayer;
    public GameObject NowRoom;
    public GameObject OriginRoom;
    public GameObject FinalRoom;
    public GameObject AwardRoom;
    public GameObject StoreRoom;
    private int NowLayerindex=0;
    public List<GameObject> Rooms= new List<GameObject>();
    protected override void Awake()
    {
        base.Awake();
        RigisterLayer();
        RigisterRewardPool();
        
    }
    
    public void RigisterPlayer(CharacterStates player)
    {
        playerStates=player;
    }
    public void RigisterEnermy(CharacterStates enermy)
    {
        
        if(!EnermyStates.Contains(enermy))
        {
             EnermyStates.Add(enermy);

        }
       
    }
    public void RigisterLayer()
    {    
        NowLayer=LayerData[NowLayerindex];

    }
    public void RigisterRewardPool()
    {    
        currentrewardPool=Instantiate(rewardPool);
       
    }
    public void RigisterRooms(GameObject room)
    {
        if(!Rooms.Contains(room))
        {
            Rooms.Add(room);
        }
         
        
    }
    public void RigisterNowRoom(GameObject room)
    {
           NowRoom=room;
    }
    public void RigisterOriginRoom(GameObject room)
    {
           OriginRoom=room;
    }
    public void RigisterFinalRoom(GameObject room)
    {
           FinalRoom=room;
           
    }
    public void RigisterAwardRoom(GameObject room)
    {
           AwardRoom=room;
          
    }
    public void RigisterStoreRoom(GameObject room)
    {
           StoreRoom=room;
           if(StoreRoom!=null)
        {
            UnityEngine.Debug.Log(Rooms.Count);
            RoomsLoad();
        }
    }
    public void RoomsLoad()
    {
        foreach(GameObject room in Rooms)
        {
            if(room==OriginRoom)
            {
                room.GetComponent<RoomStates>().RoomData=Instantiate(NowLayer.OriginRoomTypes[UnityEngine.Random.Range(0,NowLayer.OriginRoomTypes.Count)]);
                room.GetComponent<RoomStates>().TerrainSelect();
                UnityEngine.Debug.Log("初始化初始房间");

            }
              
            else if(room==AwardRoom)
            {
                room.GetComponent<RoomStates>().RoomData=Instantiate(NowLayer.RewardRoomTypes[UnityEngine.Random.Range(0,NowLayer.RewardRoomTypes.Count)]);
                room.GetComponent<RoomStates>().TerrainSelect();

            }
            else if(room==StoreRoom)
            {
                room.GetComponent<RoomStates>().RoomData=Instantiate(NowLayer.StoreRoomTypes[UnityEngine.Random.Range(0,NowLayer.StoreRoomTypes.Count)]);
                room.GetComponent<RoomStates>().TerrainSelect();

            }
                
            else
            {
                room.GetComponent<RoomStates>().RoomData=Instantiate(NowLayer.BasicRoomTypes[UnityEngine.Random.Range(0,NowLayer.BasicRoomTypes.Count)]);
                room.GetComponent<RoomStates>().TerrainSelect();

            }
              
                

               
            
            

            
            
        }
    }
    public void EnermyDead(CharacterStates enermyState)
    {
        for(int i=0;i<EnermyStates.Count;i++)
        {
            if(EnermyStates[i]==enermyState)
            {
                EnermyStates[i]=null;
            }
        }
       if( EnermyStates.TrueForAll(element => element == null)&&EnermyStates.Count!=0)
       {
             UnityEngine.Debug.Log("波次清理");
            GameEventSystem.instance.WaveClear();
            EnermyStates.Clear();
       }                           
    }

    
    public void RewardPoolUpdate(RewardData_SO rewardData_SO)
{
    NowRoom.GetComponent<RoomStates>().RoomData.roomState=RoomStates_SO.RoomState.Ban;
   for(int i=0;i<currentrewardPool.rewardPool.Count;i++)
   {
       if(rewardData_SO.rewardId==currentrewardPool.rewardPool[i].rewardId)
       {
           currentrewardPool.rewardPool.Remove(currentrewardPool.rewardPool[i]);
           
       }
   }

}

}
