using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChestSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    public Item item;
    public Text itemCountText;
    public Image icon;
    public Slider slider;
    
    public Chest chest;
    public ChestUI ui;

    public void Start() {
        ui = GameObject.Find("ChestUI").GetComponent<ChestUI>();
    }

    public void Update() {
        chest = ui.chest;
    }

    public void Additem(Item newItem) {
        if (newItem.stackable) {
            item = newItem;

            icon.sprite = item.icon;
            icon.enabled = true;



            itemCountText.enabled = true;
            itemCountText.text = item.amount.ToString();
        }
        else if (!newItem.stackable) {
            item = newItem;

            icon.sprite = item.icon;
            icon.enabled = true;

            itemCountText.enabled = false;

        }
        if (item.GetType().ToString() == "Weapon" || item.GetType().ToString() == "Equipment") {
            slider.gameObject.SetActive(true);
            UpdateSlider();
        }
        else {
            slider.gameObject.SetActive(false);
        }
    }

    public void ClearSlot() {
        item = null;

        icon.sprite = null;
        icon.enabled = false;

        itemCountText.enabled = false;

        slider.gameObject.SetActive(false);
    }

    public void UpdateSlider() {
        Equipment equip = item as Equipment;
        slider.maxValue = equip.maxDurability;
        slider.value = equip.currentDurability;
    }



    public void RemoveOneItem() {
        if(item != null) {
            chest.RemoveItemFromChest(item);
        }
    }

    public void RemoveAllItems() {
        if(item != null) {
            chest.RemoveAllItemsFromChest(item);
        }
    }



    public void OnPointerEnter(PointerEventData eventData) {
        if (item != null) {
            if (item.type.ToString() == "Weapon") {
                Weapon equip = item as Weapon;
                Tooltip.ShowTooltip_static(equip._name + " (" + equip.currentDurability + ")" + "\n" + equip.description);
            }
            else if (item.type.ToString() == "Equipment") {
                Equipment equip = item as Equipment;
                Tooltip.ShowTooltip_static(equip._name + " (" + equip.currentDurability + ")" + "\n" + equip.description); ;
            }
            else {
                Tooltip.ShowTooltip_static(item._name + "\n" + item.description);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        Tooltip.HideTooltip_static();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            RemoveOneItem();
        }
        else if (eventData.button == PointerEventData.InputButton.Right) {
            RemoveAllItems();
        }
    }
}
