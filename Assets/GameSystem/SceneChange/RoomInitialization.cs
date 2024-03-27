using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomInitialization : MonoBehaviour
{
    public RoomStates NowRoomData;
    public NavMeshSurface navMeshSurface;
    public GameObject terrain;
    
    // Start is called before the first frame update
    void Start()
    {
        NowRoomData=GameManager.Instance.NowRoom.GetComponent<RoomStates>();
        TerrainGenerate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void TerrainGenerate()
    {
        if(NowRoomData.TerrainPrefab!=null)
        {
             terrain= Instantiate(NowRoomData.TerrainPrefab);
             if(NowRoomData.RoomData.roomType==RoomStates_SO.RoomType.Origin)
             {
                if(NowRoomData.gameObject.GetComponent<RoomInformation>().hasUp)
                {
                   terrain.transform.GetChild(0).transform.Rotate(0,-180f,0);
                }
                 if(NowRoomData.gameObject.GetComponent<RoomInformation>().hasDown)
                {
                   //terrain.transform.Rotate(0,-180f,0);
                }
                 if(NowRoomData.gameObject.GetComponent<RoomInformation>().hasLeft)
                {
                   terrain.transform.GetChild(0).Rotate(0,-270f,0);
                }
                 if(NowRoomData.gameObject.GetComponent<RoomInformation>().hasRight)
                {
                   terrain.transform.GetChild(0).Rotate(0,-90f,0);
                }
             }
             navMeshSurface.BuildNavMesh();
             GameEventSystem.instance.SceneLoad();
        }
       
    }
}
