using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MineRock : Interactable
{
    public string rockName;
    public int maxDurability;
    public int durability;
    public Item rock;

    public void Awake() {
        actionAnimation = ActionAnimation.MineRock;
        timer = Random.Range(minTimer, maxTimer);
    }

    protected override void Interact() {
        base.Interact();
        isDoingAction = true;
    }

    public override void DoingAction() {
        base.DoingAction();

        if (player.CheckWeaponEquiped() != null) {
            if (player.CheckWeaponEquiped().weaponType == WeaponType.Pickaxe && durability > 0) {
                if (timer > 0) {
                    timer -= Time.deltaTime * player.CheckWeaponEquiped().ConvertTierToNumber();
                    if (timer <= 0) {
                        GetMineral();
                        timer = Random.Range(minTimer, maxTimer);
                    }
                }
            }
        }
        if (durability <= 0) {
            hasInteracted = false;
            player.RemoveFocus();
            Tooltip.HideTooltip_static();
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    private void GetMineral() {
        SpawnGameObject(rock.prefab);
        durability--;
        player.weapon.LoseDurability(5);
    }

    private void OnMouseOver() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }
        Tooltip.ShowTooltip_static(rockName);

        //MenuBar BAR
        if (Input.GetMouseButtonDown(1)) {
            MenuBar.SetTarget_static(this);
        }
    }

    private void OnMouseExit() {
        Tooltip.HideTooltip_static();
    }
}
