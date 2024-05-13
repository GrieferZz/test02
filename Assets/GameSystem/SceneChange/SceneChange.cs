using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public string LoadedScene;
    private bool isopen;
    
    
    // Start is called before the first frame update
    void Start()
    {
       
        
         
         //StartCoroutine(DelayedOperationCoroutine());
         
    }
    void OnEnable() 
    {
        GameEventSystem.instance.onRoomCombatFinish+=DoorOpen;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
    void OnDisable() 
    {
        GameEventSystem.instance.onRoomCombatFinish-=DoorOpen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Player.Instance.NowState = Player.PlayerState.Ban;
            switch (gameObject.tag)
            {
                case "Up":
                     GameEventSystem.instance.SceneChange(GameEventSystem.Direction.Up);
                    break;
                case "Down":
                     GameEventSystem.instance.SceneChange(GameEventSystem.Direction.Down);
                    break;
                case "Left":
                     GameEventSystem.instance.SceneChange(GameEventSystem.Direction.Left);
                    break;
                case "Right":
                     GameEventSystem.instance.SceneChange(GameEventSystem.Direction.Right);
                    break;


            }
           
            
             SceneManager.LoadScene( LoadedScene);
             GameEventSystem.instance.MusicPlay("newroom");
             //GameEventSystem.instance.SceneLoad()
             Debug.Log("传送");
            
            
          
        }
        
    }
    void DoorOpen(RoomStates_SO enermyState)
    {
        
        
        isopen=true;
        if(isopen)
        {
            StartCoroutine(DelayedOperationCoroutine(3f));
        }
    }
    IEnumerator DelayedOperationCoroutine(float time)
    {
        // 等待2秒
        yield return new WaitForSeconds(time);
        
        // 在等待后执行的操作
        gameObject.GetComponent<BoxCollider>().enabled=true;

        // 在这里添加你的操作代码
    }
}
