using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MineralType {
    Stone,
    Iron,
    Gold
}

[CreateAssetMenu(fileName = "Item", menuName = "Item/Mineral")]
public class Mineral : Item
{
    public MineralType mineralType;

    public override void Use() {
        base.Use();
    }
}
