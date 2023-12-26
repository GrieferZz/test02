using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Linq;
public class MapManager : MonoBehaviour
{
    public GameObject Map; 
    public GameObject MapBackGround;
    public Image RoomPrefab;
    public int RoomNumber;
    public Color StartColor,EndColor,NowColor;
    private GameObject EndRoom;
    private GameObject FirstRoom;

    public Transform GeneratorPoint;
    public static Transform OriginPoint;
    public float Xoffset;
    public float Yoffset;

    public List<RoomInformation> Rooms=new List<RoomInformation>();

    public enum Direction{UP,Down,Left,Right};
    public Direction direction;
    public LayerMask RoomLayer;
     public PlayerInput inputControl;
    public int MaxStep;
    List<GameObject> Farrooms =new List<GameObject>();
    List<GameObject> LessFarrooms =new List<GameObject>();
    List<GameObject> OneWayrooms =new List<GameObject>();
    [SerializeField]
    public static RoomInformation Nowroom;
     
    // Start is called before the first frame update
    private void Awake() 
    {
        inputControl=new PlayerInput();
        inputControl.GamePlay.Attack.started+=MapTest;
        GameEventSystem.instance.onScenechange+=FindNowRoom;
        GameEventSystem.instance.onSceneLoad+=RefreshRoom;
    }

  

    private void OnEnable() 
    {
        inputControl.Enable();
        OriginPoint=GeneratorPoint;
         GeneratorRooomStart();
         MapOffset();
    }
    private void OnDisable() 
    {
        inputControl.Disable();
    }

    private void MapTest(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Player.Instance.NowState=Player.PlayerState.Idle;
    }

