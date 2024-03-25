using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockFace : MonoBehaviour
{
    //public Vector3 attackDirection;
    // Start is called before the first frame update
    public Material originalFace;
    public Material knockFace;
    private SkinnedMeshRenderer skinnedMeshRender;
    public float delayTime = 1;
    private float timer = 0;
    public bool executed = false;
    public GameObject Player;
    void Start()
    {
        skinnedMeshRender = this.GetComponent<SkinnedMeshRenderer>();
    }
    void Awake()
    {
        if (AttackManager.instance != null)
        {
            AttackManager.instance.onAttackEvent += OnAttackEvent;
        }
        else
        {
            Debug.LogError("AttackManager instance is not set!");
        }
    }
    // Update is called once per frame
    private void OnAttackEvent(GameObject creator, GameObject target, Bullet bullet)
    {
        // 检查target是否等于当前游戏对象    
        if (target == Player)
        {
            skinnedMeshRender.material = knockFace;
            executed = true; // 立即设置executed为true  
            // 开始计时，准备在delayTime后将材质改回originalFace  
            StartCoroutine(ResetMaterialAfterDelay());
        }
    }

    void Update()
    {
       
    }

    IEnumerator ResetMaterialAfterDelay()
    {
        // 等待delayTime秒  
        yield return new WaitForSeconds(delayTime);

        // 延迟时间结束后，将材质改回originalFace  
        if (executed) // 确保executed仍然是true，以防在延迟期间有其他逻辑改变了它  
        {
            skinnedMeshRender.material = originalFace;
        }
    }
}


