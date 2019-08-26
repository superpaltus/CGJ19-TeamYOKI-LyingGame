using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Gun : MonoBehaviour
{
    

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 normallizedPos = mousePos - new Vector2(transform.parent.position.x, transform.parent.position.y);
        normallizedPos = normallizedPos.normalized;
        Vector3 targetPos = (new Vector3(normallizedPos.x, normallizedPos.y + 0.5f, 0) / 4);
        transform.position = transform.parent.position + targetPos;

        GetComponent<Animator>().SetFloat("DirX", (targetPos).normalized.x);
        GetComponent<Animator>().SetFloat("DirY", (new Vector2(targetPos.x, targetPos.y - 0.1f)).normalized.y);
    }
}
