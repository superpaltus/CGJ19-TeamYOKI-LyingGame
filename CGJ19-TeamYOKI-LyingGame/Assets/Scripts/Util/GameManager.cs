using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Texture2D mouseCursorDefault;
    public Vector2 defaultHotspot;


    void Start()
    {
        Cursor.SetCursor(mouseCursorDefault, defaultHotspot, CursorMode.Auto);
    }


    void Awake()
    { 
        if(instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
