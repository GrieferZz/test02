using UnityEngine;

public class CoinGet : MonoBehaviour
{
    public float speed = 5f;  // 移动速度
    private Transform player; // 玩家位置

    void Start()
    {
        // 查找标签为 "Player" 的游戏对象，并获取其 Transform
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            // 计算当前位置与玩家位置之间的方向
            Vector3 direction = player.position - transform.position;
            direction.Normalize();

            // 移动物体到玩家位置
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            Debug.LogWarning("Player not found!");
        }
    }
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}