using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public float bulletLifeTime = 3f;

    public Vector2 targetDir;

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().AddForce(targetDir * 60, ForceMode2D.Impulse);
        BulletLifeTimeDecrease();
    }

    private void BulletLifeTimeDecrease()
    {
        bulletLifeTime -= Time.deltaTime;
        if (bulletLifeTime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            Destroy(gameObject);
        } else if(collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            collision.transform.GetComponent<EntityHurtable>().OnEntityHit();
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            collision.transform.GetComponent<EntityHurtable>().OnEntityHit();
            Destroy(gameObject);
        }
    }
}
