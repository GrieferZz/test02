using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;


[CreateAssetMenu(fileName = "_BuffData", menuName = "BuffSystem/BuffData", order = 0)]
public class BuffData : ScriptableObject 
{
    //基本信息
    public int id;
    public string buffName;
    public string description;
    public Sprite icon;
    public int priority;
    public int maxStack;
    private string[] tags;
    //时间信息
    public bool isForever;
    public float duration;
    public float tickTime;
    //更新方式
    public BuffUpdateTimeEnum buffUpdateTime;
    public BuffRemoveStackUpdateEnum buffRemoveStackUpdate;
    //基础回调点
    
    public BaseBuffModule OnCreatePrototype;
    [HideInInspector]
    public BaseBuffModule OnCreate;
    public BaseBuffModule OnRemovePrototype;
    [HideInInspector]
    public BaseBuffModule OnRemove;
    public BaseBuffModule OnTickPrototype;
    [HideInInspector]
    public BaseBuffModule OnTick;
    //伤害回调点
    public BaseBuffModule OnHitPrototype;
    [HideInInspector]
    public BaseBuffModule OnHit;
    public BaseBuffModule OnBeHurtPrototype;
    [HideInInspector]
    public BaseBuffModule OnBeHurt;
    public BaseBuffModule OnKillPrototype;
    [HideInInspector]
    public BaseBuffModule OnKill;
    public BaseBuffModule OnBeKillPrototype;
    [HideInInspector]
    public BaseBuffModule OnBeKill;
    
    private void Awake() 
    {
        if(OnCreatePrototype!=null)
        {
            OnCreate=Instantiate(OnCreatePrototype);
            Debug.Log("Module名"+OnCreatePrototype.name);
        }
        if(OnRemovePrototype!=null)
        {
            OnRemove=Instantiate(OnRemovePrototype);
            Debug.Log("Module名"+OnRemovePrototype.name);
        }
    }
    
}

