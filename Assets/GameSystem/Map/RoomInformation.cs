using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RoomInformation : MonoBehaviour
{
    public GameObject DoorUp, DoorDown, DoorLeft, DoorRight;

    public bool hasUp,hasDown,hasLeft,hasRight;

    public int SteptoStart;
    public TextMeshProUGUI text;

    public int DoorNumber;
    public GameObject Target;
    public enum InitializatioonPosition{Up,Down,Left,Right,Null};
    public InitializatioonPosition initializatioonPosition=InitializatioonPosition.Null;
    private List<GameObject> Doors =new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        DoorUp.SetActive(hasUp);
        DoorDown.SetActive(hasDown);
        DoorLeft.SetActive(hasLeft);
        DoorRight.SetActive(hasRight);
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateRoom()
    {
        SteptoStart=(int)(Mathf.Abs(transform.localPosition.x/50)+Mathf.Abs(transform.localPosition.y/50));
        //text.text=SteptoStart.ToString();
        
        if(hasUp)
          DoorNumber++;
        if(hasDown)
          DoorNumber++;
        if(hasLeft)
          DoorNumber++;
        if(hasRight)
          DoorNumber++;
    }

}
