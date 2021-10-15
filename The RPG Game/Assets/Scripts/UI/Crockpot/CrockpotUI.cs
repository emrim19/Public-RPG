using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrockpotUI : UI
{
    public Player player; 
    public Crockpot crockpot;

    public Slider fuelSlider;
    public Slider cookSlider;

    public int oakNum;
    public int mahoganyNum;
    public Transform woodParent;
    public Button[] woodButtons;
    public Text[] woodTexts;

    public Transform foodParent;
    public FoodSlot[] foodSlots;
    public Button[] foodButton;
    public List<Food> foodList = new List<Food>();

    public Transform inputParent;
    public FoodInputSlot[] inputSlots;

    public Food output;
    public FoodOutputSlot outputSlot;

    public List<Food> input = new List<Food>();

    public Food[] results; //ADD THE RESULTS OF THE COOKING HERE!

    public float cookTimer;


    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
        
        woodButtons = woodParent.GetComponentsInChildren<Button>();
        woodTexts = woodParent.GetComponentsInChildren<Text>();

        foodButton = foodParent.GetComponentsInChildren<Button>();
        foodSlots = foodParent.GetComponentsInChildren<FoodSlot>();

        inputSlots = inputParent.GetComponentsInChildren<FoodInputSlot>();

        woodButtons[0].onClick.AddListener(PutOakOnFire);
        woodButtons[1].onClick.AddListener(PutMahoganyOnFire);

        onItemChangedCallback += UpdateNumberOfWood;
        onItemChangedCallback += UpdateFoodList;
        onItemChangedCallback += UpdateUI;
    }

    public void Update() {
        FocusCrockpot();
        UpdateFuelSlider();
        CookFood();
    }

    //For update
    public void FocusCrockpot() {
        if (player.focus != null) {
            if (player.focus.GetInstanceID() != crockpot.GetInstanceID()) {
                DeactivateUI();
            }
        }
        else if (player.focus == null) {
            DeactivateUI();
        }
    }

    public void UpdateFuelSlider() {
        fuelSlider.maxValue = crockpot.totalFuelCapacity;
        fuelSlider.value = crockpot.currentFuel;
    }


    public void UpdateFoodList() {
        foodList.Clear();
        for (int i = 0; i < Inventory.instance.items.Count; i++) {
            if (Inventory.instance.items[i].type.ToString() == "Food") {
                Food newFood = Inventory.instance.items[i] as Food;
                newFood.amount = Inventory.instance.items[i].amount;
                if (newFood.foodType != FoodType.Dish) {
                    foodList.Add(newFood);
                }
            }
        }
    }

    public void UpdateUI() {
        if (crockpot != null) {
            for(int i = 0; i < foodSlots.Length; i++) {
                if(i < foodList.Count) {
                    foodSlots[i].AddFood(foodList[i]);
                }
                else {
                    foodSlots[i].ClearSlot();
                }
            }

            for (int i = 0; i < inputSlots.Length; i++) {
                if (i < input.Count) {
                    inputSlots[i].AddFood(input[i]);
                }
                else {
                    inputSlots[i].ClearSlot();
                }
            }

            if(output != null) {
                outputSlot.AddFood(output);
            }
            else {
                outputSlot.ClearSlot();
            }
        }
    }
    public void UpdateNumberOfWood() {
        oakNum = 0;
        mahoganyNum = 0;

        for (int i = 0; i < Inventory.instance.items.Count; i++) {
            if (Inventory.instance.items[i]._name == "Oak Logs") {
                oakNum++;
            }
            else if (Inventory.instance.items[i]._name == "Mahogany Logs") {
                mahoganyNum++;
            }
        }
        woodTexts[0].text = oakNum.ToString();
        woodTexts[1].text = mahoganyNum.ToString();
    }



    public void PutOakOnFire() {
        if (oakNum > 0) {
            for (int i = 0; i < Inventory.instance.items.Count; i++) {
                if (Inventory.instance.items[i]._name == "Oak Logs") {
                    Inventory.instance.RemoveAtIndex(i);
                    crockpot.AddFuel(10);

                    UpdateNumberOfWood();
                    return;
                }
            }
        }

    }
    public void PutMahoganyOnFire() {
        if (mahoganyNum > 0) {
            for (int i = 0; i < Inventory.instance.items.Count; i++) {
                if (Inventory.instance.items[i]._name == "Mahogany Logs") {
                    Inventory.instance.RemoveAtIndex(i);
                    crockpot.AddFuel(25);

                    UpdateNumberOfWood();
                    return;
                }
            }
        }
    }


    public void CookFood() {
        if(crockpot != null) {
            if (input.Count == 4) {

                cookSlider.maxValue = 5;
                cookSlider.value = cookTimer;

                if (fuelSlider.value > 0) {
                    if (cookTimer < cookSlider.maxValue) {
                        cookTimer += Time.deltaTime;

                        if (cookTimer >= cookSlider.maxValue) {
                            output = GetResult();
                            input.Clear();
                            if (onItemChangedCallback != null) {
                                onItemChangedCallback.Invoke();
                            }
                            cookTimer = 0;
                        }
                    }
                }
            }
            else if (input.Count < 4) {
                cookSlider.value = 0;
            }
        }
    }


    public Food GetResult() {
        int vegatable = 0;
        int fruit = 0;
        int meat = 0;

        for (int i = 0; i < input.Count; i++) {
            if (input[i].foodType == FoodType.Vegetable) {
                vegatable++;
            }
            else if (input[i].foodType == FoodType.Fruit) {
                fruit++;
            }
            else if (input[i].foodType == FoodType.Meat) {
                meat++;
            }
        }

        if(meat >= 1) {
            return results[2];
        }
        else if(fruit >= 3) {
            return results[0];
        }
        else if(vegatable >= 3 && meat == 0) {
            return results[1]; 
        }
        else {
            return results[0];
        }
    }
    
}
