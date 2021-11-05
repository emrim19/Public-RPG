using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType {
    Axe,
    Pickaxe,
    Sword,
    Bow,
    Staff,
    FishingPole,
    Default
}

[CreateAssetMenu(fileName = "Item", menuName = "Item/Weapon")]
public class Weapon : Equipment {

    public WeaponType weaponType;
    public bool isRange;
}


