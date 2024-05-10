using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class RewardBag : MonoBehaviour
{
    public GameObject bagOpen;
    public GameObject bagClose;
    public GameObject bagGroup;
    public GameObject rewardShowUnit;
    [HideInInspector]
    public GameObject rewardShow;
    public List<RewardData_SO> rewardBag=new List<RewardData_SO>();
    public BuffInfo buffInfo;
    [HideInInspector]
    public GameObject Player;
    private void Awake() 
    {
         Player=GameObject.FindWithTag("Player");
        GameEventSystem.instance.onRewardChoose+=RewardAdd;
        bagOpen.SetActive(true);
        bagClose.SetActive(false);
        for (int i = bagGroup.transform.childCount - 1; i >= 0; i--)
        {
            // 获取子物体
            Transform child = bagGroup.transform.GetChild(i);
            
            // 销毁子物体
            Destroy(child.gameObject);
        }
        bagGroup.SetActive(false);
    }
    public void RewardAdd(RewardData_SO rewardData_SO)
    {
        if(!rewardBag.Contains(rewardData_SO))
        {
            rewardBag.Add(rewardData_SO);
            buffInfo=new BuffInfo();
            buffInfo.creator=gameObject;
            buffInfo.buffData=Instantiate(rewardData_SO.rewardExert);
            Player.GetComponent<BuffHandler>()?.AddBuff(buffInfo);

        }
        

    }
    public void RewardBagShow()
    {
        bagGroup.SetActive(true);
        bagOpen.SetActive(false);
        bagClose.SetActive(true);
        for (int i = bagGroup.transform.childCount - 1; i >= 0; i--)
        {
            // 获取子物体
            Transform child = bagGroup.transform.GetChild(i);
            
            // 销毁子物体
            Destroy(child.gameObject);
        }
         for(int i=0;i<rewardBag.Count;i++)
         {
            rewardShow=Instantiate(rewardShowUnit);
            rewardShow.transform.SetParent(bagGroup.transform);
            rewardShow.GetComponent<RewardUnitShow>().UnitLoad(rewardBag[i]);
         }


    }
    public void RewardBagClose()
    {
        bagGroup.SetActive(false);
        bagOpen.SetActive(true);
        bagClose.SetActive(false);
    }
     
}
