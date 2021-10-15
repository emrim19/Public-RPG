using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    public Player player;
    public Transform weaponTransform;

    public Ammo arrow;

    private void Start() {
        weaponTransform = GameObject.Find("PlayerRightHand").GetComponent<Transform>();
    }


    public void Attack() {
        if(player.focus != null) {
            player.combat.Attack(player.focus.GetComponent<CharacterStats>());
            if(player.CheckWeaponEquiped() != null) {
                player.CheckWeaponEquiped().LoseDurability(1);
            }
        }
    }

    public void ShootArrow() {
        if (player.focus != null) {
            if(player.CheckWeaponEquiped().weaponType == WeaponType.Bow) {
                Instantiate(arrow.ammoPrefab, weaponTransform.position, weaponTransform.rotation);
                player.focus.GetComponent<Enemy>().lookRadius = 20;
                EquipmentManager.instance.RemoveAmmo();

                if (player.CheckWeaponEquiped() != null) {
                    player.CheckWeaponEquiped().LoseDurability(1);
                }
            }
        }
    }



}
