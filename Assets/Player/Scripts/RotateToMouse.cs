using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    //获取主摄像机对象，利用射线，很重要
    public GameObject MainCamera;
    //摄像机组件
    private Camera camera;
    //最大长度，射线探测的，提高运行效率
    public float maxLength;

    //射线和摄像头配合获取鼠标的方向，让指定的点位转向鼠标方向
    //射线对象
    private Ray rayMouse;
    //点
    private Vector3 position;
    //方向
    private Vector3 direction;
    //四元数，用来设置旋转
    private Quaternion rotation;
    //摄像投射碰撞对象，存储信息的点
    private RaycastHit hit;
    // Start is called before the first frame updateUnity 消息| 个引用void Start()
    //获取主摄像机对象中的摄像机组件
    void Start()
    {
        camera = MainCamera.GetComponent<Camera>();
    }


    // Update is called once per frame
    void Update()
    {
       // this.transform.y = 0;
        if (camera != null)
        {
            //获取鼠标点位
            var mousePosition = Input.mousePosition;
            //从摄像机发射射线到指定点位，即鼠标所点选位置，生成一个射线
            rayMouse = camera.ScreenPointToRay(mousePosition);
            //用物理引擎的Raycast方法来测试，射线是否能跟物体碰撞
            if (Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit, maxLength))
            {
                //如果能够碰撞，则让物体自身转向
                RotateToMouseDirection(gameObject, hit.point);
            }
            else
            {
                //如果不能碰撞，则获取射线最远能达到的点
                var pos = rayMouse.GetPoint(maxLength);
                //转向最远的点位
                RotateToMouseDirection(gameObject, pos);
            }
        }
        else
        {
            Debug.Log("No Camera!!!");
        }


        // 让玩家对象转向鼠标方向
        // obi 到时获取的就是玩家对象
        // destination 就是鼠标指向，即鼠标当前所在位置
        void RotateToMouseDirection(GameObject obj, Vector3 destination) {
            //矢量相减，得到的就是指向被减矢量的方向新矢量
            direction = destination - obj.transform.position;
            //获取转向
            rotation = Quaternion.LookRotation(direction, Vector3.up);

            //插值旋转，让旋转变的更顺滑

            obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);

           

        }
    }
}
