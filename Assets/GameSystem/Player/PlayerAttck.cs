
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttck : MonoBehaviour
{
    public enum AttackType{Defalut,Track,Sticky}
    public AttackType nowAttackType;
    public GameObject aiming;
    public int attackTypeIndex = 0;
    private Animator anim;

    public PlayerInput inputControl;
   
    public int combo=0;
    private bool hasIncreased = false;
    private bool IsHold;
    private bool cd;
    public Transform bulletSpawnPoint; // 子弹发射点
    public  List<WeaponStates_SO> bulletPrefabTemplete=new List<WeaponStates_SO>(); 
    [HideInInspector]
    public  List<WeaponStates_SO> bulletPrefab=new List<WeaponStates_SO>(); 
    public  List<GameObject> stickyBullets=new List<GameObject>(); 
    private WeaponStates_SO nowBulletPrefab;
    public Bullet bulletType;
    public GameObject Plane;   // 子弹预制体
        private float lastShootTime;




    private void Awake()
    {
        inputControl=new PlayerInput();
        inputControl.GamePlay.Attack.started+=BasicAttack;
        inputControl.GamePlay.Attack.performed+=BasicAttackHold;
        inputControl.GamePlay.Attack.canceled+=BasicAttackCancel;
        inputControl.GamePlay.AttackType.started+=AttackTypeChange;
        inputControl.GamePlay.HeavyAttack.started+=Hold;
         inputControl.GamePlay.HeavyAttack.started+=Detonated;
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
        
        for(int i=0;i<bulletPrefabTemplete.Count;i++)
        {
           bulletPrefab[i]=Instantiate(bulletPrefabTemplete[i]);
        }
        nowBulletPrefab=bulletPrefab[0];
    }

    // Update is called once per frame
    void Update()
    {
      AttackHold();
      Charging();
      ChargeRelease();
      DirectionGet();
      //StickyBulletUpdate();


    }
    private void AttackTypeChange(InputAction.CallbackContext context)
    {
         attackTypeIndex= (attackTypeIndex + 1) % 3;
         nowAttackType= (AttackType)attackTypeIndex;
    }
    private void BasicAttack(InputAction.CallbackContext context)
    {
           

            if(combo>3)
            combo=0;
        
            combo++;
            StartCoroutine(ResetAttackState(1f));
            
            anim.SetTrigger("attack"+combo);
            Debug.Log("成功");
           
           
       // if(context.interaction is UnityEngine.InputSystem.Interactions.HoldInteraction)
        {
            Player.Instance.NowState=Player.PlayerState.Fight;
             if (Time.time - lastShootTime >=nowBulletPrefab.shootInterval)
        {
            
            Shoot(new Vector3(-(bulletSpawnPoint.transform.position.x-aiming.transform.GetChild(0).position.x),0f,-(bulletSpawnPoint.transform.position.z-aiming.transform.GetChild(0).position.z)));
            lastShootTime = Time.time;
        }

        }

                
    }
    private void BasicAttackHold(InputAction.CallbackContext context)
    {

            Player.Instance.NowState=Player.PlayerState.Fight;
            IsHold=true;

    }
      private void BasicAttackCancel(InputAction.CallbackContext context)
    {
            IsHold=false;
    }
    private void AttackHold()
    {
        if(IsHold)
        {
            if (Time.time - lastShootTime >= nowBulletPrefab.shootInterval)
        {
            
            Shoot(new Vector3(-(bulletSpawnPoint.transform.position.x-aiming.transform.GetChild(0).position.x),0f,-(bulletSpawnPoint.transform.position.z-aiming.transform.GetChild(0).position.z)));
            lastShootTime = Time.time;
        }
        }
    }
  
    public Vector3 DirectionGet()
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
    

    private void Shoot(Vector3 Direction)
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            switch (nowAttackType)
            {
                case AttackType.Defalut:
                     nowBulletPrefab=bulletPrefab[0];
                     GameEventSystem.instance.MusicPlay("follow");
                     break;
                case AttackType.Track:
                     nowBulletPrefab=bulletPrefab[1];
                     GameEventSystem.instance.MusicPlay("trace");
                     break;
                case AttackType.Sticky:
                     nowBulletPrefab=bulletPrefab[2];
                     GameEventSystem.instance.MusicPlay("sticky");
                     
                     break;

            }

            GameObject bullet = Instantiate(nowBulletPrefab.weaponPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(Direction));
            if(nowAttackType==AttackType.Sticky)
            {
                //StickyBulletLimit(bullet);
            }
            bulletType=bullet.GetComponent<Bullet>();
            bulletType.attackObject=Bullet.AttackObject.ForEnermy;
            bulletType.InitiatorStates=gameObject.GetComponent<CharacterStates>();
            bullet.GetComponent<WeaponStates>().templateData=nowBulletPrefab;
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
               rb.velocity = Direction.normalized *nowBulletPrefab.flightSpeed;
            }
            AttackManager.instance.Shoot();
            
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
    private void Detonated(InputAction.CallbackContext context)
    {
         GameEventSystem.instance.Detonation();
    }
    private void StickyBulletLimit(GameObject stickybullet)
    {

        if(stickyBullets.Count<3)
        {
            stickyBullets.Add(stickybullet);
            
        }
        else if(stickyBullets.Count>=3)
        {
            stickyBullets[0].GetComponent<StickyBullet>().StickyBulletExplode();
            stickyBullets.RemoveAt(0);
            stickyBullets.Add(stickybullet);
        }
        
    }
    private void StickyBulletUpdate()
    {
        for(int i=0;i<stickyBullets.Count;i++)
        {
             if(stickyBullets[i]==null)
             {
                stickyBullets.RemoveAt(i);

             }
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
