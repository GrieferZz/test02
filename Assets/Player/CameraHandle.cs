using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraHandle : MonoBehaviour
{
    public GameObject box;
    public CinemachineVirtualCamera cinemachine;
    private Transform playerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        box=GameObject.FindWithTag("CameraHandle");
        playerTransform=GameObject.FindWithTag("Player").transform;
        transform.position=playerTransform.position;
        GameEventSystem.instance.onSceneLoad+=Initialization;
    }

    // Update is called once per frame
    void Update()
    {
        limitation();
    }
    void Initialization()
    {
        box=GameObject.FindWithTag("CameraHandle");
    }
    private void limitation()
    {
        if (box)
        {
            Vector3 boxSize= box.GetComponent<BoxCollider>().size;
            Vector3 boxCenter= box.GetComponent<BoxCollider>().center;
            Vector3 min= box.transform.position + boxCenter- boxSize* 0.5f;
            Vector3 max= box.transform.position + boxCenter+ boxSize* 0.5f;
            
            Vector3 cameraP = transform.position;
            
            if (playerTransform.position.x < min.x)
            {
                
                cameraP.x = min.x;
                transform.position=new Vector3(min.x,playerTransform.position.y,playerTransform.position.z);
            }
            else if (playerTransform.position.x > max.x)
            {
               
                cameraP.x = max.x;
                transform.position=new Vector3(max.x,playerTransform.position.y,playerTransform.position.z);
            }
            else 
            {
                //if(cameraP.z > min.z&&cameraP.z < max.z)
                //transform.position=playerTransform.position;
            }
            if (playerTransform.position.z < min.z)
            {
               
                cameraP.z = min.z;
                transform.position=new Vector3(playerTransform.position.x,playerTransform.position.y,min.z);
            }
            else if (playerTransform.position.z > max.z)
            {
                
                 cameraP.z = max.z;
                 transform.position=new Vector3(playerTransform.position.x,playerTransform.position.y,max.z);
            }
             else 
            {
                //if(cameraP.x > min.x&&cameraP.x < max.x)
                //
            }

            if(playerTransform.position.x > min.x&&playerTransform.position.x < max.x&&playerTransform.position.z > min.z&&playerTransform.position.z < max.z)
            {
                transform.position=playerTransform.position;
     
            }
        }

}
}
