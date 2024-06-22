using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CoinUI : MonoBehaviour
{
   public CharacterStates characterStates;
   public TextMeshProUGUI coin;
   private void Update() 
   {
       coin.text=characterStates.itemsData.coin.ToString();
   }
}
