using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAct : MonoBehaviour
{
    public float speed = 5;
    private float slownumber = 1.14f;//���ư����ƶ����ٶȻ�����ͬ
    private Transform m_Transform;
    public bool characterMoving = false;//�ж��Ƿ����ƶ�
    private Vector3 previousPosition;//���λ��
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

        //�жϽ�ɫ�Ƿ����ƶ�
        if (transform.position != previousPosition)
        {
            characterMoving = true;
            previousPosition = transform.position; // ����ǰһ֡��λ��
        }
        else
        {
            characterMoving = false;
        }

        // ����ƶ�״̬
        Debug.Log("Is Moving: " + characterMoving);
    }

    void playerMove()

    {


        //����ĸ�б��İ���
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            //m_Transform.localRotation = Quaternion.Euler(0, -45, 0);//��ת����Ԫ��
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
        {	//�������ĸ������������м��
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
