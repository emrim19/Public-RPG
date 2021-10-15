using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class ChestData {

    public int[] itemsPerChest;
    public string[] itemPaths;
    public int[] itemAmounts;
    public int[] equipmentDurability;

    public ChestData(WorldManager worldManager) {
        worldManager.SetChestsInWorld();
        List<Item> items = new List<Item>();

        itemsPerChest = new int[worldManager.chestsInWorld.Length];

        for(int i = 0; i < worldManager.chestsInWorld.Length; i++) {
            itemsPerChest[i] = worldManager.GetChest(i).chestItems.Count;

            for(int j = 0; j < worldManager.GetChest(i).chestItems.Count; j++) {
                items.Add(worldManager.GetChest(i).chestItems[j]);
            }
        }

        itemPaths = new string[items.Count];
        itemAmounts = new int[items.Count];
        equipmentDurability = new int[items.Count];

        for(int i = 0; i < items.Count; i++) {
            itemPaths[i] = items[i].GetPath();
            itemAmounts[i] = items[i].amount;

            if (items[i].type.ToString() == "Equipment") {
                Equipment newEquip = items[i] as Equipment;

                equipmentDurability[i] = newEquip.currentDurability;
            }
        }
    }
}

