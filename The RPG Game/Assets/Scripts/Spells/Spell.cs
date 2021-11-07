using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Spell", menuName = "Spell")]
public class Spell : ScriptableObject
{
    public GameObject prefab;
    public Sprite icon;
    public Effect effect;
    public string _name;
    public string description;
    public int damageAmp;
    public int manaCost;
    public float cooldown;
    public int spellAnimationNumber;
    public bool isRanged;
    

}
