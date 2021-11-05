using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {


    public Spell spell;
    private Image image;
    private AbilityUI ui;

    void Start() {
        image = GetComponent<Image>();
        ui = GetComponentInParent<AbilityUI>();

        if(spell != null) {
            image.sprite = spell.icon;
        }
    }
    
    public void SetIsChosen() {
        if (SpellManager.instance.currentSpell == spell && spell != null) {
            image.color = Color.red;
        }
        else if(SpellManager.instance.currentSpell != spell){
            image.color = Color.white;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if(spell != null) {
            Tooltip.ShowTooltip_static(spell._name + "\nDamage: " + spell.damageAmp + "\nMana Cost: " + spell.manaCost);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        Tooltip.HideTooltip_static();
    }

    public void OnPointerClick(PointerEventData eventData) {
        SpellManager.instance.SetSpell(spell);
        ui.SetChosenSpell();
    }
}
