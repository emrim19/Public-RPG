using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    #region singelton

    public static Inventory instance;

    private void Awake() {
        if (instance != null) {
            Debug.LogWarning("Warning: More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    #endregion

    public int space = 28;

    public List<Item> items = new List<Item>();

    public GameObject itemParent;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;


    //OUTDATED ADD FUNCTION
    private bool AdddddddItem(Item item) {
        if (items.Count < space) {

            items.Add(item);

            if (onItemChangedCallback != null) {
                onItemChangedCallback.Invoke();
            }
            return true;
        }
        else if (items.Count >= space) {
            Debug.Log("No more space in inventory!");
            return false;
        }
        return false;
    }

    //New add function
    public bool AddItem(Item item) {
        if (items.Count < space) {
            if (item.stackable) {
                if (ContainsItem(item)) {
                    items[FindItemIndex(item)].amount += item.amount;

                    if (onItemChangedCallback != null) {
                        onItemChangedCallback.Invoke();
                    }
                    return true;
                }
                else if (!ContainsItem(item)) {
                    items.Add(item);

                    if (onItemChangedCallback != null) {
                        onItemChangedCallback.Invoke();
                    }
                    return true;
                }
            }
            else {
                items.Add(item);

                if (onItemChangedCallback != null) {
                    onItemChangedCallback.Invoke();
                }
                return true;
            }
        }
        else if (items.Count >= space) {
            Debug.Log("No more space in inventory!");
            return false;
        }
        return false;
    }


    //removes the item from the game 
    public void RemoveItem(Item item) {
        if (item.stackable) {
            if(item.amount > 1) {
                item.amount -= 1;

                if (onItemChangedCallback != null) {
                    onItemChangedCallback.Invoke();
                }
            }
            else if(item.amount == 1) {
                items.Remove(item);

                Tooltip.HideTooltip_static();

                if (onItemChangedCallback != null) {
                    onItemChangedCallback.Invoke();
                }
            }
        }
        else {
            items.Remove(item);

            Tooltip.HideTooltip_static();

            if (onItemChangedCallback != null) {
                onItemChangedCallback.Invoke();
            }
        }
    }

    public void RemoveAtIndex(int index) {
        if (items[index].stackable) {
            if(items[index].amount > 1) {
                items[index].amount -= 1;

                if (onItemChangedCallback != null) {
                    onItemChangedCallback.Invoke();
                }
            }
            else if(items[index].amount == 1) {
                items.RemoveAt(index);
                Tooltip.HideTooltip_static();

                if (onItemChangedCallback != null) {
                    onItemChangedCallback.Invoke();
                }
            }
        }
        else {
            items.RemoveAt(index);
            Tooltip.HideTooltip_static();

            if (onItemChangedCallback != null) {
                onItemChangedCallback.Invoke();
            }
        }
    } 


    public void DropItem(Item item) {
        if (item.stackable) {
            if(item.amount > 1) {
                GameObject newItem = Instantiate(item.prefab, transform.position, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
                newItem.transform.parent = itemParent.transform;
                item.amount -= 1;

                if (onItemChangedCallback != null) {
                    onItemChangedCallback.Invoke();
                }
            }
            else if(item.amount == 1){
                GameObject newItem = Instantiate(item.prefab, transform.position, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
                newItem.transform.parent = itemParent.transform;
                items.Remove(item);

                if (onItemChangedCallback != null) {
                    onItemChangedCallback.Invoke();
                }
            }
        }
        else {
            GameObject newItem = Instantiate(item.prefab, transform.position, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
            newItem.transform.parent = itemParent.transform;
            items.Remove(item);

            if (onItemChangedCallback != null) {
                onItemChangedCallback.Invoke();
            }
        }
    }


    public bool ContainsItem(Item item) {
        for(int i = 0; i < items.Count; i++) {
            if(items[i]._name == item._name) {
                return true;
            }
        }
        return false;
    }

    public int ItemCount(Item item) {
        int number = 0;
        
        for (int i = 0; i < items.Count; i++) {
            if (items[i]._name == item._name) {
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

    public int FindItemIndex(Item item) {
        for (int i = 0; i < items.Count; i++) {
            if (items[i]._name == item._name) {
                return i;
            }
        }
        return 0;
    }






}
