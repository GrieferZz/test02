using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAnimator : MonoBehaviour
{
    public bool _isShoot=false;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_isShoot)
            {
                _isShoot = true;
                _animator.SetBool("isShoot", true);
            }
            else
            {
                _animator.Play("player_animation_shoot", 0, 0f); // ǿ�ƶ�����ͷ��ʼ����
            }
            // ������·�����Ľ���  
        }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("player_animation_shoot") && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            // �����������
            _isShoot = false;
            _animator.SetBool("isShoot", false);

        }

    }
}
