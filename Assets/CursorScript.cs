using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<<< HEAD:Assets/GameSystem/Player/PlayerAttackNew.cs
public class PlayerAttackNew : MonoBehaviour
========
public class CursorScript : MonoBehaviour
>>>>>>>> 543ed09d369917412bf3d5975cf01dd60547454f:Assets/CursorScript.cs
{
    public Texture2D defaultCursor;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(defaultCursor, new Vector2(0.5f, 0.5f), CursorMode.Auto);
    }

}
