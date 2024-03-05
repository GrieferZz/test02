using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAct : MonoBehaviour
{
    public float speed = 5;
    private float slownumber = 1.14f;//控制八向移动的速度基本相同
    private Transform m_Transform;
    public bool characterMoving = false;//判断是否在移动
    private Vector3 previousPosition;//最初位置
    private Animator _animator;

    void Start()
    {
        previousPosition = transform.position;

        m_Transform = this.transform;

        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        playerMove();

        moveAnimator();

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

    void playerMove()

    {


        //检测四个斜向的按键
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            //m_Transform.localRotation = Quaternion.Euler(0, -45, 0);//旋转的四元数
            m_Transform.Translate(new Vector3(-1, 0, 1) / slownumber * speed * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            //m_Transform.localRotation = Quaternion.Euler(0, 45, 0);
            m_Transform.Translate(new Vector3(1, 0, 1) / slownumber * speed * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            //m_Transform.localRotation = Quaternion.Euler(0, -135, 0);
            m_Transform.Translate(new Vector3(-1, 0, -1) / slownumber * speed * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            //m_Transform.localRotation = Quaternion.Euler(0, 135, 0);
            m_Transform.Translate(new Vector3(1, 0, -1) / slownumber * speed * Time.deltaTime, Space.World);
        }
        else
        {	//单独对四个正方向最后进行检测
            if (Input.GetKey(KeyCode.W))
            {
                //m_Transform.localRotation = Quaternion.Euler(0, 0, 0);
                m_Transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(KeyCode.S))
            {
                //m_Transform.localRotation = Quaternion.Euler(0, 180, 0);
                m_Transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(KeyCode.A))
            {
                //m_Transform.localRotation = Quaternion.Euler(0, -90, 0);
                m_Transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(KeyCode.D))
            {
                //m_Transform.localRotation = Quaternion.Euler(0, 90, 0);
                m_Transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
            }
        }
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
}
