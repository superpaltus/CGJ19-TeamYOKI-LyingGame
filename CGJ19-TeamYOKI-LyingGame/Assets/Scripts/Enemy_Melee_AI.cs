using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Melee_AI : MonoBehaviour
{
    public Transform target;
    public float walkSpeed;
    public float runSpeed;
    public float viewRange;
    public int maxWalkDist;
    public LayerMask blockingLayer;


    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;

    Vector2 start;
    Vector2 end;

    float waitTime = 2f;
    static float elapsedTime = 0f;
    bool isMoving = false;
    static bool damageDone = false;
    static float cooldown = 0f;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();

        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (damageDone)
        {
            if(cooldown <= 0f)
                damageDone = false;
            else
                cooldown -= Time.deltaTime;
            return;
        }

        if (DetectPlayer())
        {
            Move();
        }
        else
        {
            Wander();
        }
    }

    bool DetectPlayer()
    {
        float dist = Vector3.Distance(target.position, transform.position);

        if (dist < 1f)
        {
            DoDamage();
            return false;
        }

        if (dist <= viewRange)
        {
            return true;
        }

        return false;
    }

    void Wander()
    {
        if(elapsedTime < waitTime)
        {
            elapsedTime += Time.deltaTime;
            return;
        }


        start = transform.position;

        if (isMoving == false)
        {
            boxCollider.enabled = false;

            RaycastHit2D hit;

            do
            {
                int xDir = Random.Range(-maxWalkDist, maxWalkDist);
                int yDir = Random.Range(-maxWalkDist, maxWalkDist);
                end = start + new Vector2(xDir, yDir);
                hit = Physics2D.Linecast(start, end, blockingLayer);
            }
            while (hit.transform != null);

            boxCollider.enabled = true;
        }

        isMoving = true;

        float sqrRemainingDistance = (start - end).sqrMagnitude;
        if (sqrRemainingDistance < float.Epsilon)
        {
            isMoving = false;
            elapsedTime = 0f;
            return;
        }

        Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, walkSpeed * Time.deltaTime);
        rb2D.MovePosition(newPosition);
    }

    void Move()
    {
        isMoving = false;
        elapsedTime = 0f;
        Vector3 newPosition = Vector3.MoveTowards(rb2D.position, target.position, runSpeed * Time.deltaTime);
        rb2D.MovePosition(newPosition);
    }

    void DoDamage()
    {
        target.GetComponent<Player>().ChangeHealth(-1);

        Vector2 moveDirection = transform.position - target.transform.position;
        target.GetComponent<Rigidbody2D>().AddForce(moveDirection.normalized * -10000f);

        damageDone = true;
        cooldown = 0.5f;
    }
}
