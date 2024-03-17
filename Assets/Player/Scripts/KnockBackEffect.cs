using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackEffect : MonoBehaviour
{
    public Vector3 attackDirection;
    // Start is called before the first frame update
    //public Material knockFace;
    //private SkinnedMeshRenderer skinnedMeshRender;
    void Start()
    {
        //skinnedMeshRender = this.GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            attackDirection = new Vector3(2.0f, 0.0f, 0.0f);
            OnAttackDetected(attackDirection);
            Debug.Log ("识别到了老大");
            //skinnedMeshRender.material = knockFace;
        }
    }


    // 假设你有一个方法来检测攻击，并且当攻击发生时调用这个OnAttackDetected方法  
    void OnAttackDetected(Vector3 attackDirection)
    {
        // 计算击退方向（攻击方向的反方向）  
        Vector3 knockbackDirection = -attackDirection;

        // 设定击退距离和力度  
        //float knockbackDistance = 5f; // 可以根据攻击强度调整  
        float knockbackForce = 100f; // 可以根据角色属性或攻击强度调整  

        // 应用击退效果到角色的刚体上  
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        Debug.Log("看看动了吗");

        // 或者直接设置速度（取决于你想要的效果）  
        // rb.velocity = knockbackDirection * knockbackDistance;  

        // 触发击退动画和粒子效果  
        //Animator animator = GetComponent<Animator>();
        //animator.SetTrigger("Knockback");


    }
}


