using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AmmoSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    public Ammo currentAmmo;
    public Image icon;
    public Text amountText;


    public void Start() {
        if (currentAmmo == null) {
            ClearSlot();
        }
        else if (currentAmmo != null) {
            EquipInSlot(currentAmmo);
        }
    }

    public void Update() {
        if (amountText.enabled) {
            amountText.text = currentAmmo.amount.ToString();
        }
    }

    public void EquipInSlot(Ammo newAmmo) {
        currentAmmo = newAmmo;

        icon.sprite = currentAmmo.icon;
        icon.enabled = true;

        amountText.enabled = true;
        
    }

    public void ClearSlot() {
        currentAmmo = null;

        icon.sprite = null;
        icon.enabled = false;

        amountText.enabled = false;
    }





    public void OnPointerEnter(PointerEventData eventData) {
        if (currentAmmo != null) {
            Tooltip.ShowTooltip_static(currentAmmo._name + " (" + currentAmmo.amount + ")");
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        Tooltip.HideTooltip_static();
    }


    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            if (currentAmmo != null) {
               EquipmentManager.instance.UnequipAmmo();
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right) {

        }
    }
}
