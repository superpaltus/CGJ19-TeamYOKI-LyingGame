using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            WaveManager.instance.ActivateGenerator();
            Destroy(gameObject);    // temporary, here will be generator activate animation or smth like this            
        }
    }
}
