using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSpawn : MonoBehaviour
{
    public GameObject rewardSpawn;
    public  List<RewardData_SO> rewardDatas=new List<RewardData_SO>();
    public RoomStates roomStates;
    public GameObject rewardPanel;
    public GameObject rewardPrefab;
    [HideInInspector]
    public GameObject reward;
    private void Start() 
    {
        rewardSpawn=GameObject.FindWithTag("RewardSpawn");
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
        if(GameManager.Instance.currentrewardPool.rewardPool.Count>=1)
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
                
            break;
        }
        rewardPanel.GetComponent<RewardPanel>().RewardLoad(rewardDatas);

        }
        
       
    }
     public void RewardShow(RoomStates_SO sO)
    {
        if(reward==null&&rewardSpawn!=null&&rewardDatas.Count>=1)
        reward=Instantiate(rewardPrefab, rewardSpawn.transform.position, Quaternion.identity);

    }
    
   
}
