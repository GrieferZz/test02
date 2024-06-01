using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
public class GameEventSystem :Singleton<GameEventSystem>
{
    public static GameEventSystem instance;
    public enum Direction{Up,Down,Left,Right};
    protected override void Awake()
    {
        base.Awake();
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
     public event Action onDetonation;
     public event Action<Direction> onScenechange;
     public event Action<GameObject> onHealthBarUpdate;
     public event Action onBuffUIUpdate;
     public event Action<GameObject,GameObject> onTargetTransmission;
     public event Action onWaveClear;
     public event Action onEnermyClear;
     public event Action onRewardPick;
      public event Action<RewardData_SO> onRewardChoose;
     public event Action<RoomStates_SO> onRoomCombatFinish;
      public event Action<string> onMusicPlay;
     public event Action onSecondUpdate;










      public void SceneLoad()
    {
        if(onSceneLoad!=null)
        onSceneLoad();
        print("房间加载");
    }
    public void Detonation()
    {
        if(onDetonation!=null)
        onDetonation();
        //print("DialogueFinish事件触发");
    }
    public void SecondUpdate()
    {
        if(onSecondUpdate!=null)
        onSecondUpdate();
        //print("DialogueFinish事件触发");
    }
    public void SceneChange(Direction direction)
    {
        if(onScenechange!=null)
        onScenechange(direction);
        //print("DialogueFinish事件触发");
    }
    public void HealthBarUpdate(GameObject target)
    {
        onHealthBarUpdate?.Invoke(target);
        //print("DialogueFinish事件触发");
    }
     public void BuffUIUpdate()
    {
        if(onBuffUIUpdate!=null)
        onBuffUIUpdate();
        print("房间加载");
    }
    public void TargetTransmission(GameObject parent, GameObject target)
    {
        onTargetTransmission?.Invoke( parent ,target);
        //print("DialogueFinish事件触发");
    }
    public void WaveClear()
    {
       if(onWaveClear!=null)
        onWaveClear();
        //print("DialogueFinish事件触发");
    }
    public void EnermyClear()
    {
       if(onEnermyClear!=null)
        onEnermyClear();
        //print("DialogueFinish事件触发");
    }
      public void RewardPick()
    {
        if(onRewardPick!=null)
        onRewardPick();
       
    }
     public void RewardChoose(RewardData_SO rewardData_SO)
    {
        if(onRewardChoose!=null)
        onRewardChoose( rewardData_SO);
       
    }
     public void RoomCombatFinish(RoomStates_SO roomstate)
    {
       if(onRoomCombatFinish!=null)
        onRoomCombatFinish(roomstate);
        //print("战斗");
    }
     public void MusicPlay(string musicname )
    {
       if(onMusicPlay!=null)
        onMusicPlay(musicname);
        //print("战斗");
    }
}
