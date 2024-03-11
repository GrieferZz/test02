using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAnimator : MonoBehaviour
{
    private Transform m_Transform;
    public bool characterMoving = false;//�ж��Ƿ����ƶ�
    private Vector3 previousPosition;//���λ��
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
        // ���ö���״̬  
        isWalkForwardBackward = false;
        isLeft = false;
        isRight = false;

        // ��鰴��״̬  
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            isWalkForwardBackward = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            isLeft = true;
            isWalkForwardBackward = false; // ����ͬʱ����ǰ���ƶ��������ƶ�  
        }
        if (Input.GetKey(KeyCode.D))
        {
            isRight = true;
            isWalkForwardBackward = false; // ����ͬʱ����ǰ���ƶ��������ƶ�  
        }

        // ���ö�������  
        _animator.SetBool("isWalkForwardBackward", isWalkForwardBackward && characterMoving);
        _animator.SetBool("isLeft", isLeft && characterMoving);
        _animator.SetBool("isRight", isRight && characterMoving);
    }
}
