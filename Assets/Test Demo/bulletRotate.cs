using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletRotate : MonoBehaviour
{
    public float rotationSpeed = 50f;

    void Update()
    {
        // 生成随机的旋转轴
        Vector3 randomAxis = new Vector3(Random.value, Random.value, Random.value);

        // 使用随机轴和速度进行旋转
        transform.Rotate(randomAxis * rotationSpeed * Time.deltaTime);
    }
}
