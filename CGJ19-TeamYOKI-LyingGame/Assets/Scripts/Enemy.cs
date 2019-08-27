using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // COMMENTED BECAUSE THIS DOES NOT WORK PROPERLY
    ////Private Variables
    //private GameObject player;
    //private float timeToAttack;
    //private LayerMask playerLayer;          //Player Layer == Player


    ////Public variables
    //[Range(.1f, 3f)]
    //public float allowedDis;                //How close the enemy can get

    //[Range(0f, 5f)]
    //public float speed = 3f;               

    //[Range(0.2f, 2f)]
    //public float damageFreq = 2f;           //How fast enemy can attack
    //public float damagePower;

    //[Range(.5f, 4f)]
    //public float attackRange;               //Range of it's attack



    //void Start()
    //{
    //    player = GameObject.FindWithTag("Player");   
    //    playerLayer = LayerMask.GetMask("Player");
    //}


    //void Update()
    //{
    //    canFollow();
    //}

    ////Follow logic
    //void canFollow ()
    //{
    //    if(Vector2.Distance(transform.position, player.transform.position) > allowedDis)
    //    {
    //        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    //    }

    //    else
    //    {
    //        attack();
    //    }
    //}

    //void attack()
    //{
    //    if(timeToAttack <= 0)
    //    {
    //        timeToAttack = damageFreq;
    //        Collider2D[] damage = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

    //        for (int i = 0; i < damage.Length; i++)
    //        {
    //            damage[i].GetComponent<Player>().TakeDamage(damagePower);
    //        }
    //    }

    //    else
    //    {
    //       timeToAttack -= Time.deltaTime;
    //    }

    //}

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);        
    //}
}
