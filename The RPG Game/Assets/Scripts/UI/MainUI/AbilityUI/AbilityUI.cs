using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : UI
{


    public Transform parentTransform;
    public AbilityButton[] abilityButtons;


    private void Start() {
        abilityButtons = parentTransform.GetComponentsInChildren<AbilityButton>();
    }

    public void SetChosenSpell() {
        for(int i = 0; i < abilityButtons.Length; i++) {
            abilityButtons[i].SetIsChosen();
        }
    }

}
