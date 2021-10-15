using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodType{
    Vegetable,
    Fruit,
    Meat,
    Fish,
    Dish
}

[CreateAssetMenu(fileName = "Object", menuName = "Item/Food")]
public class Food : Item
{
    public FoodType foodType;

    public int foodValue;
    public int healthValue;
    public int staminaValue;

    public override void Use() {
        base.Use();
        PlayerStats.instance.EatFood(this);
        RemoveFromInventory();
    }
}
