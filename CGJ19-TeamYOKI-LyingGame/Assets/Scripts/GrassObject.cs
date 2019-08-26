﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassObject : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player" || collision.transform.tag == "Bullet")
        {
            GetComponent<Animator>().SetTrigger("Shake");
            AudioManager.instance.Play("GrassShake");
        }
        
    }

}
