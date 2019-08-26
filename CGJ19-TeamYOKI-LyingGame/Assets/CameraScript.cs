using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public bool followPlayer = true;
    public GameObject target;

    void LateUpdate()
    {
        if(followPlayer)
        {
            UpdateCameraPosition();
        }
        
    }

    void UpdateCameraPosition()
    {
        Vector2 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lerpedPos = Vector2.Lerp(target.transform.position, targetPos, 0.05f);
        transform.position = new Vector3(lerpedPos.x, lerpedPos.y, -10);
    }

}
