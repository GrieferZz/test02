using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAnimator : MonoBehaviour
{
    private Transform m_Transform;
    public bool characterMoving = false;//判断是否在移动
    private Vector3 previousPosition;//最初位置
    private Animator _animator;
    private bool isWalkForwardBackward = false;
    private bool isLeft = false;
    private bool isRight = false;
    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;

        m_Transform = this.transform;

        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        actAnimator();

        //判断角色是否在移动
        if (transform.position != previousPosition)
        {
            characterMoving = true;
            previousPosition = transform.position; // 更新前一帧的位置
        }
        else
        {
            characterMoving = false;
        }

        
        // 输出移动状态
        Debug.Log("Is Moving: " + characterMoving);
    }

    void moveAnimator()
    {
        if (characterMoving == true)
        {
            _animator.SetBool("isMoving", true);
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }
    }
    void actAnimator()
    {
        // 重置动画状态  
        isWalkForwardBackward = false;
        isLeft = false;
        isRight = false;

        // 检查按键状态  
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            isWalkForwardBackward = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            isLeft = true;
            isWalkForwardBackward = false; // 避免同时触发前后移动和左右移动  
        }
        if (Input.GetKey(KeyCode.D))
        {
            isRight = true;
            isWalkForwardBackward = false; // 避免同时触发前后移动和左右移动  
        }

        // 设置动画参数  
        _animator.SetBool("isWalkForwardBackward", isWalkForwardBackward && characterMoving);
        _animator.SetBool("isLeft", isLeft && characterMoving);
        _animator.SetBool("isRight", isRight && characterMoving);
    }
}
