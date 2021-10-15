using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public string[] itemPaths;
    public int[] itemAmounts;
    public int[] equipmentDurability;

    public InventoryData(Inventory inventory) {
        itemPaths = new string[inventory.items.Count];
        itemAmounts = new int[inventory.items.Count];
        equipmentDurability = new int[inventory.items.Count];

        for (int i = 0; i < inventory.items.Count; i++) {
            itemPaths[i] = inventory.items[i].GetPath();
            itemAmounts[i] = inventory.items[i].amount;

            if(inventory.items[i].type.ToString() == "Equipment") {
                Equipment newEquip = inventory.items[i] as Equipment;

                equipmentDurability[i] = newEquip.currentDurability;
            }
        }
    }
    
}
