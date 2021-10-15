using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : UI
{

    private Inventory inventory;
    public Transform itemsParent;
    public InventorySlot[] slots;

    public UI craftingUI;
    public Button craftingButton;




    // Start is called before the first frame update
    void Start() {
        inventory = Inventory.instance;
        craftingUI = GameObject.Find("CraftingUI").GetComponent<UI>();
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        craftingButton.onClick.AddListener(ToggleCrafting);

        UpdateUI();
    } 


    private void UpdateUI() {
        for (int i = 0; i < slots.Length; i++) {
            if (i < inventory.items.Count) {
                slots[i].Additem(inventory.items[i]);
            }
            else {
                slots[i].ClearSlot();
            }
        }
    }


    public void ToggleCrafting() {
        craftingUI.ToggleUI();
    }
}
