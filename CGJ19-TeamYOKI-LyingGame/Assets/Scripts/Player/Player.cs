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
    public GameObject playerBulletRailGun;
    public GameObject playerGrenade;
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
    const float MACHINEGUN_BULLETS_LIFE_TIME = .75f;

    public int ammo = 5;
    public int fullAmmo = 5;
    public float reloadWeaponTime = 3f;

    private bool isMachinegunAttacking = false;
    private bool isWeaponReloading = false;

    public enum Ability
    {
        Grenade,
    }
    public Ability myAbility = Ability.Grenade;

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
        CheckForAbilityUse();
    }




    // ===OTHER FUNCTIONS===

    void CheckForAbilityUse()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector2 shootDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            if (myAbility == Ability.Grenade)
            {
                GrenadeThrow(shootDirection);
                return;
            }
        }
    }

    void GrenadeThrow(Vector2 _dir)
    {
        GameObject _grenade = Instantiate(playerGrenade, (new Vector3(_dir.x, _dir.y, 0) * 1.5f + transform.position), Quaternion.identity);
        Grenade _grenadeProp = _grenade.GetComponent<Grenade>();
        _grenadeProp.targetDir = _dir.normalized * bulletSpeed;
        GameManager.instance.ShakeCamera(2f, 5);
    }

    void CheckForAttack()
    {
        if (isWeaponReloading) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            isWeaponReloading = true;
            StartCoroutine(ReloadingWeapon());
        }

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
            if (myWeapon == Weapon.RailGun)
            {
                RailgunAttack(shootDirection);
                return;
            }
        }
        if (Input.GetButton("Shoot"))
        {
            if (myWeapon == Weapon.MachineGun)
            {
                Vector2 shootDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                MachinegunAttack(shootDirection);
                return;
            }
        }
    }
    
    void DecreaseAmmo()
    {
        ammo--;
        if (ammo == 0)
        {
            isWeaponReloading = true;
            StartCoroutine(ReloadingWeapon());
        }
    }

    IEnumerator ReloadingWeapon()
    {
        yield return new WaitForSeconds(reloadWeaponTime);
        ammo = fullAmmo;
        isWeaponReloading = false;
    }

    void PistolAttack(Vector2 _dir)
    {
        DecreaseAmmo();
        GameObject _bullet = Instantiate(playerBullet, (new Vector3(_dir.x, _dir.y, 0)*1.5f + transform.position), Quaternion.identity);
        Bullet _bulletProp = _bullet.GetComponent<Bullet>();
        _bulletProp.bulletLifeTime = PISTOL_BULLETS_LIFE_TIME;
        _bulletProp.targetDir = _dir.normalized * bulletSpeed;
        GameManager.instance.ShakeCamera(2f, 5);
    }
    void ShotgunAttack(Vector2 _dir)
    {
        DecreaseAmmo();
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
    void MachinegunAttack(Vector2 _dir)
    {
        if (!isMachinegunAttacking)
        {
            DecreaseAmmo();
            StartCoroutine(MachinegunAttacking(_dir));
            GameManager.instance.ShakeCamera(2f, 5);
        }
    }

    IEnumerator MachinegunAttacking(Vector2 _dir)
    {
        isMachinegunAttacking = true;
        GameObject _bullet = Instantiate(playerBullet, (new Vector3(_dir.x, _dir.y, 0) * 1.5f + transform.position), Quaternion.identity);
        Bullet _bulletProp = _bullet.GetComponent<Bullet>();
        _bulletProp.bulletLifeTime = MACHINEGUN_BULLETS_LIFE_TIME;
        _bulletProp.targetDir = _dir.normalized * bulletSpeed;
        yield return null;
        _bullet = Instantiate(playerBullet, (new Vector3(_dir.x, _dir.y, 0) * 1.5f + transform.position), Quaternion.identity);
        _bulletProp = _bullet.GetComponent<Bullet>();
        _bulletProp.bulletLifeTime = MACHINEGUN_BULLETS_LIFE_TIME;
        _bulletProp.targetDir = _dir.normalized * bulletSpeed;
        yield return null;
        _bullet = Instantiate(playerBullet, (new Vector3(_dir.x, _dir.y, 0) * 1.5f + transform.position), Quaternion.identity);
        _bulletProp = _bullet.GetComponent<Bullet>();
        _bulletProp.bulletLifeTime = MACHINEGUN_BULLETS_LIFE_TIME;
        _bulletProp.targetDir = _dir.normalized * bulletSpeed;
        yield return new WaitForSeconds(.3f);
        isMachinegunAttacking = false;
    }

    void RailgunAttack(Vector2 _dir)
    {
        DecreaseAmmo();
        GameObject _bullet = Instantiate(playerBulletRailGun, (new Vector3(_dir.x, _dir.y, 0) * 1.5f + transform.position), Quaternion.identity);
        Bullet _bulletProp = _bullet.GetComponent<Bullet>();
        _bulletProp.bulletLifeTime = PISTOL_BULLETS_LIFE_TIME;
        _bulletProp.targetDir = _dir.normalized * bulletSpeed * 3;
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
