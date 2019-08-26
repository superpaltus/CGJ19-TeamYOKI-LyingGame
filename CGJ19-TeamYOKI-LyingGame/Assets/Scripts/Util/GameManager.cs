using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Texture2D mouseCursorDefault;
    public Vector2 defaultHotspot;

    public float shakeTime = 0;
    public float lastShakeTime = 0;
    public Vector2 shakePoint;


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

    void Update()
    {
        if(shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
        }

        if(shakeTime > 0 && shakeTime < lastShakeTime/3)
        {
            Camera.main.transform.GetComponent<CameraScript>().offset = Vector2.Lerp(Camera.main.transform.GetComponent<CameraScript>().offset, Vector2.zero, shakeTime / lastShakeTime * 3);
        } else if (shakeTime > lastShakeTime / 3 && shakeTime < (lastShakeTime / 3) * 2)
        {
            Camera.main.transform.GetComponent<CameraScript>().offset = Vector2.Lerp(shakePoint, Camera.main.transform.GetComponent<CameraScript>().offset, shakeTime / lastShakeTime * 3);
        } else if (shakeTime > (lastShakeTime / 3) * 2 && shakeTime < lastShakeTime)
        {
            Camera.main.transform.GetComponent<CameraScript>().offset = Vector2.Lerp(shakePoint, Vector2.zero, shakeTime - lastShakeTime * 3);
        }
        
}


    public void ShakeCamera(float _amount, float _time, Vector2 _dir = new Vector2())
    {
        //if(_dir == Vector2.zero)
        //{
        //    _dir = new Vector2(Random.Range(-_amount, _amount), Random.Range(-_amount, _amount));
        //}
        //shakePoint = _dir * _amount;
        //lastShakeTime = _time;
        //shakeTime = _time;
        //Camera.main.transform.GetComponent<CameraScript>().offset = _dir;
        //Camera.main.transform.GetComponent<CameraScript>().offset = -_dir;
        //Camera.main.transform.GetComponent<CameraScript>().offset = Vector2.zero;

    }

}
