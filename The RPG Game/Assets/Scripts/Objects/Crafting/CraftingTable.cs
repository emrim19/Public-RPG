using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : Construction
{
    public AdvancedCraftingUI advancedCraftingUI;

    public void Start() {
        advancedCraftingUI = GameObject.Find("AdvancedCraftingUI").GetComponent<AdvancedCraftingUI>();
    }

    protected override void Interact() {
        base.Interact();
        advancedCraftingUI.ToggleUI();
    }


}
