using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    
    public Spell spell;
    private Image image;
    

    void Start() {
        image = GetComponent<Image>();
        
        if(spell != null) {
            image.sprite = spell.icon;
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
    }
}
