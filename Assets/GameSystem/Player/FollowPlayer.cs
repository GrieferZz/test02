using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset ;
    public float smoothSpeed = 0.125f;
    private void Start() 
    {
        playerTransform=GameObject.FindWithTag("Player").transform;
        offset=gameObject.transform.position;
    }
    void LateUpdate()
    {
        // 设置摄像机的目标位置
        Vector3 desiredPosition = playerTransform.position + offset;

        // 使用 SmoothDamp 函数平滑地移动摄像机
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // 让摄像机一直朝向角色
        //transform.LookAt(playerTransform);
    }
}
