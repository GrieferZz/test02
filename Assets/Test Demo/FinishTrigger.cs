using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public GameObject ResetPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   private void OnTriggerEnter(Collider other) 
    
   
    {
      
        if (other.CompareTag("Player"))
        {
            // 子弹碰撞到玩家时销毁子弹对象
            
            ResetPanel.SetActive(true);
        }
        
    }
}
