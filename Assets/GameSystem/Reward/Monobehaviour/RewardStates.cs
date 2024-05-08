using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using Unity.VisualScripting;
public class RewardStates : MonoBehaviour
{
    public RewardData_SO rewardDataTemplete;
    [HideInInspector]
    public RewardData_SO rewardData;
    
    public Image rewardImage;
    public TextMeshProUGUI rewardName;
    public TextMeshProUGUI rewardEffect;
    public BuffInfo buffInfo;
    public GameObject Player;
    
    private void Update()
    {
        if(rewardDataTemplete!=null&&rewardData==null)
        {
            rewardData=Instantiate(rewardDataTemplete);
            RewardLoad();
            Debug.Log("道具");
        }
       
    }
    public void RewardLoad()
    {
        if(rewardData!=null)
        {
            rewardImage.sprite=rewardData.rewardicon;
            rewardName.text=rewardData.rewardName;
            rewardEffect.text=rewardData.effectDescription;
            Player=GameObject.FindWithTag("Player");
            buffInfo=new BuffInfo();
            buffInfo.creator=gameObject;
            buffInfo.buffData=Instantiate(rewardData.rewardExert);
        }
    }
    public void RewardChoose()
    {
        Debug.Log("选择"+buffInfo.buffData.name);
       
        Player.GetComponent<BuffHandler>()?.AddBuff(buffInfo);
   
        
    }
}
