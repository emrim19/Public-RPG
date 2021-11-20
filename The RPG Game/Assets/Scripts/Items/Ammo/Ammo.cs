using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AmmoType {
    Arrow
}


[CreateAssetMenu(fileName = "Item", menuName = "Item/Ammo")]
public class Ammo : Item
{
    public AmmoType ammoType;
    public GameObject ammoPrefab;
    public int damageAmp;
    public Effect effect;

    public override void Use() {
        base.Use();
        Ammo newAmmo = ScriptableObject.Instantiate(this);
        newAmmo.amount = amount;
        EquipmentManager.instance.EquipAmmo(newAmmo);

        amount = 1;
        RemoveFromInventory();
    }
}
