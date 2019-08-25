using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{


    // ===VARIABLES===

    public float moveSpeed;
    
    public float inputX, inputY;
    public float lastMoveX, lastMoveY;
    public bool moving = false;

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
    }




    // ===OTHER FUNCTIONS===


    void UpdateMovement()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
    }

    void RigidMove()
    {
        Vector2 moveForce = new Vector2(inputX, inputY);
        moveForce = moveForce.normalized;
        playerRB.AddForce(moveForce * 6 * moveSpeed, ForceMode2D.Impulse);
    }
}
