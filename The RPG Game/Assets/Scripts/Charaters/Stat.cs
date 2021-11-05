using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private int baseValue = 1;

    private List<int> modifiers = new List<int>();

    public int GetValue() {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    public void AddModifier(int modifier) {
        if(modifier != 0) {
            modifiers.Add(modifier);
        }
    }

    public void RemoveModifier(int modifier) {
        if (modifier != 0) {
            modifiers.Remove(modifier);
        }
    }


    public void SetModifier(int value) {
        if(value != 0) {
            baseValue = value;
        }
    }

    public void ResetModifier() {
        baseValue = 1;
    }




}
