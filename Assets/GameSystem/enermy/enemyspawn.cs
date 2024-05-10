using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemyspawn : MonoBehaviour
{
    public RoomStates NowRoomData;
    public List<GameObject> prefabToSpawns=new List<GameObject>();
    public int EnermyWaveNumber;
    public int EnermyNowWaveNumber;
    public int numberOfObjectsToSpawn;
    private bool CombatFinish;
    public GameObject SpawnEditor;
    public  List<GameObject> spawnAreas = new List<GameObject>(); // 生成区域的GameObject
    public List<GameObject> hasspawnedprefebs=new List<GameObject>();
    public LayerMask groundLayer; // 地面的层级
     void OnEnable() 
    {
        GameEventSystem.instance.onWaveClear+=EnermyWave;
    }
    void OnDisable() 
    {
        GameEventSystem.instance.onWaveClear-=EnermyWave;
    }
    private void Start()
    {
        
        NowRoomData=GameManager.Instance.NowRoom.GetComponent<RoomStates>();
        SpawnEditor=GameObject.FindWithTag("SpawnEditor");
        EnermySpawnInitialization();
        if(SpawnEditor!=null)
        {
            foreach (Transform child in SpawnEditor.transform)
        {
            spawnAreas.Add(child.gameObject);
        }

        }
        
        EnermyWave();
        

    }
    void EnermySpawnInitialization()
    {
        foreach(var prefab in NowRoomData.RoomData.EnermyPool)
        {
            prefabToSpawns.Add(prefab);
            
        }
        numberOfObjectsToSpawn= NowRoomData.RoomData.EnermyNumber;
        EnermyWaveNumber=NowRoomData.RoomData.EnermyWaveNumber;
        if(NowRoomData.RoomData.roomState==RoomStates_SO.RoomState.Finish)
        {
            CombatFinish=true;
            GameEventSystem.instance.RoomCombatFinish(NowRoomData.RoomData);
        }

    }

    private void SpawnObjects()
    {
        if (spawnAreas != null)
        {
            foreach (GameObject spawnArea in spawnAreas)
            {
                int randomnumber=UnityEngine.Random.Range(1,numberOfObjectsToSpawn);
                if(spawnAreas.Count==1)
                {
                    randomnumber=numberOfObjectsToSpawn;
                }
                
                for (int i = 0; i < randomnumber; i++)
            {
                if(hasspawnedprefebs.Count==numberOfObjectsToSpawn)
                {
                    break;
                }
                Vector3 randomPosition = GetRandomPositionInSpawnArea(spawnArea);
                hasspawnedprefebs.Add(Instantiate(prefabToSpawns[UnityEngine.Random.Range(0,prefabToSpawns.Count)], randomPosition, Quaternion.identity)) ;
                
                
            }

            }
            
        }
        else
        {
            Debug.LogError("Spawn area is not assigned.");
        }
    }

    private Vector3 GetRandomPositionInSpawnArea(GameObject _spawnarea)
    {
        // 获取生成区域的边界
        Bounds spawnAreaBounds = _spawnarea.GetComponent<Renderer>().bounds;
        
        
        // 随机生成位置
        float randomX = Random.Range(spawnAreaBounds.min.x, spawnAreaBounds.max.x);
        float randomZ = Random.Range(spawnAreaBounds.min.z, spawnAreaBounds.max.z);
        
        Vector3 randomPosition = new Vector3(randomX,_spawnarea.transform.position.y , randomZ);
        if (Physics.Raycast(randomPosition, Vector3.down, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            // 获取地面上的位置
            Vector3 groundPosition = hit.point;

            // 获取地面的法线向量（垂直于地面的方向）
            Vector3 groundNormal = hit.normal;

            // 设置角色位置
            randomPosition= groundPosition + groundNormal * 0.1f; // 0.1f 是为了略微抬高角色，以避免陷入地面
        }
        return randomPosition;
    }
    IEnumerator DelayedAction()
    {
        yield return new WaitForSeconds(1f); // 延迟两秒

        // 在这里执行需要延迟执行的操作
          SpawnObjects();
    }
    public void EnermyWave()
    {
        if(!CombatFinish)
        {
            EnermyNowWaveNumber++;
        if(EnermyNowWaveNumber<=EnermyWaveNumber)
        {
            hasspawnedprefebs.Clear();
             StartCoroutine(DelayedAction());
        }
        
        if(EnermyNowWaveNumber>EnermyWaveNumber)
        {
            GameEventSystem.instance.RoomCombatFinish(NowRoomData.RoomData);
            Debug.Log("战斗结束");
        }
         if(EnermyWaveNumber==0)
        {
            GameEventSystem.instance.RoomCombatFinish(NowRoomData.RoomData);
            
            Debug.Log("战斗结束");
        }
        }
        
        
            
    }
    
}
