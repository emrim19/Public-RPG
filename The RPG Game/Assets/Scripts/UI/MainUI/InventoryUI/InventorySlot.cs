using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IEndDragHandler {
    
    public Item item;
    public Text itemCountText;
    public Image icon;
    public Slider slider;

    public bool isDragging;

    public void Additem(Item newItem) {
        if (newItem.stackable) {
            item = newItem;

            icon.sprite = item.icon;
            icon.enabled = true;
            
            itemCountText.enabled = true;
            itemCountText.text = item.amount.ToString();

            slider.gameObject.SetActive(false);
        }
        else if (!newItem.stackable) {
            item = newItem;

            icon.sprite = item.icon;
            icon.enabled = true;

            itemCountText.enabled = false;

            if (item.GetType().ToString() == "Weapon" || item.GetType().ToString() == "Equipment") {
                slider.gameObject.SetActive(true);
                UpdateSlider();
            }
            else {
                slider.gameObject.SetActive(false);
            }
        }
    }

    public void ClearSlot() {
        item = null;

        icon.sprite = null;
        icon.enabled = false;

        itemCountText.enabled = false;

        slider.gameObject.SetActive(false);
    }

    public void UseItem() {
        if (item != null && !isDragging) {
            item.Use();
        }
    }
    
    public void RemoveItem() {
        if (item != null) {
            if (item.GetType().ToString() == "Weapon") {
                Weapon equip = item as Weapon;
                if (equip.currentDurability< equip.maxDurability) {
                    equip.DropItemOnGround();
                }
                else if (equip.currentDurability == equip.maxDurability) {
                    equip.DropItemOnGround();
                }
            }
            else if (item.GetType().ToString() == "Equipment") {
                Equipment equip = item as Equipment;
                if (equip.currentDurability < equip.maxDurability) {
                    equip.DropItemOnGround();
                }
                else if (equip.currentDurability == equip.maxDurability) {
                    equip.DropItemOnGround();
                }
            }
            else {
                item.DropItemOnGround();
            }
        }
    }

    public void UpdateSlider() {
        Equipment equip = item as Equipment;
        slider.maxValue = equip.maxDurability;
        slider.value = equip.currentDurability;
    }

    public int CalculateDurabilityPercentage(Equipment equip) {
        int result = 0;

        if (equip != null) {
            float current = equip.currentDurability;
            float max = equip.maxDurability;

            float floatResult = Mathf.Floor((current / max) * 100);

            result = (int)floatResult;
        }
        return result;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (item != null) {
            if (item.type.ToString() == "Weapon") {
                Weapon equip = item as Weapon;
                Tooltip.ShowTooltip_static(equip._name + " (" + CalculateDurabilityPercentage(equip) + ")" + "\n" + equip.description);
            }
            else if (item.type.ToString() == "Equipment") {
                Equipment equip = item as Equipment;
                Tooltip.ShowTooltip_static(equip._name + " (" + CalculateDurabilityPercentage(equip) + ")" + "\n" + equip.description);
            }
            else if(item.type.ToString() == "Food") {
                Food food = item as Food;
                Tooltip.ShowTooltip_static(food._name + "\n" + food.description + "\nFood: " + food.foodValue + "\nHealth: " + food.healthValue + "\nStamina: " + food.staminaValue);
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
            UseItem();
        }
        else if (eventData.button == PointerEventData.InputButton.Right) {
            RemoveItem();
        }
    }

    public void OnDrag(PointerEventData eventData) {
        icon.transform.position = Input.mousePosition;
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData) {
        icon.transform.localPosition = Vector3.zero;
        isDragging = false;
        
    }

}
