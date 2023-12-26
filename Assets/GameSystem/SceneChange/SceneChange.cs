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
    
    // Start is called before the first frame update
    void Start()
    {
         gameObject.GetComponent<BoxCollider>().enabled=false;
         StartCoroutine(DelayedOperationCoroutine());
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
           
           
            
              switch(gameObject.tag)
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
           
            Player.Instance.NowState=Player.PlayerState.Ban;
             SceneManager.LoadScene( LoadedScene);
            
             gameObject.GetComponent<BoxCollider>().enabled=false;
          
        }
        
    }
    IEnumerator DelayedOperationCoroutine()
    {
        // 等待2秒
        yield return new WaitForSeconds(3f);

        // 在等待后执行的操作
        gameObject.GetComponent<BoxCollider>().enabled=true;

        // 在这里添加你的操作代码
    }
}
