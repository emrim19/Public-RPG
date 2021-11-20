using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    public Player player;
    public Ammo currentAmmo;
    public Spell currentSpell;
    public Spell tempSpell;


    public void Update() {
        currentAmmo = EquipmentManager.instance.currentAmmo;
        currentSpell = SpellManager.instance.currentSpell;

        if(currentSpell != null) {
            if (tempSpell == null) {
                tempSpell = currentSpell;
            }
            if (tempSpell != currentSpell && !player.animator.GetCurrentAnimatorStateInfo(0).IsTag("Spellcast")) {
                tempSpell = currentSpell;
            }

        }
        if(currentSpell == null) {
            tempSpell = null;
        }
         

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
            if(tempSpell != null) {
                if (tempSpell == currentSpell) {
                    if (tempSpell.manaCost <= player.playerStats.stamina) {
                        if (SpellManager.instance.currentSpell != null) {
                            Projectile.SpawnProjectile(tempSpell.prefab);
                            //player.playerStats.LoseStamina(currentSpell.manaCost);
                        }
                    }
                }
            }
        }
    }

    



}
