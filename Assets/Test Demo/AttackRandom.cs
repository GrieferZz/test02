using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRandom : MonoBehaviour
{public GameObject bulletPrefab;    // 子弹预制体
    public Transform spawnPoint;       // 子弹发射点
    public float bulletSpeed = 10f;    // 子弹速度
    public int bulletsPerSecond = 5;   // 每秒发射的子弹数量
    public float spreadAngle = 30f;    // 子弹的发射角度范围

    private bool isShooting = false;

    private void Start()
    {
        StartShooting();
    }

    private void StartShooting()
    {
        if (!isShooting)
        {
            isShooting = true;
            StartCoroutine(ShootBullets());
        }
    }

    private void StopShooting()
    {
        isShooting = false;
    }

    private IEnumerator ShootBullets()
    {
        while (isShooting)
        {
            for (int i = 0; i < bulletsPerSecond; i++)
            {
                // 随机生成子弹发射方向
                float angle = Random.Range(-spreadAngle, spreadAngle);
                Vector3 shootDirection = Quaternion.Euler(0, angle, 0) * transform.forward;

                // 创建子弹对象
                GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.LookRotation(shootDirection));
                Rigidbody rb = bullet.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.velocity = shootDirection * bulletSpeed;
                }
            }

            yield return new WaitForSeconds(1.0f / bulletsPerSecond);
        }
    }
}
