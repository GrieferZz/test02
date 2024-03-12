using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBomb : MonoBehaviour
{
    
    public float explosionRange;
    
    private GameObject explosion;
    public List<GameObject>Enermies=new List<GameObject>();
    private Bullet bullet;
    private Rigidbody rb;
    private void OnEnable() 
    {
       GameEventSystem.instance.onTargetTransmission+=TargetGet; 
       GameEventSystem.instance.onDetonation+=StickyBulletExplode; 
    }
    private void OnDisable() 
    {
        GameEventSystem.instance.onTargetTransmission-=TargetGet; 
        GameEventSystem.instance.onDetonation-=StickyBulletExplode; 
    }
    // Start is called before the first frame update
    void Start()
    {
        bullet=GetComponent<Bullet>();
        
        //rb = GetComponent<Rigidbody>();
        foreach(Transform child in transform)
        {
             if(child.gameObject.CompareTag("ExplosionRange"))//TagSomthing换成要比较的Tag
                 explosion=child.gameObject;
                 child.gameObject.SetActive(false);
        }
        explosion.SetActive(false);
        //gameObject.GetComponent<SphereCollider>().enabled=false;
        Physics.IgnoreCollision(gameObject.GetComponent<SphereCollider>(), GameObject.FindWithTag("Player").GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StickyBulletExplode()
    {
        
        explosion.SetActive(true);
        //rb.isKinematic=true;
    }
    void TargetGet(GameObject parent,GameObject target)
    {
        if(parent==gameObject)
        {
            if(!Enermies.Contains(target))
        {
            Enermies.Add(target);
            bullet.StickyBulletHit(target);
            Debug.Log("爆炸发生"+target);
        }

        }
         
    }
}
