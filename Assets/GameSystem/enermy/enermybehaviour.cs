using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class enermybehaviour : MonoBehaviour
{
    
    public Transform location;

    public NavMeshAgent agent;
    
    private Transform player;
    private bool ischasing;
    
    public bool needchase;
    
    
    
    
    private void OnTriggerStay(Collider other) 
    {
        if(other.CompareTag("Player")&&agent.enabled==true)
        {
           agent.destination=player.position;
           ischasing=true;
           Debug.Log(gameObject.name);
        }
        
    }
    private void OnCollisionEnter(Collision other) 
    {
      
        if (other.gameObject.CompareTag("Player"))
        {
           
            Debug.Log("停止"); // 子弹碰撞到玩家时销毁子弹对象
           StartCoroutine(ExecuteAfterDelay(3f));
        }
        
        
    }
    IEnumerator ExecuteAfterDelay(float delay)
    {
        agent.enabled=false;

        // 然后等待指定的时间
        yield return new WaitForSeconds(delay);

        // 再执行第二段代码
       agent.enabled=true;

    }
   
   
    
    void Start()
    {
       
        agent=gameObject.GetComponent<NavMeshAgent>();
        player=GameObject.FindWithTag("Player").transform;
        

    
        
    }
    void OnDisable()
    {
       
    }
    private void Update() 
    {
        
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(agent.enabled==true)
        {
           
        if(needchase)
        {
            
            if(ischasing==false)
        {
            return;
        }
        if(agent.remainingDistance<0.2f&&!agent.pathPending&&ischasing==false)
        {

            
            
        }

        }

        }
        
        
        
       
           
        
        
    }
   
  
}
