using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyAttack : MonoBehaviour
{
    public Transform bulletSpawnPoint; // 子弹发射点
    public GameObject bulletPrefab;    // 子弹预制体
    public float bulletSpeed = 10f;    // 子弹速度
    public float shootInterval = 2f;   // 发射间隔
    private float lastShootTime;
    public static bool canshoot;
    public static Transform player;          // 玩家的Transform
    private Bullet bulletType;

    private void Update()
    {
        if(canshoot)
        {
            if (Time.time - lastShootTime >= shootInterval)
        {
            
            Shoot();
            lastShootTime = Time.time;
        }

        }
       
    }

    private void Shoot()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null&&player!=null)
        {
            Vector3 shootDirection = (player.position - bulletSpawnPoint.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(shootDirection));
            bulletType=bullet.GetComponent<Bullet>();
            bulletType.attackObject=Bullet.AttackObject.ForPlayer;
            bulletType.InitiatorStates=gameObject.GetComponent<CharacterStates>();
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
               rb.velocity = shootDirection * bulletSpeed;
            }
        }
    }
 
     
   
}
