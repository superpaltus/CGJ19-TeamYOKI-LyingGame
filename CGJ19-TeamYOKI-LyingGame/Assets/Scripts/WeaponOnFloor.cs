using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOnFloor : MonoBehaviour
{
    public enum PickUpWeaponType
    {
        Pistol,
        Shotgun,
        Machinegun,
        Railgun,
    }
    public PickUpWeaponType thisPickUpWeaponType;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            switch (thisPickUpWeaponType)
            {
                case PickUpWeaponType.Pistol:
                    player.myWeapon = Player.Weapon.Pistol;
                    break;
                case PickUpWeaponType.Shotgun:
                    player.myWeapon = Player.Weapon.Shotgun;
                    break;
                case PickUpWeaponType.Machinegun:
                    player.myWeapon = Player.Weapon.MachineGun;
                    break;
                case PickUpWeaponType.Railgun:
                    player.myWeapon = Player.Weapon.RailGun;
                    break;
            }
        }

    }
}
