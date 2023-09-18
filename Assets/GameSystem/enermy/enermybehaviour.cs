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
        if(other.gameObject.name=="Player"&&agent.enabled==true)
        {
           agent.destination=player.position;
           ischasing=true;
           Debug.Log(gameObject.name);
        }
        
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
