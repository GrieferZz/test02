using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviorManager : MonoBehaviour
{
    public bool beAttacked;
    public Vector3 attackedDirection;
    void Start()
    {
        
    }
    void OnEnable()
    {
        AttackManager.instance.onAttackEvent+=AttackDirectionGet;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AttackDirectionGet(GameObject creator,GameObject target,Bullet bullet)
    {
       
    }
}
