using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using System.Runtime.CompilerServices;
public class MapManager : MonoBehaviour
{
    public GameObject Map; 
    public GameObject MapBackGround;
    public Image RoomPrefab;
    public int RoomNumber;
    public Color StartColor,EndColor,NowColor;
    private GameObject EndRoom;
    private GameObject FirstRoom;
    public GameObject AwardRoom;

    public Transform GeneratorPoint;
    public static Transform OriginPoint;
    public float Xoffset;
    public float Yoffset;

    public List<RoomInformation> Rooms=new List<RoomInformation>();

    public enum Direction{UP,Down,Left,Right};
    private Direction lastdirection;
    public Direction direction;
    public LayerMask RoomLayer;
     public PlayerInput inputControl;
    public int MaxStep;
    List<GameObject> Farrooms =new List<GameObject>();
    List<GameObject> LessFarrooms =new List<GameObject>();
    List<GameObject> OneWayrooms =new List<GameObject>();
    [SerializeField]
    public static RoomInformation Nowroom;
    public RoomInformation test;
    public GameObject mapLocation;
     
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
        RoomNumber=GameManager.Instance.NowLayer.RoomNumber;
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
        MapShow();
    }
    public void GeneratorRooomStart()
    {
        for(int i=0;i<RoomNumber;i++)
        {
            Rooms.Add(Instantiate(RoomPrefab,GeneratorPoint.transform.position,Quaternion.identity).GetComponent<RoomInformation>());
            Rooms[i].transform.SetParent(Map.transform);
            GameManager.Instance.RigisterRooms(Rooms[i].gameObject);
            ChangePostionPos();
        }

       
        for(int i=0;i<Rooms.Count-1;i++)
        {
            /*if(Vector3.Distance(room.transform.position,Rooms[0].transform.position)>Vector3.Distance(EndRoom.transform.position,Rooms[0].transform.position))
            {
                EndRoom=room.gameObject;
            }*/
            
            SetupRoom(Rooms[i],Rooms[i].transform.position);
        }
         FirstRoomSetup();
        /* switch(lastdirection)
        {
           case Direction.UP:
                Rooms[0].hasUp=true;
                 break;
            case Direction.Down:
                Rooms[0].hasDown=true;
                 break;
            case Direction.Left:
                 Rooms[0].hasLeft=true;
                 break;
            case Direction.Right:
                Rooms[0].hasRight=true;
                 break;
        }*/
        FindEndRoom();
        EndRoom.transform.GetChild(0).gameObject.SetActive(false);
        EndRoom.transform.GetChild(1).gameObject.SetActive(true);
        FindAwardRoom();
        AwardRoom.transform.GetChild(2).gameObject.SetActive(true);
        GameManager.Instance.RigisterFinalRoom(EndRoom.gameObject);
        GameManager.Instance.RigisterAwardRoom(AwardRoom);
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
                 lastdirection=Direction.UP;
                 break;
            case Direction.Down:
                GeneratorPoint.transform.position += new Vector3 (0,-Yoffset,0);
                lastdirection=Direction.Down;
                 break;
            case Direction.Left:
                 GeneratorPoint.transform.position += new Vector3 (-Xoffset,0,0);
                 lastdirection=Direction.Left;
                 break;
            case Direction.Right:
                 GeneratorPoint.transform.position += new Vector3 (Xoffset,0,0);
                 lastdirection=Direction.Right;
                 break;
        }
        } while (Physics2D.OverlapCircle(GeneratorPoint.position,0.2f,RoomLayer));
          
    
    }
    public void SetupRoom(RoomInformation newroom,Vector3 roomPostion)
    {
        if(Physics2D.OverlapCircle(roomPostion + new Vector3(0, Yoffset, 0), 0.2f, RoomLayer)!=Rooms[Rooms.Count-1])
        {
            newroom.hasUp = Physics2D.OverlapCircle(roomPostion + new Vector3(0, Yoffset, 0), 0.2f, RoomLayer);

        }
        else
        {
            Debug.Log("唯一");
        }
        
        if(Physics2D.OverlapCircle(roomPostion + new Vector3(0, -Yoffset, 0), 0.2f, RoomLayer)!=Rooms[Rooms.Count-1])
        {
            newroom.hasDown = Physics2D.OverlapCircle(roomPostion + new Vector3(0, -Yoffset, 0), 0.2f, RoomLayer);

        }
        else
        {
            Debug.Log("唯一");
        }
        
        if(Physics2D.OverlapCircle(roomPostion + new Vector3(-Xoffset, 0, 0), 0.2f, RoomLayer)!=Rooms[Rooms.Count-1])
        {
            newroom.hasLeft = Physics2D.OverlapCircle(roomPostion + new Vector3(-Xoffset, 0, 0), 0.2f, RoomLayer);

        }
        else
        {
            Debug.Log("唯一");
        }
        
        if(Physics2D.OverlapCircle(roomPostion + new Vector3(Xoffset, 0, 0), 0.2f, RoomLayer)!=Rooms[Rooms.Count-1])
        {
            newroom.hasRight = Physics2D.OverlapCircle(roomPostion + new Vector3(Xoffset, 0, 0), 0.2f, RoomLayer);

        }
        else
        {
            Debug.Log("唯一");
        }
        
        
        newroom.UpdateRoom();

    }
    public void FirstRoomSetup()
    {
        GeneratorPoint.transform.position=Rooms[0].transform.position;
        ChangePostionPos();
        Rooms.Add(Instantiate(RoomPrefab,GeneratorPoint.transform.position,Quaternion.identity).GetComponent<RoomInformation>());
        Rooms[Rooms.Count-1].transform.SetParent(Map.transform);
        GameManager.Instance.RigisterRooms(Rooms[Rooms.Count-1].gameObject);
        Rooms[Rooms.Count-1].transform.GetChild(0).gameObject.GetComponent<Image>().color=StartColor;
        Nowroom=Rooms[Rooms.Count-1];
        GameManager.Instance.RigisterNowRoom(Nowroom.gameObject);
        GameManager.Instance.RigisterOriginRoom(Rooms[Rooms.Count-1].gameObject);
        Nowroom.Target.SetActive(true);
        EndRoom=Rooms[Rooms.Count-1].gameObject;
        switch(lastdirection)
        {
           case Direction.UP:
                Nowroom.hasDown=true;
                Physics2D.OverlapCircle(Nowroom.transform.position + new Vector3(0, -Yoffset, 0), 0.2f, RoomLayer).GetComponent<RoomInformation>().hasUp=true;
                 break;
            case Direction.Down:
                Physics2D.OverlapCircle(Nowroom.transform.position + new Vector3(0, Yoffset, 0), 0.2f, RoomLayer).GetComponent<RoomInformation>().hasDown=true; 
                Nowroom.hasUp=true;
                 break;
            case Direction.Left:
                Nowroom.hasRight=true;
                Physics2D.OverlapCircle(Nowroom.transform.position + new Vector3(Xoffset, 0, 0), 0.2f, RoomLayer).GetComponent<RoomInformation>().hasLeft=true;
                 break;
            case Direction.Right:
                Nowroom.hasLeft=true;
                Physics2D.OverlapCircle(Nowroom.transform.position + new Vector3(-Xoffset, 0, 0), 0.2f, RoomLayer).GetComponent<RoomInformation>().hasRight=true;
                 break;
        }

       /*List<bool> trueDoors = new List<bool>();

        // 将为真的布尔值添加到列表中
        if (Rooms[0].hasUp) trueDoors.Add(Rooms[0].hasUp);
        if (Rooms[0].hasDown) trueDoors.Add(Rooms[0].hasDown);
        if (Rooms[0].hasLeft) trueDoors.Add(Rooms[0].hasLeft);
        if (Rooms[0].hasRight) trueDoors.Add(Rooms[0].hasRight);

        // 如果列表为空，则退出
        if (trueDoors.Count == 0)
        {
            Debug.Log("没有为真的布尔值可供选择！");
            return;
        }
        
        for(int i=0;i<trueDoors.Count;i++)
        {
           
                trueDoors[i]=false;
                 Debug.Log("为假！");
           
        }
        int randomIndex = Random.Range(0, trueDoors.Count);

        // 将选定的布尔值设置为真
        trueDoors[randomIndex] = true;
        Rooms[0].UpdateRoom();*/
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
    private void FindAwardRoom()
    {
        while (AwardRoom==null)
        {
                 RoomInformation random = Rooms[Random.Range(0, Rooms.Count)];
                 if(random!=FirstRoom&&random!=EndRoom)
                 {
                    AwardRoom=random.gameObject;
                 }
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
                 Nowroom=Physics2D.OverlapCircle(Nowroom.gameObject.transform.position+ new Vector3(0, Yoffset, 0), 0.2f, RoomLayer)?.gameObject.GetComponent<RoomInformation>();
                 Nowroom.initializatioonPosition=RoomInformation.InitializatioonPosition.Down;
                 GameManager.Instance.RigisterNowRoom(Nowroom.gameObject);
                 test=Nowroom;
                 break;
            case GameEventSystem.Direction.Down:
                 Nowroom=Physics2D.OverlapCircle(Nowroom.gameObject.transform.position+ new Vector3(0, -Yoffset, 0), 0.2f, RoomLayer)?.gameObject.GetComponent<RoomInformation>();
                Nowroom.initializatioonPosition=RoomInformation.InitializatioonPosition.Up;
                 GameManager.Instance.RigisterNowRoom(Nowroom.gameObject);
                  test=Nowroom;
                 break;
            case GameEventSystem.Direction.Left:
                 Nowroom=Physics2D.OverlapCircle(Nowroom.gameObject.transform.position+ new Vector3(-Xoffset, 0, 0), 0.2f, RoomLayer)?.gameObject.GetComponent<RoomInformation>();
                Nowroom.initializatioonPosition=RoomInformation.InitializatioonPosition.Right;
                 GameManager.Instance.RigisterNowRoom(Nowroom.gameObject);
                  test=Nowroom;
                 break;
            case GameEventSystem.Direction.Right:
                 Nowroom=Physics2D.OverlapCircle(Nowroom.gameObject.transform.position+ new Vector3(Xoffset, 0, 0), 0.2f, RoomLayer)?.gameObject.GetComponent<RoomInformation>();
                Nowroom.initializatioonPosition=RoomInformation.InitializatioonPosition.Left;
                 GameManager.Instance.RigisterNowRoom(Nowroom.gameObject);
                  test=Nowroom;
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
    public void MapShow()
    {
        Vector3 MapOffest=Nowroom.gameObject.transform.position-mapLocation.transform.position;
        if(MapOffest.magnitude>1f)
        transform.position -= MapOffest*Time.deltaTime;
    }
  
}
