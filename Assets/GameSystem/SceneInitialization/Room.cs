using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject DoorUp, DoorDown, DoorLeft, DoorRight;
    public GameObject IniatializationPosition;

    // Start is called before the first frame update
    private void Start() 
    {
        DoorUp=GameObject.FindWithTag("Up");
        DoorDown=GameObject.FindWithTag("Down");
        DoorLeft=GameObject.FindWithTag("Left");
        DoorRight=GameObject.FindWithTag("Right");
        if(GameManager.Instance.NowRoom.GetComponent<RoomStates>().RoomData.roomType!=RoomStates_SO.RoomType.Origin)
        {
        DoorUp.transform.parent.gameObject.SetActive(GameManager.Instance.NowRoom.GetComponent<RoomInformation>().hasUp);
        DoorDown.transform.parent.gameObject.SetActive(GameManager.Instance.NowRoom.GetComponent<RoomInformation>().hasDown);
        DoorLeft.transform.parent.gameObject.SetActive(GameManager.Instance.NowRoom.GetComponent<RoomInformation>().hasLeft);
        DoorRight.transform.parent.gameObject.SetActive(GameManager.Instance.NowRoom.GetComponent<RoomInformation>().hasRight);
        IniatializationPosition=GameObject.FindWithTag(GameManager.Instance.NowRoom.GetComponent<RoomInformation>().initializatioonPosition.ToString());

        }
        else if(GameManager.Instance.NowRoom.GetComponent<RoomStates>().RoomData.roomType==RoomStates_SO.RoomType.Origin)
        {
        DoorUp.transform.parent.gameObject.SetActive(GameManager.Instance.NowRoom.GetComponent<RoomInformation>().hasUp);
        DoorDown.transform.parent.gameObject.SetActive(GameManager.Instance.NowRoom.GetComponent<RoomInformation>().hasDown);
        DoorLeft.transform.parent.gameObject.SetActive(GameManager.Instance.NowRoom.GetComponent<RoomInformation>().hasLeft);
        DoorRight.transform.parent.gameObject.SetActive(GameManager.Instance.NowRoom.GetComponent<RoomInformation>().hasRight);
        IniatializationPosition=GameObject.Find("OriginPosition");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
