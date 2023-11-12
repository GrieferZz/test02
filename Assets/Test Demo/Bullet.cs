using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other) 
    {
      
        if (other.gameObject.CompareTag("Wall"))
        {
            // 子弹碰撞到玩家时销毁子弹对象
            Destroy(gameObject);
        }
        
    }
}