    void onen()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GeneratorRooomStart()
    {
        for(int i=0;i<RoomNumber;i++)
        {
            Rooms.Add(Instantiate(RoomPrefab,GeneratorPoint.transform.position,Quaternion.identity).GetComponent<RoomInformation>());
            Rooms[i].transform.SetParent(Map.transform);
            ChangePostionPos();
        }
        Rooms[0].GetComponent<Image>().color=StartColor;
        Nowroom=Rooms[0];
        Nowroom.Target.SetActive(true);
        EndRoom=Rooms[0].gameObject;
        foreach(var room in Rooms)
        {
            /*if(Vector3.Distance(room.transform.position,Rooms[0].transform.position)>Vector3.Distance(EndRoom.transform.position,Rooms[0].transform.position))
            {
                EndRoom=room.gameObject;
            }*/
            SetupRoom(room,room.transform.position);
        }
        FindEndRoom();
        EndRoom.GetComponent<Image>().color=EndColor;
    }
    public void ChangePostionPos()
    {
        do
        {
        direction =(Direction)Random.Range(0,4);
        Debug.Log(direction);
        switch(direction)
        {
            case Direction.UP:
                 GeneratorPoint.transform.position += new Vector3 (0,Yoffset,0);
                 break;
            case Direction.Down:
                GeneratorPoint.transform.position += new Vector3 (0,-Yoffset,0);
                 break;
            case Direction.Left:
                 GeneratorPoint.transform.position += new Vector3 (-Xoffset,0,0);
                 break;
            case Direction.Right:
                 GeneratorPoint.transform.position += new Vector3 (Xoffset,0,0);
                 break;
        }
        } while (Physics2D.OverlapCircle(GeneratorPoint.position,0.2f,RoomLayer));
          
    
    }
    public void SetupRoom(RoomInformation newroom,Vector3 roomPostion)
    {
        newroom.hasUp = Physics2D.OverlapCircle(roomPostion + new Vector3(0, Yoffset, 0), 0.2f, RoomLayer);
        newroom.hasDown = Physics2D.OverlapCircle(roomPostion + new Vector3(0, -Yoffset, 0), 0.2f, RoomLayer);
        newroom.hasLeft = Physics2D.OverlapCircle(roomPostion + new Vector3(-Xoffset, 0, 0), 0.2f, RoomLayer);
        newroom.hasRight = Physics2D.OverlapCircle(roomPostion + new Vector3(Xoffset, 0, 0), 0.2f, RoomLayer);

        newroom.UpdateRoom();

    }
    public void FindFirstRoom()
    {
        Rooms[0].GetComponent<Image>().color=StartColor;
        bool isuniquedoor;
        
        switch (direction)
        {
            case Direction.UP:
                 
                 break;
            case Direction.Down:
                 
                 break;
            case Direction.Left:
                 
                 break;
            case Direction.Right:
                
                 break;
            
            
        }

    }
    public void FindEndRoom()
    {
        for(int i=0;i<Rooms.Count;i++)
        {
            if(Rooms[i].SteptoStart>MaxStep)
            {
                MaxStep=Rooms[i].SteptoStart;
            }

        }
        foreach(var room in Rooms)
        {
            if(room.SteptoStart==MaxStep)
            {
                Farrooms.Add(room.gameObject);
            }
            if(room.SteptoStart==MaxStep-1)
            {
                LessFarrooms.Add(room.gameObject);
            }
        }
         for(int i=0;i<Farrooms.Count;i++)
        {
            if(Farrooms[i].GetComponent<RoomInformation>().DoorNumber>MaxStep)
            {
                OneWayrooms.Add(Farrooms[i]);
            }

        }
        for(int i=0;i<LessFarrooms.Count;i++)
        {
            if(LessFarrooms[i].GetComponent<RoomInformation>().DoorNumber>MaxStep)
            {
                OneWayrooms.Add(LessFarrooms[i]);
            }

        }
        if(OneWayrooms.Count!=0)
        {
            EndRoom=OneWayrooms[Random.Range(0,OneWayrooms.Count)];
        }
        else
        {
            EndRoom=Farrooms[Random.Range(0,Farrooms.Count)];
        }

    }
    public void MapOffset()
    {
        Vector3 MapOffset=EndRoom.transform.position-Rooms[0].transform.position;
        Vector3 MapCenterXOffset=(Farrooms[Random.Range(0,Farrooms.Count)].transform.position-Rooms[0].transform.position)/2;
         //Vector3 MapCenterYOffset=(EndRoom.transform.position-Rooms[Rooms.Count/2].transform.position)/2;
         if(Mathf.Abs(MapCenterXOffset.x)<=Mathf.Abs(MapCenterXOffset.y))
         {
            MapCenterXOffset-=new Vector3(MapCenterXOffset.x/4,0,0);
         }
         else
         {
            MapCenterXOffset-=new Vector3(0,MapCenterXOffset.y/4,0);
         }
        Map.transform.position-=MapCenterXOffset*1f;
        
        MapBackGround.transform.position+=MapCenterXOffset*1f;
    }
    public void FindNowRoom(GameEventSystem.Direction direction )
    {
        if(direction!=null)
        {
            switch (direction)
        {
            case GameEventSystem.Direction.Up:
                 Nowroom=Physics2D.OverlapCircle(Nowroom.gameObject.transform.position+ new Vector3(0, Yoffset, 0), 0.2f, RoomLayer).gameObject.GetComponent<RoomInformation>();
                 Nowroom.initializatioonPosition=RoomInformation.InitializatioonPosition.Down;
                 break;
            case GameEventSystem.Direction.Down:
                 Nowroom=Physics2D.OverlapCircle(Nowroom.gameObject.transform.position+ new Vector3(0, -Yoffset, 0), 0.2f, RoomLayer).gameObject.GetComponent<RoomInformation>();
                Nowroom.initializatioonPosition=RoomInformation.InitializatioonPosition.Up;
                 break;
            case GameEventSystem.Direction.Left:
                 Nowroom=Physics2D.OverlapCircle(Nowroom.gameObject.transform.position+ new Vector3(-Xoffset, 0, 0), 0.2f, RoomLayer).gameObject.GetComponent<RoomInformation>();
                Nowroom.initializatioonPosition=RoomInformation.InitializatioonPosition.Right;
                 break;
            case GameEventSystem.Direction.Right:
                 Nowroom=Physics2D.OverlapCircle(Nowroom.gameObject.transform.position+ new Vector3(Xoffset, 0, 0), 0.2f, RoomLayer).gameObject.GetComponent<RoomInformation>();
                Nowroom.initializatioonPosition=RoomInformation.InitializatioonPosition.Left;
                 break;
            
            
        }

        }
        foreach(var room in Rooms)
        {
            room.Target.SetActive(false);

        }
        Nowroom.Target.SetActive(true);
        
    }
    public void RefreshRoom()
    {
         Player.Instance.NowState=Player.PlayerState.Idle;
    }

  
}
