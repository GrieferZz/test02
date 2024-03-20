using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerRotate : MonoBehaviour
{
    public PlayerAttck playerAttck;
    public Vector3 targetDirection;
    public float rotationSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //playerAttck=GetComponent<PlayerAttck>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        
    }
    void Rotate()
    {
        targetDirection=DirectionGet();
        Vector3 horizontalTargetDirection = new Vector3(-targetDirection.x, 0f, -targetDirection.z).normalized;

        // 如果目标方向不是零向量
        if (horizontalTargetDirection != Vector3.zero)
        {
            // 计算朝向目标方向的旋转
            Quaternion targetRotation = Quaternion.LookRotation(horizontalTargetDirection);

            // 应用旋转
           transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

    }
    public Vector3 DirectionGet()
    {
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        // 获取鼠标在屏幕上的坐标
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        // 计算二维向量
        Vector2 direction2D = new Vector2(mouseScreenPos.x - playerScreenPos.x, mouseScreenPos.y - playerScreenPos.y);

        // 构建射击方向的三维向量
        Vector3 shootingDirection = new Vector3(direction2D.x, 0f, direction2D.y);
        return shootingDirection;
    }
}
