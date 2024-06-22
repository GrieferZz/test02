using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RewardPick : MonoBehaviour
{
    private bool canPick;
    public PlayerInput inputControl;
    public GameObject interactionPrefab;
    public GameObject treasure;
    private GameObject interactionPoint; 
    GameObject interaction;
    Transform cm;
  
    
    public void Awake()
    {
        inputControl=new PlayerInput();
        inputControl.GamePlay.Pick.started+=Pick;
        GameEventSystem.instance.onRewardChoose+=RewardDestroy;

    }

    

    public void OnEnable()
    {
        inputControl.Enable();      
        interactionPoint=GameObject.FindWithTag("InteractionPoint");
        cm=Camera.main.transform;
        
    
    }
    private void OnDisable() 
    {
         inputControl.GamePlay.Pick.started-=Pick;
         GameEventSystem.instance.onRewardChoose-=RewardDestroy;
         inputControl.Disable();
    }
    private void Pick(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(canPick)
        {
            if(transform.parent == null)
            {
             GameEventSystem.instance.RewardPick();
             GameEventSystem.instance.MusicPlay("pick");
            Player.Instance.NowState=Player.PlayerState.Ban;
              GameObject.FindWithTag("Player").GetComponent<PlayerAttck>().enabled=false;
            }
            
             treasure.GetComponent<Animator>().SetBool("Open",true);
             Debug.Log("拾取");
             
             

        }
       
        
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            canPick=true;
            foreach(Canvas canvas in FindObjectsOfType<Canvas>())
        {
            if(canvas.renderMode==RenderMode.WorldSpace)
            {
                interaction=Instantiate(interactionPrefab,canvas.transform);
                
                //HealthUIbar.gameObject.SetActive(false);
            }

        }
        }
    }
    void OnTriggerStay(Collider other) 
    {
         if(other.CompareTag("Player"))
        {
            StartCoroutine(InteracitonUpdate());
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            canPick=false;
            StopCoroutine(InteracitonUpdate());
            Destroy(interaction);
        }
    }
   
     IEnumerator InteracitonUpdate()
    {
        while (true)
        {
            // 在每一帧结束时暂停协程，然后在下一帧开始时继续执行
            yield return null;

            if(interaction!=null)
        {
            interaction.transform.position=interactionPoint.transform.position;
            interaction.transform.forward=cm.forward;
        }
        }
    }
    public void RewardDestroy(RewardData_SO sO)
    {
            GetComponent<BoxCollider>().enabled=false;
            Player.Instance.NowState=Player.PlayerState.Idle;
            GameObject.FindWithTag("Player").GetComponent<PlayerAttck>().enabled=true;
            Destroy(interaction);
            Destroy(gameObject,2f);

    }
    
}
