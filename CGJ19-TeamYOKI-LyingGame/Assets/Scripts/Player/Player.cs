using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{


    // ===VARIABLES===

    public float moveSpeed;

    public GameObject playerBullet;

    float inputX, inputY;
    float lastMoveX, lastMoveY;
    bool moving = false;

    Animator playerAnim;
    Rigidbody2D playerRB;


    // ===UNITY FUNCTIONS===

    void Start()
    {
        lastMoveX = 0;
        lastMoveY = -1;
        playerAnim = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        UpdateMovement();
        RigidMove();
        CheckForAttack();
    }




    // ===OTHER FUNCTIONS===


    void CheckForAttack()
    {
        if (Input.GetButtonDown("Shoot"))
        {
            Vector2 shootDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            Attack(shootDirection);
        }
    }

    void Attack(Vector2 _dir)
    {
        GameObject _bullet = Instantiate(playerBullet, (new Vector3(_dir.x, _dir.y, 0)*1.5f + transform.position), Quaternion.identity);
        _bullet.GetComponent<Bullet>().targetDir = _dir;
        GameManager.instance.ShakeCamera(2f, 5);
    }

    void UpdateMovement()
    {
        if (moving == true)
        {
            lastMoveX = inputX;
            lastMoveY = inputY;
        }

        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        if(inputX != 0 || inputY != 0)
        {
            moving = true;
        } else
        {
            moving = false;
        }
        playerAnim.SetBool("Moving", moving);
        playerAnim.SetFloat("MoveX", inputX);
        playerAnim.SetFloat("MoveY", inputY);
        playerAnim.SetFloat("LastMoveX", lastMoveX);
        playerAnim.SetFloat("LastMoveY", lastMoveY);

    }

    void RigidMove()
    {
        Vector2 moveForce = new Vector2(inputX, inputY);
        moveForce = moveForce.normalized;
        playerRB.AddForce(moveForce * 800 * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
    }
}
