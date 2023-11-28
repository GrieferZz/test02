using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyJump : MonoBehaviour
{
    public float jumpHeight = 1f;
    public float jumpSpeed = 2f;

    private float timeElapsed = 0f;

    void Update()
    {
        // 使用Sin函数创建一个上下运动的曲线
        float yOffset = Mathf.Sin(timeElapsed * jumpSpeed) * jumpHeight;

        // 更新物体的transform
        transform.position = new Vector3(transform.position.x, yOffset+2.5f, transform.position.z);

        // 增加时间以改变Sin函数的输入，产生动画效果
        timeElapsed += Time.deltaTime;
    }
}
