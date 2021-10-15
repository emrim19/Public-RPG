using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FoodSlot : MonoBehaviour
{
    public CrockpotUI crockpotUI;

    public Food food;
    public Text itemCountText;
    public Image icon;
    public Button button;

     
    public void Start() {
        button = GetComponentInChildren<Button>();

        button.onClick.AddListener(AddFoodToInput);
    }


    public void AddFood(Food newFood) {
        food = newFood;

        icon.enabled = true;
        icon.sprite = food.icon;

        itemCountText.enabled = true;
        itemCountText.text = food.amount.ToString();
    }

    public void ClearSlot() {
        food = null;

        icon.enabled = false;
        icon.sprite = null;

        itemCountText.enabled = false;
    }

    public void AddFoodToInput() {
        if (food != null) {
            if(crockpotUI.input.Count < 4 && crockpotUI.output == null) {
                Food newFood = ScriptableObject.Instantiate(food);
                crockpotUI.input.Add(newFood);
                Inventory.instance.RemoveAtIndex(Inventory.instance.FindItemIndex(food));
                if (crockpotUI.onItemChangedCallback != null) {
                    crockpotUI.onItemChangedCallback.Invoke();
                }
            }
        }
    }


}
