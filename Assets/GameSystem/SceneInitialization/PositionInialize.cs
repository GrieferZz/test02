using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PositionInialize : MonoBehaviour
{
    public GameObject PlayerCharacter;
    
    private GameObject ChangeSceneParent;
    public LayerMask groundLayer; // 地面的层级
    
    // Start is called before the first frame update
    public void Start()
    {
        PlayerCharacter=GameObject.FindWithTag("Player");
        PostionChoose();
        AutoPostion();
        StartCoroutine(DelayedAction(0.5f));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PostionChoose()
    {
        if(gameObject.GetComponent<Room>().IniatializationPosition!=null)
        {
            PlayerCharacter.transform.position=gameObject.GetComponent<Room>().IniatializationPosition.gameObject.transform.position;
            
            Debug.Log("位置初始化");

        }
        
    }
    public void AutoPostion()
    {
        if (Physics.Raycast(PlayerCharacter.transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            // 获取地面上的位置
            Vector3 groundPosition = hit.point;

            // 获取地面的法线向量（垂直于地面的方向）
            Vector3 groundNormal = hit.normal;

            // 设置角色位置
            PlayerCharacter.transform.position = groundPosition + groundNormal * 0.0f; // 0.1f 是为了略微抬高角色，以避免陷入地面
            //Player.Instance.NowState=Player.PlayerState.Idle;
        }
    }
    GameObject GetRandomElement(List<GameObject> list)
    {
        // 如果列表为空，返回空字符串或者你认为合适的默认值
        if (list.Count == 0)
        {
            return null;
        }

        // 生成一个随机的索引
        int randomIndex = Random.Range(0, list.Count);

        // 返回随机选择的元素
        return list[randomIndex];
    }
     IEnumerator DelayedAction(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // 等待指定的时间

        // 在这里执行你想要延迟的操作
         Player.Instance.NowState=Player.PlayerState.Idle;
    }
}

