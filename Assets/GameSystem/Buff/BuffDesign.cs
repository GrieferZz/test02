using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public enum BuffUpdateTimeEnum
{
    Add,
    Replace,
    Keep
}
public enum BuffRemoveStackUpdateEnum
{
    Clear,
    Reduce
}
public class BuffInfo
{
    public BuffData buffData;
    public GameObject creator;
    public GameObject self;
    public GameObject target;
    public float durationTimer;
    public float tickTimer;
    public int curStack=1;
    /*public BuffInfo(BuffData buffData,GameObject creator,GameObject target,float durationTimer,float tickTimer,int curStack)
    {
        this.buffData=buffData;
        this.creator=creator;
        this.target=target;
        this.durationTimer=durationTimer;
        this.tickTimer=tickTimer;
        this.curStack=curStack;
    }*/
}
public class DamageInfo
{
    public GameObject creator;
    public GameObject target;
    public float damage;
}