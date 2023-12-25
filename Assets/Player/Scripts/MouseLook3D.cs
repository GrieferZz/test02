using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook3D : MonoBehaviour
{
    public float mouseSensitivity = 100f; // 鼠标灵敏度
    public Transform playerBody; // 角色的Transform组件
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 锁定光标到屏幕中心
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 防止过度旋转

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
