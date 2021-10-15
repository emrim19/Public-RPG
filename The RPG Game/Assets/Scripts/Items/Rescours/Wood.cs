using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WoodType {
    Oak,
    Mahogany
}

[CreateAssetMenu(fileName = "Item", menuName = "Item/Wood")]
public class Wood : Item
{
    public WoodType woodType;

    public override void Use() {
        base.Use();
    }
}
