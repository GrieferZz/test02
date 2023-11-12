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
    private bool canshoot;
    private Transform player;          // 玩家的Transform

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
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            Vector3 shootDirection = (player.position - bulletSpawnPoint.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(shootDirection));
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
               rb.velocity = shootDirection * bulletSpeed;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 玩家进入物体范围时开始发射子弹
            // 可以在这里添加更多逻辑，比如根据玩家位置改变发射方向等
            Debug.Log("进入");
            player = GameObject.FindGameObjectWithTag("Player").transform;
            canshoot=true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            canshoot=false;
        }
    }
}
