using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public CharacterStates playerStates;
    public List<CharacterStates> EnermyStates=new List<CharacterStates>();
    public List<LayerStates_SO> LayerData= new List<LayerStates_SO>();
    public LayerStates_SO NowLayer;
    public GameObject NowRoom;
    public GameObject OriginRoom;
    public GameObject FinalRoom;
    private int NowLayerindex=0;
    public List<GameObject> Rooms= new List<GameObject>();
    protected override void Awake()
    {
        base.Awake();
        RigisterLayer();
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
    public void RigisterRooms(GameObject room)
    {
        if(!Rooms.Contains(room))
        {
            Rooms.Add(room);
        }
         
        if(Rooms.Count>=NowLayer.RoomNumber)
        {
            Debug.Log(Rooms.Count);
            RoomsLoad();
        }
    }
    public void RigisterNowRoom(GameObject room)
    {
           NowRoom=room;
    }
    public void RoomsLoad()
    {
        foreach(GameObject room in Rooms)
        {
            Debug.Log(NowLayer.BasicRoomTypes.Count);
            room.GetComponent<RoomStates>().RoomData=Instantiate(NowLayer.BasicRoomTypes[UnityEngine.Random.Range(0,NowLayer.BasicRoomTypes.Count)]);
            room.GetComponent<RoomStates>().TerrainSelect();
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
             Debug.Log("波次清理");
            GameEventSystem.instance.WaveClear();
            EnermyStates.Clear();
       }                           
    }
}
