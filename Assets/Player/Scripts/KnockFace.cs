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
        // ���target�Ƿ���ڵ�ǰ��Ϸ����    
        if (target == Player)
        {
            skinnedMeshRender.material = knockFace;
            executed = true; // ��������executedΪtrue  
            // ��ʼ��ʱ��׼����delayTime�󽫲��ʸĻ�originalFace  
            StartCoroutine(ResetMaterialAfterDelay());
        }
    }

    void Update()
    {
       
    }

    IEnumerator ResetMaterialAfterDelay()
    {
        // �ȴ�delayTime��  
        yield return new WaitForSeconds(delayTime);

        // �ӳ�ʱ������󣬽����ʸĻ�originalFace  
        if (executed) // ȷ��executed��Ȼ��true���Է����ӳ��ڼ��������߼��ı�����  
        {
            skinnedMeshRender.material = originalFace;
        }
    }
}


