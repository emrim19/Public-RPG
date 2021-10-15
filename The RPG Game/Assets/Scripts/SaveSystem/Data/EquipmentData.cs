using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentData
{
    public string[] equipmentPath;
    public int[] equipmentDurrability;

    public string ammoPath;
    public int ammoAmount;

    public EquipmentData(EquipmentManager equipManager) {
        equipmentPath = new string[equipManager.currentEquipment.Length];
        equipmentDurrability= new int[equipManager.currentEquipment.Length];

        for (int i = 0; i < equipManager.currentEquipment.Length; i++) {
            if(equipManager.currentEquipment[i] != null) {
                equipmentPath[i] = equipManager.currentEquipment[i].GetPath();
                equipmentDurrability[i] = equipManager.currentEquipment[i].currentDurability;
            }
        }
        if(equipManager.currentAmmo != null) {
            ammoPath = equipManager.currentAmmo.GetPath();
            ammoAmount = equipManager.currentAmmo.amount;
        }
    }
}
