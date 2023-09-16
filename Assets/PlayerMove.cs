using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;

    public float MoveSpeed;
    private Vector3 MoveInput;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Animate();
    }


    private void Move()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        if (Horizontal==0&&Vertical==0)
        {
            rb.velocity = new Vector2(0, 0);
            return;
        }
        MoveInput = new Vector3(Horizontal, 0, Vertical);
        rb.velocity = MoveInput * MoveSpeed * Time.fixedDeltaTime;
    }

    private void Animate()
    {
        anim.SetFloat("MovementX",MoveInput.x);
        anim.SetFloat("MovementZ", MoveInput.z);
    }
}