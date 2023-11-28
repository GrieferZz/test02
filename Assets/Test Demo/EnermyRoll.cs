using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyRoll : MonoBehaviour
{
    public float rotationSpeed = 5f;
     public Transform player; // 请将玩家对象拖拽到这个字段中
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 检测玩家是否进入范围
        if (player != null)
        {
            // 获取朝向玩家的方向
            Vector3 directionToPlayer = player.position - transform.parent.gameObject.transform.GetChild(1).position;
            directionToPlayer.y = 0f; // 避免垂直方向的旋转

            // 计算旋转角度
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

            // 平滑旋转
            transform.parent.gameObject.transform.GetChild(1).transform.rotation = Quaternion.Slerp(transform.parent.gameObject.transform.GetChild(1).transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
     void OnTriggerEnter(Collider other)
    {
        // 检测进入范围的对象是否为玩家
        if (other.CompareTag("Player"))
        {
            player = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 离开范围时将玩家设为null
        if (other.CompareTag("Player"))
        {
            player = null;
        }
    }
}
