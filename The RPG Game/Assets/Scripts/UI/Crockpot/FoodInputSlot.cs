using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodInputSlot : MonoBehaviour
{
    public CrockpotUI crockpotUI;

    public Food food;
    public Text itemCountText;
    public Image icon;
    public Button button;


    public void Start() {
        //crockpotUI = GameObject.Find("CrockpotUI").GetComponent<CrockpotUI>();
        button = GetComponentInChildren<Button>();

        button.onClick.AddListener(RemoveFoodFromInput);
    }


    public void AddFood(Food newFood) {
        food = newFood;

        icon.enabled = true;
        icon.sprite = food.icon;

        itemCountText.enabled = false;
    }

    public void ClearSlot() {
        food = null;

        icon.enabled = false;
        icon.sprite = null;

        itemCountText.enabled = false;
    }

    public void RemoveFoodFromInput() {
        if(food != null) {
            crockpotUI.input.Remove(food);
            crockpotUI.cookTimer = 0;
            Inventory.instance.AddItem(food);

            if (crockpotUI.onItemChangedCallback != null) {
                crockpotUI.onItemChangedCallback.Invoke();
            }
        }

    }
}
