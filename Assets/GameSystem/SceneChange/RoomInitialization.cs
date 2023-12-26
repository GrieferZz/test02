using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomInitialization : MonoBehaviour
{
    public RoomStates NowRoomData;
    public NavMeshSurface navMeshSurface;
    
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
             Instantiate(NowRoomData.TerrainPrefab);
             navMeshSurface.BuildNavMesh();
        }
       
    }
}
