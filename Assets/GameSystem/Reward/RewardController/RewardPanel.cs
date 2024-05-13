using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class RewardPanel : MonoBehaviour
{
    public GameObject rewardChooseUnit;
    public GameObject rewardGroup;
    public GameObject rewardPanel;
   public  List<RewardData_SO> rewardDatas=new List<RewardData_SO>();

    private void Awake() 
    {
        for (int i = rewardGroup.transform.childCount - 1; i >= 0; i--)
        {
            // 获取子物体
            Transform child = rewardGroup.transform.GetChild(i);
            
            // 销毁子物体
            Destroy(child.gameObject);
        }
        GameEventSystem.instance.onRewardPick+=RewardPick;
        GameEventSystem.instance.onRewardChoose+=RewardChoose;
        rewardPanel.GetComponent<UnityEngine.UI.Image>().enabled=false;
    }

    

    public void RewardPick()
    {
        rewardPanel.GetComponent<UnityEngine.UI.Image>().enabled=true;
        rewardGroup.SetActive(true);
        rewardPanel.GetComponent<Animation>().Play("RewardPanelOpen");
    }
    public void RewardLoad(List<RewardData_SO> rewardData_SOs)
    {
       
         rewardDatas.Clear();
        
        for(int i=0;i<rewardData_SOs.Count;i++)
        {
            rewardDatas.Add(rewardData_SOs[i]);
        }
        for(int i=0;i<rewardDatas.Count;i++)
        {
           GameObject rewardChoose= Instantiate(rewardChooseUnit);
           rewardChoose.transform.SetParent(rewardGroup.transform);
           rewardChoose.GetComponent<RewardStates>().rewardDataTemplete=rewardDatas[i];
        }
        RewardPanelUIUpdate();
        rewardGroup.SetActive(false);
        
    }
    private void RewardChoose(RewardData_SO sO)
    {
        
        StartCoroutine(RewardPanelCloseUI());

    }
    IEnumerator RewardPanelCloseUI()
    {
        rewardPanel.GetComponent<Animation>().Play("RewardPanelClose");
        while (true)
        {
           if(!rewardPanel.GetComponent<Animation>().IsPlaying("RewardPanelClose"))
        {
        for (int i = rewardGroup.transform.childCount - 1; i >= 0; i--)
        {
            // 获取子物体
            Transform child = rewardGroup.transform.GetChild(i);
            
            // 销毁子物体
            Destroy(child.gameObject);
        }
        
        rewardPanel.GetComponent<UnityEngine.UI.Image>().enabled=false;
        break;

        }

            // 等待一秒钟
            yield return null;
        }
    }
    public void RewardPanelUIUpdate()
    {
        rewardPanel.GetComponent<RectTransform>().sizeDelta=new Vector2(200+300*rewardDatas.Count, gameObject.GetComponent<RectTransform>().sizeDelta.y);
    }
}
