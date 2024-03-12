using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrack : MonoBehaviour
{
    public Transform Target; // 目标的Transform组件
    
    public float rotateSpeed = 200f; // 子弹旋转的速度
    // Start is called before the first frame update
    public float  trackRange;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
    }
    private void OnDrawGizmos() 
    
    {
        Gizmos.color=Color.blue;
        Gizmos.DrawWireSphere(transform.position,trackRange);

    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate() 
    {
        TargetSelect();
        TrackMove();
    }
    void TargetSelect()
    {
        var colliders=Physics.OverlapSphere(transform.position,trackRange);
        foreach(var target in colliders)
        {
            if(target.CompareTag("Enermy"))
            {
                
                Target=target.gameObject.transform;
                
            }
        }
        
    }
    void TrackMove()
    {
        if(Target!=null)
        {
        Vector3 direction = (Target.position - transform.position).normalized;

        // 使用角度差来旋转子弹朝向目标
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, lookRotation, rotateSpeed * Time.fixedDeltaTime));

        // 让子弹向目标移动
        rb.velocity = transform.forward * Mathf.Abs(rb.velocity.magnitude);
        }
       if(Target=null)
       {
        Destroy(gameObject);
       }
    }
}
