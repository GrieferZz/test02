using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackEffect : MonoBehaviour
{
    public Vector3 attackDirection;
    public float knockbackForce = 100f; // 可以根据角色属性或攻击强度调整  
    private Rigidbody rb;
    public GameObject Player;
    void Awake()
    {
        if (AttackManager.instance != null)
        {
            AttackManager.instance.onAttackEvent += OnAttackEvent;
            Debug.Log("666666666666666666");
        }
        else
        {
            Debug.LogError("AttackManager instance is not set!");
            Debug.Log("7777777777777");
        }
        // 获取Rigidbody组件    
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is not found on this GameObject!");
        }
    }
    private void OnAttackEvent(GameObject creator, GameObject target, Bullet bullet)
    {
        // 检查target是否等于当前游戏对象    
        if (target == Player)
        {
            // 计算攻击方向    
            attackDirection = (creator.transform.position - Player.transform.position).normalized;
            // 应用击退效果    
            ApplyKnockback();
        }
    }
    private void ApplyKnockback()
    {
        // 计算击退方向（攻击方向的反方向）      
        Vector3 knockbackDirection = -attackDirection;
        // 应用击退效果到角色的刚体上      
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
   /* void OnAttackDetected(Vector3 attackDirection)
    {
        // 计算击退方向（攻击方向的反方向）  
        Vector3 knockbackDirection = -attackDirection;

        // 设定击退距离和力度  
        //float knockbackDistance = 5f; // 可以根据攻击强度调整  
        float knockbackForce = 100f; // 可以根据角色属性或攻击强度调整  

        // 应用击退效果到角色的刚体上  
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
    }*/
}


