using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttck : MonoBehaviour
{
    private Animator anim;

    public PlayerInput inputControl;
    public int combo=0;
    private bool hasIncreased = false;
    private bool cd;
    public Transform bulletSpawnPoint; // 子弹发射点
    public GameObject bulletPrefab; 
    public GameObject Plane;   // 子弹预制体
    public float bulletSpeed = 10f;    // 子弹速度
    public float shootInterval = 2f;   // 发射间隔
    private float lastShootTime;
   
    private void Awake()
    {
        inputControl=new PlayerInput();
        inputControl.GamePlay.Attack.performed+=BasicAttack;
        inputControl.GamePlay.HeavyAttack.started+=Hold;
        inputControl.GamePlay.HeavyAttack.canceled += HoldRelease;
    }

    

    private void OnEnable() 
    {
        inputControl.Enable();
    }
    private void OnDisable() 
    {
        inputControl.Disable();
    }

    

    // Start is called before the first frame update
    void Start()
    {
        anim=this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      Charging();
      ChargeRelease();
      DirectionGet();
     

    }
    private void BasicAttack(InputAction.CallbackContext context)
    {
            Player.Instance.NowState=Player.PlayerState.Fight;
            if(combo>3)
            combo=0;
        
            combo++;
            StartCoroutine(ResetAttackState(1f));
            
            anim.SetTrigger("attack"+combo);
            Debug.Log("成功");
            if (Time.time - lastShootTime >= shootInterval)
        {
            
            Shoot(DirectionGet());
            lastShootTime = Time.time;
        }

        
        
        
    }
    private Vector3 DirectionGet()
    {
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(bulletSpawnPoint.transform.position);

        // 获取鼠标在屏幕上的坐标
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        // 计算二维向量
        Vector2 direction2D = new Vector2(mouseScreenPos.x - playerScreenPos.x, mouseScreenPos.y - playerScreenPos.y);

        // 构建射击方向的三维向量
        Vector3 shootingDirection = new Vector3(direction2D.x, 0f, direction2D.y);
        return shootingDirection;
    }
    

    private void Shoot(Vector3 shootingDirection)
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
           

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(shootingDirection));
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
               rb.velocity = shootingDirection.normalized * bulletSpeed;
            }
        }
    }
    private IEnumerator ResetAttackState(float delay)
    {
        int recordcombo=combo;
        cd = true;
        yield return new WaitForSeconds(delay);
        cd = false;
        if(combo-recordcombo==0)
        {
            combo=0;
            Player.Instance.NowState=Player.PlayerState.Idle;
        }
    }
     private void Hold(InputAction.CallbackContext context)
    {
            Player.Instance.NowState=Player.PlayerState.Fight;
    
        
             anim.SetBool("IsHold",true);
             anim.SetBool("IsCharging",true);
             Debug.Log("蓄力");

        
       

    }
    private void HoldRelease(InputAction.CallbackContext context)
    {
        Player.Instance.NowState=Player.PlayerState.Idle;
        anim.SetBool("IsHold",false);
        anim.SetBool("IsCharging",false);
    }
    private void Charging()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Hold"))
        {
            // 在这里执行动画未播放完毕时的操作

        }
        else
        {
            anim.SetBool("IsHold",false);
            // 在这里执行动画播放完毕时的操作
            Debug.Log("动画播放完毕！");
        }
    }
    public  void ChargIncrease()
     {
         anim.SetInteger("Charge",  anim.GetInteger("Charge")+1);
        
        
     }
     private void ChargeRelease()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            // 在这里执行动画未播放完毕时的操作

        }
        else
        {
             anim.SetInteger("Charge", 0);

           
            
            // 在这里执行动画播放完毕时的操作
            Debug.Log("动画播放完毕！");
        }

    }
}
