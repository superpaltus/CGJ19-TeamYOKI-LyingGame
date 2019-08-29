using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    public GameObject target;
    RaycastHit2D hit;

    public GameObject bullet;

    // Angular speed in radians per sec.
    public float speed;

    public bool playerSight = false;

    public float shotTimer;
    public float force = 300.0f;




    void Start()
    {
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void Update()
    {
        if(playerSight)
        {
            shotTimer += Time.deltaTime;
            if(shotTimer >= 0.25f)
            {
                Shoot();
                shotTimer = 0;
            } 
        } else
        {
            shotTimer = 0;
        }

    }

    void Shoot() {
        Vector3 targ = target.transform.position;
        targ.x = targ.x - transform.position.x;
        targ.y = targ.y - transform.position.y;
        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        GameObject _bul = Instantiate(bullet, transform.position, rotation);
        Vector2 lookDirection = target.gameObject.transform.position - transform.position;
        lookDirection.Normalize();
        _bul.gameObject.GetComponent<Rigidbody2D>().AddForce(lookDirection * force);
    }


    void FixedUpdate()
    {
        hit = Physics2D.Raycast(transform.position, (target.transform.position - transform.position).normalized, 10.0f);

        if (hit != false && hit.transform.tag == "Player")
        {
            playerSight = true;
            Vector3 vectorToTarget = target.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
        } else
        {
            playerSight = false;
        }
    }
}
