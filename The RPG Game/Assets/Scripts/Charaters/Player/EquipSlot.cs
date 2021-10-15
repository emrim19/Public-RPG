using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    public Equipment currentEquipt;
    public Image icon;
    public Slider slider;

    public void Update() {
        UpdateSlider();
    }

    public void Start() {
        if(currentEquipt == null) {
            ClearSlot();
        }
        else if(currentEquipt != null) {
            EquipInSlot(currentEquipt);
        }
    }

    public void EquipInSlot(Equipment newItem) {
        currentEquipt = newItem;

        icon.sprite = currentEquipt.icon;
        icon.enabled = true;

        slider.gameObject.SetActive(true);
        UpdateSlider();
    }

    public void ClearSlot() {
        currentEquipt = null;

        icon.sprite = null;
        icon.enabled = false;

        slider.gameObject.SetActive(false);
    }

    private string StatString(int stat) {
        if (stat < 0) {
            return " " + stat;
        }
        else if (stat > 0) {
            return "+" + stat;
        }
        else {
            return "  " + stat;
        }
    }

    public void UpdateSlider() {
        if(currentEquipt != null) {
            slider.maxValue = currentEquipt.maxDurability;
            slider.value = currentEquipt.currentDurability;
        }
    }

    public int CalculateDurabilityPercentage() {
        int result = 0;

        if(currentEquipt != null) {
            float current = currentEquipt.currentDurability;
            float max = currentEquipt.maxDurability;

            float floatResult = Mathf.Floor((current / max) * 100);

            result = (int)floatResult;
        }


        return result;
    }


    public void OnPointerEnter(PointerEventData eventData) {
        if (currentEquipt != null) {
            Tooltip.ShowTooltip_static(currentEquipt._name + " (" + CalculateDurabilityPercentage() + ")");
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        Tooltip.HideTooltip_static();
    }


    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            if(currentEquipt != null) {
                int equipIndex = (int)currentEquipt.equipSlot;
                EquipmentManager.instance.Unequip(equipIndex);
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right) {

        }
    }
}
