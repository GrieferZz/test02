using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackEffect : MonoBehaviour
{
    public Vector3 attackDirection;
    public float knockbackForce = 100f; // ���Ը��ݽ�ɫ���Ի򹥻�ǿ�ȵ���  
    private Rigidbody rb;
    public GameObject Player;
    void Awake()
    {
        if (AttackManager.instance != null)
        {
            AttackManager.instance.onHurtEvent += OnAttackEvent;
        }
        else
        {
            Debug.LogError("AttackManager instance is not set!");
        }
        // ��ȡRigidbody���    
        rb =Player. GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is not found on this GameObject!");
        }
    }
    private void OnAttackEvent(GameObject creator, GameObject target, AttackInfo attackInfo)
    {
        // ���target�Ƿ���ڵ�ǰ��Ϸ����    
        if (target == Player)
        {
            GameEventSystem.instance.MusicPlay("playerhurt");
            // ���㹥������    
            attackDirection = (creator.transform.position - Player.transform.position).normalized;
            // Ӧ�û���Ч��    
            ApplyKnockback(attackInfo.strength);
        }
    }
    private void ApplyKnockback(float strength)
    {
        // ������˷��򣨹�������ķ�����      
        Vector3 knockbackDirection = -attackDirection;
        // Ӧ�û���Ч������ɫ�ĸ�����      
        rb.AddForce(knockbackDirection * knockbackForce*strength, ForceMode.Impulse);
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
        // ������˷��򣨹�������ķ�����  
        Vector3 knockbackDirection = -attackDirection;

        // �趨���˾��������  
        //float knockbackDistance = 5f; // ���Ը��ݹ���ǿ�ȵ���  
        float knockbackForce = 100f; // ���Ը��ݽ�ɫ���Ի򹥻�ǿ�ȵ���  

        // Ӧ�û���Ч������ɫ�ĸ�����  
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
    }*/
}


