using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RoomList : MonoBehaviour
{
    public List<string> sceneNames = new List<string>();
    public List<GameObject> ChangeSceneUnits = new List<GameObject>();
    private GameObject ChangeSceneParent;
    public int EnermyAmount;
    private void Awake() 
    {
        RoomListInitialization();
        GameEventSystem.instance.onSceneLoad+=ChangeSceneListInitialization;
    }
    private void OnEnable()
    {
        
    }
    
    public void RoomListInitialization()
    {
         for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            sceneNames.Add(SceneUtility.GetScenePathByBuildIndex(i));
            
        } 
       


    }
    public void ChangeSceneListInitialization()
    {
        ChangeSceneUnits.Clear();
        ChangeSceneParent=GameObject.FindWithTag("ChangeSceneUnit");
        
        // 将场景切换单位添加到列表
        for (int i = 0; i < ChangeSceneParent.transform.childCount; i++)
        {
            ChangeSceneUnits.Add(ChangeSceneParent.transform.GetChild(i).gameObject);
        }

        // 生成不包含当前场景名称的随机场景索引数组
        List<int> randomIndices = GenerateRandomIndicesWithoutCurrent(sceneNames.Count,  SceneManager.GetActiveScene().path);
       
        // 如果排除后的字符串数组长度小于游戏对象数组，进行一次额外的随机组合
        while (randomIndices.Count < ChangeSceneUnits.Count)
        {
            int additionalIndex = Random.Range(0, sceneNames.Count);

            // 检查额外的索引是否等于当前场景的索引，如果不等于才添加
            if (!randomIndices.Contains(additionalIndex) && additionalIndex != randomIndices[0])
            {
                randomIndices.Add(additionalIndex);
            }
        }

        // 遍历场景切换单位列表
        for (int i = 0; i < ChangeSceneUnits.Count; i++)
        {
            // 获取当前的字符串和GameObject
            int index = randomIndices[i]; // 不再使用取余运算
            string currentString = sceneNames[index];
            Debug.Log(index);
            GameObject currentGameObject = ChangeSceneUnits[i];

            // 检查GameObject上是否有SceneChange脚本
            SceneChange yourScript = currentGameObject.GetComponent<SceneChange>();

            if (yourScript != null)
            {
                // 将字符串赋值给SceneChange脚本的LoadedScene
                yourScript.LoadedScene = currentString;
            }
            else
            {
                Debug.LogWarning("GameObject上没有SceneChange脚本：" + currentGameObject.name);
            }
        }
    }
    List<int> GenerateRandomIndicesWithoutCurrent(int count, string currentScenePath)
    {
       List<int> indices = new List<int>();
    for (int i = 0; i < count; i++)
    {
        indices.Add(i);
    }

    // 找到当前场景的索引并排除
    int currentSceneIndex = sceneNames.FindIndex(path => path == currentScenePath);
    //Debug.Log(currentSceneIndex);
    if (currentSceneIndex != -1)
    {
        indices.Remove(currentSceneIndex);
    }
    for (int i = 0; i < indices.Count; i++)
    {
        //Debug.Log(indices[i]);
    }
    // Fisher-Yates随机置乱算法
    for (int i = indices.Count - 1; i > 0; i--)
    {
        int randomIndex = Random.Range(0, i + 1);
        int temp = indices[i];
        indices[i] = indices[randomIndex];
        indices[randomIndex] = temp;
    }

    return indices;
    }
}
