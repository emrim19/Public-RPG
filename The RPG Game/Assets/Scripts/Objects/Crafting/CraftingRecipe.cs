using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemAmount {
    public Item item;
    public int amount;
}


[CreateAssetMenu(fileName = "Crafting", menuName = "New Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public List<ItemAmount> materials = new List<ItemAmount>();
    public ItemAmount result;




    public bool ContainsItem(Item item) {
        for (int i = 0; i < materials.Count; i++) {
            if (materials[i].item._name == item._name) {
                return true;
            }
        }
        return false;
    }

    public int ItemCount(Item item) {
        int number = 0;

        for (int i = 0; i < materials.Count; i++) {
            if (materials[i].item._name == item._name) {
                if (item.stackable) {
                    for(int j = 0; j < item.amount; j++) {
                        number++;
                    }
                }
                else {
                    number++;
                }
            }
        }
        return number;
    }
}
