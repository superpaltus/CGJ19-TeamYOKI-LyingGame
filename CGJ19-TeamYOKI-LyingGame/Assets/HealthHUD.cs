using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
    public Sprite[] healthSprites;


    void Start()
    {
        
    }

    
    void Update()
    {
        GetComponent<Image>().sprite = healthSprites[Player.health];
    }
}
