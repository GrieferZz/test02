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
        DoorUp.transform.parent.gameObject.SetActive(MapManager.Nowroom.hasUp);
        DoorDown.transform.parent.gameObject.SetActive(MapManager.Nowroom.hasDown);
        DoorLeft.transform.parent.gameObject.SetActive(MapManager.Nowroom.hasLeft);
        DoorRight.transform.parent.gameObject.SetActive(MapManager.Nowroom.hasRight);
        IniatializationPosition=GameObject.FindWithTag(MapManager.Nowroom.initializatioonPosition.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
