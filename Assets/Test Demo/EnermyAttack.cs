using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyAttack : MonoBehaviour
{
    public Transform bulletSpawnPoint; // 子弹发射点
     public  List<WeaponStates_SO> bulletPrefabTemplete=new List<WeaponStates_SO>(); 
    [HideInInspector]
    public  List<WeaponStates_SO> bulletPrefab=new List<WeaponStates_SO>();
    [HideInInspector]
    public WeaponStates_SO nowBulletPrefab;    // 子弹预制体
    private float lastShootTime;
    public static bool canshoot;
    public static Transform player;          // 玩家的Transform
    private Bullet bulletType;


    private void Update()
    {
       /* if(canshoot&&nowBulletPrefab!=null)
        {
            if (Time.time - lastShootTime >= nowBulletPrefab.shootInterval)
        {
            
            Shoot();
            lastShootTime = Time.time;
        }

        }*/
       
    }
     void Start()
    {
        
        
        for(int i=0;i<bulletPrefabTemplete.Count;i++)
        {
           bulletPrefab[i]=Instantiate(bulletPrefabTemplete[i]);
        }
        nowBulletPrefab=bulletPrefab[0];
    }
    
    public void Shoot()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null&&player!=null)
        {
            Vector3 shootDirection = (player.position - bulletSpawnPoint.position).normalized;

            GameObject bullet = Instantiate(nowBulletPrefab.weaponPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(shootDirection));
            bulletType=bullet.GetComponent<Bullet>();
            bulletType.attackObject=Bullet.AttackObject.ForPlayer;
            bulletType.InitiatorStates=gameObject.GetComponent<CharacterStates>();
            bullet.GetComponent<WeaponStates>().templateData=nowBulletPrefab;
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
               rb.velocity = shootDirection.normalized *nowBulletPrefab.flightSpeed;
            }
        }
    }
    public void ShootPro(int numberOfBullets, float angleBetweenBullets)
{
    if (bulletPrefab != null && bulletSpawnPoint != null && player != null)
    {
        // 计算中心方向，即朝向玩家的方向
        Vector3 shootDirection = (player.position - bulletSpawnPoint.position).normalized;

        // 计算第一个子弹的初始角度
        float startAngle = -((numberOfBullets - 1) * angleBetweenBullets) / 2;
        
        for (int i = 0; i < numberOfBullets; i++)
        {
            // 计算每个子弹的发射方向
            float currentAngle =  i * angleBetweenBullets;
            Vector3 currentDirection = Quaternion.Euler(0, currentAngle, 0) * shootDirection;

            // 实例化子弹
            GameObject bullet = Instantiate(nowBulletPrefab.weaponPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(currentDirection));
            bulletType = bullet.GetComponent<Bullet>();
            bulletType.attackObject = Bullet.AttackObject.ForPlayer;
            bulletType.InitiatorStates = gameObject.GetComponent<CharacterStates>();
            bullet.GetComponent<WeaponStates>().templateData = nowBulletPrefab;

            // 设置子弹的速度和方向
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = currentDirection.normalized * nowBulletPrefab.flightSpeed;
            }
        }
    }
}
 
     
   
}
