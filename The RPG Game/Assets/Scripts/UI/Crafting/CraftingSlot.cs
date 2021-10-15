using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour
{
    public Item item;
    public Image icon;

    public void AddItem(Item newItem) {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        
    }

    public void ClearSlot() {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }


}
