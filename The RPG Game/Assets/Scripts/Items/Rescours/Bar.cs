using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BarType {
    Iron,
    Gold
}

[CreateAssetMenu(fileName = "Item", menuName = "Item/Bar")]
public class Bar : Item
{
    public BarType barType;

    public override void Use() {
        base.Use();
    }
}
