using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBuff : MonoBehaviour
{
    //public int maxStickyAmount;
    public float explosionRange;
    public BuffInfo sticky;
    public BuffData buffData;
    public BuffData currentbuffData;
    private GameObject explosion;
    public List<GameObject>Enermies=new List<GameObject>();
    private Bullet bullet;
    private Rigidbody rb;
    private void OnEnable() 
    {
       //GameEventSystem.instance.onTargetTransmission+=TargetGet; 
       //GameEventSystem.instance.onDetonation+=StickyBulletExplode; 
    }
    private void OnDisable() 
    {
       // GameEventSystem.instance.onTargetTransmission-=TargetGet; 
       // GameEventSystem.instance.onDetonation-=StickyBulletExplode; 
    }
    // Start is called before the first frame update
    void Start()
    {
        sticky=new BuffInfo();
        bullet=GetComponent<Bullet>();
        rb = GetComponent<Rigidbody>();
        foreach(Transform child in transform)
        {
             if(child.gameObject.CompareTag("ExplosionRange"))//TagSomthing换成要比较的Tag
                 explosion=child.gameObject;
        }
        explosion.SetActive(false);
        //gameObject.GetComponent<SphereCollider>().enabled=false;
        Physics.IgnoreCollision(gameObject.GetComponent<SphereCollider>(), GameObject.FindWithTag("Player").GetComponent<Collider>());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other) 
    {
        if(!other.gameObject.CompareTag("Player"))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            foreach(Transform child in transform)
        {
             child.gameObject.SetActive(false);
        }
                // 设置子弹的父对象为碰撞到的物体，使其黏在上面
            transform.parent = other.transform;
        if(buffData!=null)
        {
            currentbuffData=Instantiate(buffData);
            sticky.buffData=currentbuffData;

        }
        
        sticky.creator=bullet.InitiatorStates.gameObject;
        sticky.target=other.gameObject;   
        sticky.target.GetComponent<BuffHandler>().AddBuff(sticky);
        }
        
    }
    public void StickyBulletExplode()
    {
        explosion.SetActive(true);
        rb.isKinematic=true;
    }
    void TargetGet(GameObject parent,GameObject target)
    {
        if(parent==gameObject)
        {
            if(!Enermies.Contains(target))
        {
            Enermies.Add(target);
            bullet.StickyBulletHit(target);
        }

        }
         
    }
}
