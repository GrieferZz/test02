using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using Unity.VisualScripting;
public class StoreStates : MonoBehaviour
{
    public RewardData_SO rewardDataTemplete;
    [HideInInspector]
    public RewardData_SO rewardData;
    
    public Image rewardImage;
    public TextMeshProUGUI rewardName;
    public TextMeshProUGUI rewardEffect;
    public TextMeshProUGUI rewardCoin;
    public CharacterStates characterStates;
    
    public GameObject Player;
    void Start() 
    {
        characterStates=GameObject.FindWithTag("Player").GetComponent<CharacterStates>();
    }
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
            rewardCoin.text=rewardData.rewardCoin.ToString();
            
        }
    }
    public void RewardChoose()
    {
        if(characterStates.itemsData.coin>=rewardData.rewardCoin)
        {
            GameManager.Instance.RewardPoolUpdate(rewardData);
            GameEventSystem.instance.RewardChoose(rewardData);
            GameEventSystem.instance.MusicPlay("choosereward");

        }
        
        
    }
}
