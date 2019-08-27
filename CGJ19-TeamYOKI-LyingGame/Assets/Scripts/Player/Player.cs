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
    [Range(0f,1f)]
    public float bulletSpeed = 0.5f;

    float inputX, inputY;
    float lastMoveX, lastMoveY;
    bool moving = false;

    Animator playerAnim;
    Rigidbody2D playerRB;

    public enum Weapon
    {
        Pistol,
        Shotgun,
        MachineGun,
        RailGun,
    }
    public Weapon myWeapon = Weapon.Pistol;

    //const bulletLifeTime values for different types of weapon
    const float PISTOL_BULLETS_LIFE_TIME = 3f;
    const float SHOTGUN_BULLETS_LIFE_TIME = .3f;
    const float RAILGUN_BULLETS_LIFE_TIME = .3f;
    const float MACHINEGUN_BULLETS_LIFE_TIME = 2f;



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
            if (myWeapon == Weapon.Pistol)
            {
                PistolAttack(shootDirection);
                return;
            }
            if (myWeapon == Weapon.Shotgun)
            {
                ShotgunAttack(shootDirection);
                return;
            }
            if (myWeapon == Weapon.MachineGun)
            {
                PistolAttack(shootDirection);
                return;
            }
            if (myWeapon == Weapon.RailGun)
            {
                PistolAttack(shootDirection);
                return;
            }
        }
    }

    void PistolAttack(Vector2 _dir)
    {
        GameObject _bullet = Instantiate(playerBullet, (new Vector3(_dir.x, _dir.y, 0)*1.5f + transform.position), Quaternion.identity);
        Bullet _bulletProp = _bullet.GetComponent<Bullet>();
        _bulletProp.bulletLifeTime = PISTOL_BULLETS_LIFE_TIME;
        _bulletProp.targetDir = _dir.normalized * bulletSpeed;
        GameManager.instance.ShakeCamera(2f, 5);
    }
    void ShotgunAttack(Vector2 _dir)
    {
        for (int i = 0; i < 5; i++)
        {
            Vector2 offset = new Vector2(Random.Range(-.2f, .2f), Random.Range(-.2f, .2f));
            GameObject _bullet = Instantiate(playerBullet, (new Vector3(_dir.x, _dir.y, 0) * 1.5f + transform.position), Quaternion.identity);
            Bullet _bulletProp = _bullet.GetComponent<Bullet>();
            _bulletProp.bulletLifeTime = SHOTGUN_BULLETS_LIFE_TIME;
            _bulletProp.targetDir = (_dir.normalized + offset) * bulletSpeed;
            GameManager.instance.ShakeCamera(2f, 5);
        }
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
