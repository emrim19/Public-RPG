using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Chest : Construction {
    public int space = 28;

    public List<Item> chestItems = new List<Item>();
    public ChestUI chestUI;

    public void Start() {
        chestUI = GameObject.Find("ChestUI").GetComponent<ChestUI>();
    }

    protected override void Interact() {
        base.Interact();
        chestUI.ToggleUI();
    }

    //Outdated methods
    public bool AddddddddddddddddItem(Item item) {
        if (chestItems.Count < space) {
            chestItems.Add(item);

            return true;
        }
        else if (chestItems.Count >= space) {
            Debug.Log("No more space in Chest!");
            return false;
        }
        return false;
    }
    public void RemoveeeeeeeeeeeeeeeItem(Item item) {
        chestItems.Remove(item);

        Tooltip.HideTooltip_static();
    }



    //New methods
    public bool AddItemToChest(Item item) {
        if(chestItems.Count < space) {
            if (item.stackable) {
                if (ContainsItem(item)) {
                    chestItems[FindItemIndex(item)].amount += 1;
                    Inventory.instance.RemoveItem(item);
                    return true;
                }
                else if (!ContainsItem(item)) {
                    Item newitem = ScriptableObject.Instantiate(item);
                    newitem.name = newitem.name.Replace("(Clone)", "");
                    newitem.amount = 1;

                    chestItems.Add(newitem);
                    Inventory.instance.RemoveItem(item);
                    return true;
                }
            }
            else if (!item.stackable) {
                chestItems.Add(item);
                Inventory.instance.RemoveItem(item);
                return true;
            }
        }
        else if (chestItems.Count >= space) {
            Debug.Log("No more space in Chest!");
            return false;
        }
        return false;
    }

    public void AddAllItemsToChest(Item item) {
        if (chestItems.Count < space) {
            int amount = item.amount;
            for(int i = 0; i < amount; i++) {
                AddItemToChest(item);
            }
        }
        else if (chestItems.Count >= space) {
            Debug.Log("No more space in Chest!");
        }
    }

    public void RemoveItemFromChest(Item item) {
        if (item.stackable) {
            if(chestItems[FindItemIndex(item)].amount > 1) {
                Item newitem = ScriptableObject.Instantiate(item);
                newitem.name = newitem.name.Replace("(Clone)", "");
                newitem.amount = 1;

                chestItems[FindItemIndex(item)].amount -= 1;
                Inventory.instance.AddItem(newitem);
            }
            else if(chestItems[FindItemIndex(item)].amount == 1) {
                chestItems.Remove(item);
                Inventory.instance.AddItem(item);
            }
        }
        else if (!item.stackable) {
            chestItems.Remove(item);
            Inventory.instance.AddItem(item);

            Tooltip.HideTooltip_static();
        }
    }

    public void RemoveAllItemsFromChest(Item item) {
        int amount = item.amount;
        for(int i = 0; i < amount; i++) {
            RemoveItemFromChest(item);
        }
    }

    protected override void OnPickingUpObject() {
        base.OnPickingUpObject();
        for(int i = 0; i < chestItems.Count; i++) {
            Inventory.instance.AddItem(chestItems[i]);
        }
    }


    public bool ContainsItem(Item item) {
        for (int i = 0; i < chestItems.Count; i++) {
            if (chestItems[i]._name == item._name) {
                return true;
            }
        }
        return false;
    }
    public int ItemCount(Item item) {
        int number = 0;

        for (int i = 0; i < chestItems.Count; i++) {
            if (chestItems[i]._name == item._name) {
                number++;
            }
        }
        return number;
    }
    public int FindItemIndex(Item item) {
        for (int i = 0; i < chestItems.Count; i++) {
            if (chestItems[i]._name == item._name) {
                return i;
            }
        }
        return 0;
    }


    public void OnDestroy() {
        chestUI.DeactivateUI();
    }




}
