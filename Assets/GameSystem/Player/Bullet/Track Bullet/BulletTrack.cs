using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrack : MonoBehaviour
{
    public Transform Target; // 目标的Transform组件
    
    public float rotateSpeed; // 子弹旋转的速度
    // Start is called before the first frame update
    public float  trackRange;
    private Rigidbody rb;
    private bool isTrack;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DelayedTrack(0.2f));
       
    }
    private void OnDrawGizmos() 
    
    {
        Gizmos.color=Color.blue;
        Gizmos.DrawWireSphere(transform.position,trackRange);

    }
   
    // Update is called once per frame
    void Update()
    {
        if(GetComponent<WeaponStates>().weaponStates!=null)
        {
            rotateSpeed=GetComponent<WeaponStates>().weaponStates.steerSpeed;
            trackRange=GetComponent<WeaponStates>().weaponStates.trackRadius;

        }
    }
    private void FixedUpdate() 
    {
        if(GetComponent<WeaponStates>().weaponStates!=null&&isTrack)
        {
             TargetSelect();
             TrackMove();

        }
       
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
     IEnumerator DelayedTrack(float delaySeconds)
    {
        // 等待一定的时间后执行下一步操作
        yield return new WaitForSeconds(delaySeconds);

        // 在延迟之后执行的操作
        isTrack=true;
    }
}
