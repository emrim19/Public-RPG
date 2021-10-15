using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CutTree : Interactable
{
    public string treeName;
    public int maxDurability;
    public int durability;
    public Item wood;
    public GameObject stub;
    public Transform forestTransform;

    public void Awake() {
        forestTransform = GameObject.Find("Forest").GetComponent<Transform>();
        actionAnimation = ActionAnimation.CutWood;
        timer = Random.Range(minTimer, maxTimer);
        InvokeRepeating("Timber", 0, 0.01f);
    }

    protected override void Interact() {
        base.Interact();
        isDoingAction = true;
    }

    public override void DoingAction() {
        base.DoingAction();

        if (player.CheckWeaponEquiped() != null) {
            if (player.CheckWeaponEquiped().weaponType == WeaponType.Axe && durability > 0) {
                if (timer > 0) {
                    timer -= Time.deltaTime * player.CheckWeaponEquiped().ConvertTierToNumber();
                    if (timer <= 0) {
                        GetWood();
                        timer = Random.Range(minTimer, maxTimer);
                    }
                }
            }
        }
        if (durability <= 0) {
            hasInteracted = false;
            player.RemoveFocus();
            Tooltip.HideTooltip_static();

            GameObject theStub = Instantiate(stub, transform.position, transform.rotation, forestTransform);
            theStub.name = theStub.name.Replace("(Clone)", string.Empty);
        }
    }


    private void GetWood() {
        SpawnGameObject(wood.prefab);
        durability--;
        player.weapon.LoseDurability(5);
    }
    

    private void Timber() {
        if(durability <= 0) {
            if (gameObject.transform.eulerAngles.x < 70) {
                gameObject.transform.Rotate(0.5f, 0, 0);
            }
            else if(gameObject.transform.eulerAngles.x >= 70) {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }

    private void OnMouseOver() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }
        Tooltip.ShowTooltip_static(treeName);

        //MenuBar BAR
        if (Input.GetMouseButtonDown(1)) {
            MenuBar.SetTarget_static(this);
        }
    }

    private void OnMouseExit() {
        Tooltip.HideTooltip_static();
    }
}
