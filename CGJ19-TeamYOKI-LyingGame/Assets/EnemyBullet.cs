using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 1;

    void Start()
    {

    }


    void Update()
    {
        
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector3.forward * speed * 40, ForceMode2D.Impulse);

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            Destroy(gameObject);
        }
    }

}
