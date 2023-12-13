using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public Texture2D defaultCursor;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(defaultCursor, new Vector2(0.5f, 0.5f), CursorMode.Auto);
    }

}
