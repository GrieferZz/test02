using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
public class GameEventSystem :Singleton<GameEventSystem>
{
    public static GameEventSystem instance;
    public enum Direction{Up,Down,Left,Right};
    private void Awake() 
    {
        if(instance==null)
        {
            instance =this;
        }
        else
        {
            if(instance!=this)
            {
                Destroy(gameObject);
            }
        }
        //DontDestroyOnLoad(gameObject);
        
    }
    // Start is called before the first frame update
    




     public event Action onSceneLoad;
     public event Action<Direction> onScenechange;












      public void SceneLoad()
    {
        if(onSceneLoad!=null)
        onSceneLoad();
        //print("DialogueFinish事件触发");
    }
    public void SceneChange(Direction direction)
    {
        if(onScenechange!=null)
        onScenechange(direction);
        //print("DialogueFinish事件触发");
    }
}
