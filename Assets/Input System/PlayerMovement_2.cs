using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_2 : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;

    public float MoveSpeed;
    private Vector3 MoveInput;
    private int combo = 0;
    private bool one = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Animate();
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("isAttack", true);
            if (combo >= 1)
                anim.SetTrigger("attack1");
            combo++;
            Debug.Log("1");

        }
    }


    private void Move()
    {
        float horizontal1 = Input.GetAxisRaw("Horizontal");
        float vertical1 = Input.GetAxisRaw("Vertical");

        if (Mathf.Approximately(horizontal1, 0) && Mathf.Approximately(vertical1, 0))
        {
            rb.velocity = Vector3.zero;
            anim.SetBool("IsMoving", false); // No input, set IsMoving to false
            return;
        }

        MoveInput = new Vector3(horizontal1, 0, vertical1).normalized;
        rb.velocity = MoveInput * MoveSpeed * Time.fixedDeltaTime;
        anim.SetBool("IsMoving", true); // There is input, set IsMoving to true
    }

    private void Animate()
    {
        anim.SetFloat("MovementX", MoveInput.x);
        anim.SetFloat("MovementZ", MoveInput.z);
    }

}
