using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLaser : MonoBehaviour
{
    public GameObject Laser;
    private GameObject LaserPrefab;
    public float displayTime = 2f;  // 每个物体的显示时间
    public float intervalTime = 5f; // 两个物体之间的间隔时间
    private bool isLocking;
    // Start is called before the first frame update
    void Start()
    {
        SpawnCubeAboveCharacter();
        StartCoroutine(DisplayObjects());
    }

    // Update is called once per frame
    void Update()
    {
        TargetLock();
    }
    void SpawnCubeAboveCharacter()
    {
        // 生成长方体预制件
       LaserPrefab = Instantiate(Laser, transform.position, Quaternion.identity);

        

        // 将长方体设置为角色的子物体
        LaserPrefab.transform.parent = transform;

        // 获取长方体的高度（假设长方体在本地坐标系的 Y 轴是高度）
        float cubeHeight = LaserPrefab.transform.GetChild(1).gameObject.GetComponent<Renderer>().bounds.size.z;

        // 将长方体的底部与角色的顶部对齐，并在 z 轴上减去自身长度一半
        LaserPrefab.transform.localPosition = new Vector3(LaserPrefab.transform.localPosition.x, LaserPrefab.transform.localPosition.y-0.5f, cubeHeight);

        // 设置长方体的旋转，确保一端与角色中心对齐
        LaserPrefab.transform.localRotation = Quaternion.Euler(0f, 0f, 0f); // 这里的角度可以根据需要调整
    
    }
    private void TargetLock()
    {
        if (GameObject.FindWithTag("Player")!= null&&isLocking==true)
        {
            // 获取玩家位置
            Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;

            // 计算角色朝向玩家的方向
            Vector3 directionToPlayer = playerPosition - transform.position;

            // 将角色的朝向设置为计算得到的方向（只旋转角度）
            transform.rotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        }
        
    }
    IEnumerator DisplayObjects()
    {
        while (true)
        {
            isLocking=true;
            // 显示第一个物体
            LaserPrefab.transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(displayTime);

            // 隐藏第一个物体
            LaserPrefab.transform.GetChild(2).gameObject.SetActive(true);
            LaserPrefab.transform.GetChild(0).gameObject.SetActive(false);
             isLocking=false;
            // 等待间隔时间
            yield return new WaitForSeconds(intervalTime);

            // 显示第二个物体
             LaserPrefab.transform.GetChild(1).gameObject.SetActive(true);
             LaserPrefab.transform.GetChild(2).gameObject.SetActive(false);
            yield return new WaitForSeconds(intervalTime);

            // 隐藏第二个物体
             LaserPrefab.transform.GetChild(1).gameObject.SetActive(false);
             isLocking=true;
            // 等待间隔时间
            yield return new WaitForSeconds(intervalTime);
        }
    }
}
