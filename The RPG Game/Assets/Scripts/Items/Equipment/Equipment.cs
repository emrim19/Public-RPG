using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EquipmentSlot {
    Head,
    Chest,
    Legs, 
    Feet,
    Weapon,
    Shield,
    Cape
}

public enum EquipmentTier{
    Other,
    Wood,
    Stone,
    Iron
}

[CreateAssetMenu(fileName = "Item", menuName = "Item/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    public EquipmentTier equipTier;

    public int currentDurability;
    public int maxDurability;

    public int attackMod;
    public int defenceMod;
    public int attackSpeedMod;
    public int attackRange;
    public int magicMod;
  
    

    public new void Awake() {
        amount = 1;
        groundLayer = LayerMask.GetMask("GroundLayer");

        SetPath("Items/" + type.ToString() + "/" + _name);
        SetPath(GetPath().Replace(" ", string.Empty));

        currentDurability = maxDurability;
    }

     
    public override void Use() { 
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }

    public bool LoseDurability(int amount) {
        currentDurability -= amount;
        return true;
    }

    public float ConvertTierToNumber() {
        if (equipTier == EquipmentTier.Wood) {
            return 0.8f;
        }
        else if (equipTier == EquipmentTier.Stone) {
            return 1f;
        }
        else if (equipTier == EquipmentTier.Iron) {
            return 1.2f;
        }
        else if (equipTier == EquipmentTier.Other) {
            return 1f;
        }
        else {
            return 1f;
        }
    }


}

