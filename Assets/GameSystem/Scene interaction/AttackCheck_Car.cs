using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCheck_Car : MonoBehaviour
{
    public Collider collider;
    public float interval;
    public float strength;
    private float lastShootTime;
    bool cancheck;

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Enermy")||other.gameObject.CompareTag("Player"))
        {
            if(cancheck)
            {
                GetComponent<ItemsAttack>().attackInfo.strength=strength;
                GetComponent<ItemsAttack>().Hit(other.gameObject);
                cancheck=false;

            }
            

        }
    }
    private void Update() 
    {
        IntervalChcek();
    }
    private void IntervalChcek()
    {
        if (Time.time - lastShootTime >=interval)
        {
            cancheck=true;
            lastShootTime = Time.time;
        }

    }
     IEnumerator CheckFalse(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); // 等待waitTime秒

        cancheck=false;
    }
     
}
