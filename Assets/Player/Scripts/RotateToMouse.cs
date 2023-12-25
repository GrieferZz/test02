using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    //��ȡ������������������ߣ�����Ҫ
    public GameObject MainCamera;
    //��������
    private Camera camera;
    //��󳤶ȣ�����̽��ģ��������Ч��
    public float maxLength;

    //���ߺ�����ͷ��ϻ�ȡ���ķ�����ָ���ĵ�λת����귽��
    //���߶���
    private Ray rayMouse;
    //��
    private Vector3 position;
    //����
    private Vector3 direction;
    //��Ԫ��������������ת
    private Quaternion rotation;
    //����Ͷ����ײ���󣬴洢��Ϣ�ĵ�
    private RaycastHit hit;
    // Start is called before the first frame updateUnity ��Ϣ| ������void Start()
    //��ȡ������������е���������
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
            //��ȡ����λ
            var mousePosition = Input.mousePosition;
            //��������������ߵ�ָ����λ�����������ѡλ�ã�����һ������
            rayMouse = camera.ScreenPointToRay(mousePosition);
            //�����������Raycast���������ԣ������Ƿ��ܸ�������ײ
            if (Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit, maxLength))
            {
                //����ܹ���ײ��������������ת��
                RotateToMouseDirection(gameObject, hit.point);
            }
            else
            {
                //���������ײ�����ȡ������Զ�ܴﵽ�ĵ�
                var pos = rayMouse.GetPoint(maxLength);
                //ת����Զ�ĵ�λ
                RotateToMouseDirection(gameObject, pos);
            }
        }
        else
        {
            Debug.Log("No Camera!!!");
        }


        // ����Ҷ���ת����귽��
        // obi ��ʱ��ȡ�ľ�����Ҷ���
        // destination �������ָ�򣬼���굱ǰ����λ��
        void RotateToMouseDirection(GameObject obj, Vector3 destination) {
            //ʸ��������õ��ľ���ָ�򱻼�ʸ���ķ�����ʸ��
            direction = destination - obj.transform.position;
            //��ȡת��
            rotation = Quaternion.LookRotation(direction, Vector3.up);

            //��ֵ��ת������ת��ĸ�˳��

            obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);

           

        }
    }
}
