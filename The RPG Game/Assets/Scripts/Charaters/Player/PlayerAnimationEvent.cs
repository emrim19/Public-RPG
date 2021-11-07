using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    public Player player;
    public Ammo currentAmmo;
    public Spell currentSpell;


    public void Update() {
        currentAmmo = EquipmentManager.instance.currentAmmo;
        currentSpell = SpellManager.instance.currentSpell;
    }


    public void MeeleAttack() {
        if (player.focus != null && player.focus.GetComponent<Enemy>()) {
            
            CharacterStats targetStats = player.focus.GetComponent<CharacterStats>();
            targetStats.TakeDamage(player.playerStats.CalcMeeleDamage());

            if (player.CheckWeaponEquiped() != null) {
                player.CheckWeaponEquiped().LoseDurability(1);
            }
        }    
    }

    public void ShootArrow() {
        if (player.focus != null && player.focus.GetComponent<Enemy>()) {
            if(player.CheckWeaponEquiped() != null) {
                if(player.CheckWeaponEquiped().weaponType == WeaponType.Bow) {
                    if (currentAmmo != null) {
                        Projectile.SpawnProjectile(currentAmmo.prefab);
                        EquipmentManager.instance.RemoveAmmo();
                    }

                    if (player.CheckWeaponEquiped() != null) {
                        player.CheckWeaponEquiped().LoseDurability(1);
                    }
                }
            }
        }
    }

    public void Spellcast1() {
        if (player.focus != null && player.focus.GetComponent<Enemy>()) {
            if(currentSpell != null) {
                if (currentSpell.manaCost <= player.playerStats.stamina) {
                    if (SpellManager.instance.currentSpell != null) {
                        Projectile.SpawnProjectile(currentSpell.prefab);
                        player.playerStats.LoseStamina(currentSpell.manaCost);
                    }
                }
            }
        }
    }

    



}
