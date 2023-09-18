using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
public class enemyspawn : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public int numberOfObjectsToSpawn;
    public GameObject SpawnEditor;
    public  List<GameObject> spawnAreas = new List<GameObject>(); // 生成区域的GameObject
    public List<GameObject> hasspawnedprefebs=new List<GameObject>();

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

        Vector3 randomPosition = new Vector3(randomX, 0f, randomZ);

        return randomPosition;
    }
    IEnumerator DelayedAction()
    {
        yield return new WaitForSeconds(2f); // 延迟两秒

        // 在这里执行需要延迟执行的操作
          SpawnObjects();
    }
}
