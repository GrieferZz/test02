using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardUnitShow : MonoBehaviour
{
    public RewardData_SO rewardData;
    
    public Image rewardImage;
    public TextMeshProUGUI rewardName;
    public TextMeshProUGUI rewardEffect;
    
    public void Awake()
    {
        
    }
    public void UnitLoad(RewardData_SO rewardData)
    {
        
        if(rewardData!=null)
        {
            rewardImage.sprite=rewardData.rewardicon;
            rewardName.text=rewardData.rewardName;
            rewardEffect.text=rewardData.effectDescription;
            gameObject.GetComponent<Animation>().Play("RewardShow");

           
        }
    }
    public void UnitClose()
    {
        StartCoroutine(RewardCloseUI());
    }
     IEnumerator RewardCloseUI()
    {
        gameObject.GetComponent<Animation>().Play("RewardClose");
        while (true)
        {
           if(!gameObject.GetComponent<Animation>().IsPlaying("RewardClose"))
        {
        Destroy(gameObject);
        break;

        }

            // 等待一秒钟
            yield return null;
        }
    }
}
