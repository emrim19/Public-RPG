using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType{
    Slow,
    Stun,
    Heal,
    DmgOverTime,
    DmgReduction,
    DefReduction
}

[CreateAssetMenu(fileName = "Effect", menuName = "Effect")]
public class Effect : ScriptableObject
{
    public EffectType effectType;
    public float effectStrenght;
    public float duration;

}
