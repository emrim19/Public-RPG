using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPickup : Interactable
{
    public Item item;

    public void Start() {
        item = ScriptableObject.Instantiate(item);
        item.name = item.name.Replace("(Clone)", "");
    }


    protected override void Interact() {
        base.Interact();
        PickUp();
    }

    private void PickUp() {
        bool wasPickedUp = Inventory.instance.AddItem(item);
        if (wasPickedUp) {
            Destroy(gameObject);
            Tooltip.HideTooltip_static();
        }
    }

    private void OnMouseOver() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }
        Tooltip.ShowTooltip_static(item._name);
    }

    private void OnMouseExit() {
        Tooltip.HideTooltip_static();
    }

}
