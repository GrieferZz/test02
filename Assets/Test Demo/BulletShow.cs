using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShow : MonoBehaviour
{
    void Start()
    {
        // 获取物体的所有子物体数量
        int childCount = transform.childCount;

        // 在开始时随机选择一个子物体的索引
        int randomChildIndex = Random.Range(0, childCount);

        // 循环遍历所有子物体
        for (int i = 0; i < childCount; i++)
        {
            // 获取子物体
            Transform child = transform.GetChild(i);

            // 判断是否为随机选择的子物体索引
            if (i == randomChildIndex)
            {
                // 如果是随机选择的子物体，激活它
                child.gameObject.SetActive(true);
            }
            else
            {
                // 如果不是随机选择的子物体，禁用它
                child.gameObject.SetActive(false);
            }
        }
    }
}
