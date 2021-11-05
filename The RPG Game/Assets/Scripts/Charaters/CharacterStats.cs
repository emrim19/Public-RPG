using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStats : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public bool isRange;
    public Stat damage;
    public Stat defence;
    public Stat range;
    public Stat magic;
    public Stat attackSpeed;
    
   

    public virtual void Die() {
        //do something when dead
    }

    public virtual void TakeDamage(int damage) {
        DamagePopup.Create(gameObject.transform.position, damage);
        health -= damage;
    }

    public float CalcDamageMultiplier(CharacterStats targetStats) {
        if(targetStats.defence.GetValue() >= 0) {
            float multiplier = 100 / (100 + (float)targetStats.defence.GetValue());
            return multiplier;
            
        }
        else if(targetStats.defence.GetValue() < 0){
            float multiplier = 100 / (100 - (float)targetStats.defence.GetValue());
            return multiplier;
        }
        return 0;
    }
     

    public int CalcMeeleDamage() {
        return damage.GetValue();
    }

    public int CalcRangeDamage(Ammo ammo) {
        return range.GetValue() + ammo.damageAmp;
    }

    public int CalcSpellDamage(Spell spell) {
        return magic.GetValue() + spell.damageAmp;
    }

}
