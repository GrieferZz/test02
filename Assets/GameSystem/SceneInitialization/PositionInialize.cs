using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionInialize : MonoBehaviour
{
    private GameObject Player;
    public LayerMask groundLayer; // 地面的层级
    // Start is called before the first frame update
    void Start()
    {
        Player=GameObject.Find("Player");
        Player.transform.position=this.transform.position;
        AutoPostion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AutoPostion()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            // 获取地面上的位置
            Vector3 groundPosition = hit.point;

            // 获取地面的法线向量（垂直于地面的方向）
            Vector3 groundNormal = hit.normal;

            // 设置角色位置
            Player.transform.position = groundPosition + groundNormal * 0.1f; // 0.1f 是为了略微抬高角色，以避免陷入地面
        }
    }
}

