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
            Debug.Log ("ʶ�����ϴ�");
            //skinnedMeshRender.material = knockFace;
        }
    }


    // ��������һ����������⹥�������ҵ���������ʱ�������OnAttackDetected����  
    void OnAttackDetected(Vector3 attackDirection)
    {
        // ������˷��򣨹�������ķ�����  
        Vector3 knockbackDirection = -attackDirection;

        // �趨���˾��������  
        //float knockbackDistance = 5f; // ���Ը��ݹ���ǿ�ȵ���  
        float knockbackForce = 100f; // ���Ը��ݽ�ɫ���Ի򹥻�ǿ�ȵ���  

        // Ӧ�û���Ч������ɫ�ĸ�����  
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        Debug.Log("����������");

        // ����ֱ�������ٶȣ�ȡ��������Ҫ��Ч����  
        // rb.velocity = knockbackDirection * knockbackDistance;  

        // �������˶���������Ч��  
        //Animator animator = GetComponent<Animator>();
        //animator.SetTrigger("Knockback");


    }
}


