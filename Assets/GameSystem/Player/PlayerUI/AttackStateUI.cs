using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateUI : MonoBehaviour
{
    public PlayerAttck playerAttck;
    
    public int attackTypeIndex = 0;
    private float currentRotationAngle;
    public float rotationSpeed;
    public float targetAngle = 0f; // 目标旋转角度
    public float[] targetAngles = { 0f, -120f, -240f }; // 目标旋转角度数组
    private Quaternion targetRotation; // 目标旋转角度

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackTypeIndex=playerAttck.attackTypeIndex;
        AttackTypeUIChange();
    }
    private void AttackTypeUIChange()
    {
       targetRotation = Quaternion.Euler(0f, 0f, targetAngles[attackTypeIndex]);
       transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        

    }
}
