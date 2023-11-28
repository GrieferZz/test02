using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
public class CharacterHurt : MonoBehaviour
{
    public int Health=3;
    public GameObject HealthPanel;
     public GameObject ResetPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hurt()
    {
        Health--;
       for(int i=0;i<HealthPanel.transform.childCount;i++)
       {
        if(HealthPanel.transform.GetChild(i).gameObject.active)
        {
            HealthPanel.transform.GetChild(i).gameObject.SetActive(false);
            break;
        }

       }
       if(Health<=0)
       {
         ResetPanel.SetActive(true);

       }
    }
    private void OnCollisionEnter(Collision other) 
    {
      
        if (other.gameObject.CompareTag("bullet"))
        {
            // 子弹碰撞到玩家时销毁子弹对象
            Destroy(other.gameObject);
            Hurt();
        }
         if (other.gameObject.CompareTag("Enermy"))
        {
            // 子弹碰撞到玩家时销毁子弹对象
            Debug.Log("碰撞");
            Hurt();
        }
        
    }
   private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Laser"))
        {
           Hurt();
        }
        
    }
    
    
}
