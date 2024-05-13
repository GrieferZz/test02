using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyAttack : MonoBehaviour
{
    public Transform bulletSpawnPoint; // 子弹发射点
     public  List<WeaponStates_SO> bulletPrefabTemplete=new List<WeaponStates_SO>(); 
    [HideInInspector]
    public  List<WeaponStates_SO> bulletPrefab=new List<WeaponStates_SO>();
    private WeaponStates_SO nowBulletPrefab;    // 子弹预制体
    private float lastShootTime;
    public static bool canshoot;
    public static Transform player;          // 玩家的Transform
    private Bullet bulletType;


    private void Update()
    {
        if(canshoot&&nowBulletPrefab!=null)
        {
            if (Time.time - lastShootTime >= nowBulletPrefab.shootInterval)
        {
            
            Shoot();
            lastShootTime = Time.time;
        }

        }
       
    }
     void Start()
    {
        
        
        for(int i=0;i<bulletPrefabTemplete.Count;i++)
        {
           bulletPrefab[i]=Instantiate(bulletPrefabTemplete[i]);
        }
        nowBulletPrefab=bulletPrefab[0];
    }
    
    private void Shoot()
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
 
     
   
}
