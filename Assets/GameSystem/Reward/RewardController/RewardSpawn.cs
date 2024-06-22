using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSpawn : MonoBehaviour
{
    public List<GameObject> rewardSpawns=new List<GameObject>();
    public  List<RewardData_SO> rewardDatas=new List<RewardData_SO>();
    public RoomStates roomStates;
    public GameObject rewardPanel;
    public GameObject rewardPrefab;
    public GameObject rewardEnermyPrefab;
    [HideInInspector]
    public GameObject reward;
    private void Start() 
    {
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("RewardSpawn"))
        {   
           rewardSpawns.Add(obj);
        }
        //rewardSpawn=GameObject.FindWithTag("RewardSpawn");
        rewardPanel=GameObject.Find("RewardUI");
        
        roomStates=gameObject.GetComponent<RoomInitialization>().NowRoomData;
        GameEventSystem.instance.onRoomCombatFinish+=RewardShow;
        
        RewardChoose();
    }

   

    private void OnDisable()
    {
        GameEventSystem.instance.onRoomCombatFinish-=RewardShow;
        
    }
    public void RewardChoose()
    {
        if(GameManager.Instance.currentrewardPool.rewardPool.Count>=1&&roomStates.RoomData.roomState!=RoomStates_SO.RoomState.Ban)
        {
            switch(roomStates.RoomData.roomType)
        {
            case RoomStates_SO.RoomType.Basic:
                while (rewardDatas.Count<1)
                {
                 int randomIndex = Random.Range(0, GameManager.Instance.currentrewardPool.rewardPool.Count); // 生成一个随机索引
                 RewardData_SO rewardData = GameManager.Instance.currentrewardPool.rewardPool[randomIndex]; // 获取随机抽取的元素
                 if(!rewardDatas.Contains(rewardData))
                 rewardDatas.Add(rewardData); // 将元素添加到选定列表中
                 //GameManager.Instance.currentrewardPool.rewardPool.RemoveAt(randomIndex); // 从原始列表中移除已选择的元素
                }
                rewardPanel.GetComponent<RewardPanel>().RewardLoad(rewardDatas);
                
            break;
            case RoomStates_SO.RoomType.Reward:
            if(GameManager.Instance.currentrewardPool.rewardPool.Count>=3)
            {
                while (rewardDatas.Count<3)
                {
                 int randomIndex = Random.Range(0, GameManager.Instance.currentrewardPool.rewardPool.Count); // 生成一个随机索引
                 RewardData_SO rewardData = GameManager.Instance.currentrewardPool.rewardPool[randomIndex]; // 获取随机抽取的元素
                 if(!rewardDatas.Contains(rewardData))
                 rewardDatas.Add(rewardData); // 将元素添加到选定列表中
                 //GameManager.Instance.currentrewardPool.rewardPool.RemoveAt(randomIndex); // 从原始列表中移除已选择的元素
                }
                rewardPanel.GetComponent<RewardPanel>().RewardLoad(rewardDatas);

            }
                
                
            break;
             case RoomStates_SO.RoomType.Store:
            if(GameManager.Instance.currentrewardPool.rewardPool.Count>=3)
            {
                while (rewardDatas.Count<3)
                {
                 int randomIndex = Random.Range(0, GameManager.Instance.currentrewardPool.rewardPool.Count); // 生成一个随机索引
                 RewardData_SO rewardData = GameManager.Instance.currentrewardPool.rewardPool[randomIndex]; // 获取随机抽取的元素
                 if(!rewardDatas.Contains(rewardData))
                 rewardDatas.Add(rewardData); // 将元素添加到选定列表中
                 //GameManager.Instance.currentrewardPool.rewardPool.RemoveAt(randomIndex); // 从原始列表中移除已选择的元素
                }
                rewardPanel.GetComponent<RewardPanel>().StoreLoad(rewardDatas);

            }
                
                
            break;
        }
        
        }
        
       
    }
     public void RewardShow(RoomStates_SO sO)
    {
        if(roomStates.RoomData.roomState!=RoomStates_SO.RoomState.Ban)
        {
            switch (roomStates.RoomData.roomType)
        {
            case RoomStates_SO.RoomType.Basic:
            {
            Debug.Log("道具房");
            float randomValue = Random.Range(0f, 100f);

            // 10%的概率生成另外一个预制体
            GameObject prefabToInstantiate = (randomValue < 10f) ? rewardEnermyPrefab : rewardPrefab;
            if(reward==null&&rewardSpawns!=null&&rewardDatas.Count>=1)
            reward=Instantiate(prefabToInstantiate, rewardSpawns[0].transform.position, Quaternion.identity);

            break;

            }
            case RoomStates_SO.RoomType.Reward:
            {
            if(reward==null&&rewardSpawns!=null&&rewardDatas.Count>=1)
            reward=Instantiate(rewardPrefab, rewardSpawns[0].transform.position, Quaternion.identity);
            break;
            }
            case RoomStates_SO.RoomType.Store:
            {
            if(reward==null&&rewardSpawns!=null&&rewardDatas.Count>=1)
            {
                for(int i=0;i<rewardSpawns.Count;i++)
                {
                    reward=Instantiate(rewardPrefab, rewardSpawns[i].transform.position, Quaternion.identity);

                }
            }
            
            break;
            }
        }
            
        }
        
            
        
       
    }
    
   
}
