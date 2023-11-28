using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyRotate : MonoBehaviour
{
    public float rotationSpeed = 30f;

    void Update()
    {
        // 沿着物体的上方轴旋转
        transform.parent.gameObject.transform.GetChild(1).transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
