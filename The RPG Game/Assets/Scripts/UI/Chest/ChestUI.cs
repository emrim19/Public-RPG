using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUI : UI
{

    public Player player;

    private Inventory inventory;
    public Chest chest;

    public Transform itemsParentInvent;
    public Transform itemsParentChest;

    public InventChestSlot[] inventSlots;
    public ChestSlot[] chestSlots;

    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        inventory = Inventory.instance;

        inventory.onItemChangedCallback += UpdateUI;

        inventSlots = itemsParentInvent.GetComponentsInChildren<InventChestSlot>();
        chestSlots = itemsParentChest.GetComponentsInChildren<ChestSlot>();

        UpdateUI();
    }

    
    public void Update() {
        FocusChest();
    }

    public void FocusChest() {
        if (player.focus != null) {
            if (player.focus.GetType().ToString() == "Chest") {
                chest = player.focus as Chest;
                UpdateUI();
            }
            else if (player.focus.GetType().ToString() != "Chest") {
                if (chest != null) {
                    DeactivateUI();
                    chest = null;
                }
            }
        }
        else if (player.focus == null) {
            if (chest != null) {
                DeactivateUI();
                chest = null;
            }
        }
    }
    
    private void UpdateUI() {
        if(chest != null) {
            for (int i = 0; i < chestSlots.Length; i++) {
                if (i < chest.chestItems.Count) {
                    chestSlots[i].Additem(chest.chestItems[i]);
                }
                else {
                    chestSlots[i].ClearSlot();
                }
            }

            for (int i = 0; i < inventSlots.Length; i++) {
                if (i < inventory.items.Count) {
                    inventSlots[i].Additem(inventory.items[i]);
                }
                else {
                    inventSlots[i].ClearSlot();
                }
            }
        }
        else if(chest == null) {
            DeactivateUI();
        }


    }
}
