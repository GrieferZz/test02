using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Target : MonoBehaviour
{
    public Transform body;
    //public Transform endbody;//身体
    public LayerMask terrainLayer;          //检测图层
    Vector3 newposition, oldposition, currentposition; //位置
    public Vector3 a = new Vector3(0, 0, 1);
    public float footSpacing1, footSpacing2; //落点偏移
    public float stepstance;                //步长
    public float high = 0.1f;               //高度
    public float speed = 2;                 //速度
    float lerp = 1;
    public Target leg1, leg2;                //约束
   // public float x=0f, y=0f, z=0f;
    //public Vector3 a = new Vector3(0f, 0f, 0f);


    private void Start()
    {
        newposition = transform.position;
        currentposition = transform.position;
        //oldposition = transform.position;
        
    }

    void Update()
    {
        transform.position = currentposition;

        Vector3 start = body.position + (body.up * footSpacing1) + (body.right * footSpacing2);//+a
        Vector3 end = new Vector3(body.position.x, -2, body.position.z) + (body.forward * footSpacing1) + (body.right * footSpacing2);
        //Vector3 end = new Vector3(into.x, -2, body.position.z) + (body.forward * footSpacing1) + (body.right * footSpacing2);
        Ray ray = new Ray(start, body.up);
           Debug.Log(body.up);
        Debug.DrawLine(start, end, Color.red);

        if (Physics.Raycast(ray, out RaycastHit info, 20, terrainLayer.value))
        {


            if (Vector3.Distance(newposition, info.point) > stepstance && leg1.lerp >= 1 && leg2.lerp >= 1)//
            {

                lerp = 0;
                newposition = info.point;
                //Debug.Log(body.forward);
            }

            if (lerp < 1)
            {

                Vector3 footposition = Vector3.Lerp(oldposition, newposition, lerp);
                footposition.y += Mathf.Sin(lerp * Mathf.PI) * high;
                currentposition = footposition;
                lerp += Time.deltaTime * speed;
            }
            else
            {

                oldposition = newposition;

            }
        }

        else
        {
            oldposition = newposition;
        }
        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(newposition, 0.1f);


    }

}
