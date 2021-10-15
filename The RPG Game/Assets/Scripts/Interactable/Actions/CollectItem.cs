using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : Interactable
{
    public Transform parent;
    public GameObject[] items;
    public Item item;
    public int amount, maxAmount;
    

    public void Start() {
        amount = parent.childCount;
    }

    public override void DoingAction() {
        base.DoingAction();
        PickSomeFruit();
    }

    public void PickSomeFruit() { 
        player.MoveToPoint(transform.position);
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= GetRadius()) {
            if(amount > 0) {
                Item theItem = ScriptableObject.Instantiate(item);
                theItem.name = theItem.name.Replace("(Clone", string.Empty);

                Inventory.instance.AddItem(theItem);
                parent.GetChild(amount - 1).gameObject.SetActive(false);
                amount--;
            }
            isDoingAction = false;
        }
    }


    public void UpdateItems() {
        for (int i = 0; i < amount; i++) {

        }
    }

}
