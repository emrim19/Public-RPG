using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FishingInLake : Interactable
{
    public string lakeName;
    public int amount, maxAmount;
    public float cooldown = 60;
    public Food[] fish;
    
    void Awake() {
        timer = Random.Range(minTimer, maxTimer);
        amount = maxAmount;
        InvokeRepeating("ReplenishFish", 0, 1f);
    }



    protected override void Interact() {
        base.Interact();
        isDoingAction = true;
    }

    public override void DoingAction() {
        base.DoingAction();

        if(player.CheckWeaponEquiped() != null) {
            if (player.CheckWeaponEquiped().weaponType == WeaponType.FishingPole && amount > 0) {
                if (timer > 0) {
                    timer -= Time.deltaTime;
                    if (timer <= 0) {
                        GetFish();
                        timer = Random.Range(minTimer, maxTimer);
                    }
                }
            }
            else if(amount == 0) {
                hasInteracted = false;
                player.RemoveFocus();
            }
        }
    }


    private void GetFish() {
        Food theFood = ScriptableObject.Instantiate(fish[0]);
        theFood.amount = 1;
        Inventory.instance.AddItem(theFood);
        player.weapon.LoseDurability(1);
        amount--;
    }


    private void ReplenishFish() {
        if(amount < maxAmount) {
            if(cooldown >= 0) {
                cooldown -= 1;
                if(cooldown < 0) {
                    amount++;
                    cooldown = 60;
                }
            }
        }
    }


    private void OnMouseOver() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }
        Tooltip.ShowTooltip_static(lakeName + " (" + amount + ")");

        //MenuBar BAR
        if (Input.GetMouseButtonDown(1)) {
            MenuBar.SetTarget_static(this);
        }
    }

    private void OnMouseExit() {
        Tooltip.HideTooltip_static();
    }
}
