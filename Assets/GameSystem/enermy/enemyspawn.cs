using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemyspawn : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public int numberOfObjectsToSpawn;
    public GameObject SpawnEditor;
    public  List<GameObject> spawnAreas = new List<GameObject>(); // 生成区域的GameObject
    public List<GameObject> hasspawnedprefebs=new List<GameObject>();
    public LayerMask groundLayer; // 地面的层级

    private void Start()
    {
       
        foreach (Transform child in SpawnEditor.transform)
        {
            spawnAreas.Add(child.gameObject);
        }
       StartCoroutine(DelayedAction());

    }

    private void SpawnObjects()
    {
        if (spawnAreas != null)
        {
            foreach (GameObject spawnArea in spawnAreas)
            {
                int randomnumber=Random.Range(1,numberOfObjectsToSpawn+1);
                for (int i = 0; i < randomnumber; i++)
            {
                Vector3 randomPosition = GetRandomPositionInSpawnArea(spawnArea);
                hasspawnedprefebs.Add(Instantiate(prefabToSpawn, randomPosition, Quaternion.identity)) ;
                if(hasspawnedprefebs.Count<=numberOfObjectsToSpawn)
                {
                    break;
                }
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
    
}
