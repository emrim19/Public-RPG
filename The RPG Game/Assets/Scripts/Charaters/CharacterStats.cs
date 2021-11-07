using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStats : MonoBehaviour
{
    public float speed;
    public float defaultSpeed = 3f;
    public float health;
    public float maxHealth;
    public bool isRange;
    public Stat damage;
    public Stat defence;
    public Stat range;
    public Stat magic;
    public Stat attackSpeed;

    public List<Effect> effects = new List<Effect>();

    public bool isSlowed;
    public float slowDuration;
    public bool isStunned;
    public float stunDuration;
    public bool isDmgOverTime;
    public float dmgOverTimeDuration;

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
     
    //damage calculations
    public int CalcMeeleDamage() {
        return damage.GetValue();
    }

    public int CalcRangeDamage(Ammo ammo) {
        return range.GetValue() + ammo.damageAmp;
    }

    public int CalcSpellDamage(Spell spell) {
        return magic.GetValue() + spell.damageAmp;
    }

    //set movementSpeed
    public void SetSpeed(float speed) {
        this.speed = speed;
    }

    public void UpdateMovementSpeed(Animator animator) {
        animator.SetFloat(Animator.StringToHash("MovementSpeedModifier"), speed);
    }

    //set AttackSpeed
    public void UpdateAttackSpeed(Animator animator) {
        float attSpeed = ((float)attackSpeed.GetValue() / 10);
        animator.SetFloat(Animator.StringToHash("AttackSpeedModifier"), 1 + attSpeed);
    }




    //EFFECTS
    public void AddEffect(Effect effect) {
        if(effects.Count == 0) {
            effects.Add(effect);
        }
        else if(effects.Count > 0) {
            if (!effects.Contains(effect)) {
                effects.Add(effect);
            }
        }
    }

    public void RemoveEffect(Effect effect) {
        effects.Remove(effect);
    }

    protected void UpdateEffects() {
        if(effects.Count > 0) {
            for(int i = 0; i < effects.Count; i++) {
                if (effects[i].effectType == EffectType.Slow) {
                    if (!isSlowed) {
                        Slow(effects[i]);
                    }
                }
                if (effects[i].effectType == EffectType.Stun) {
                    if (!isStunned) {
                        Stun(effects[i]);
                    }
                }
                if(effects[i].effectType == EffectType.DmgOverTime) {
                    if (!isDmgOverTime) {
                        DmgOverTime(effects[i]);
                    }
                }
            }
        }

        if (isSlowed) {
            if(slowDuration > 0) {
                slowDuration -= Time.deltaTime;
            }
            else if (slowDuration <= 0) {
                RemoveEffect(effects.Find((x) => x.effectType == EffectType.Slow));
                SetSpeed(defaultSpeed);
                isSlowed = false;
            }
        }
        if (isStunned) {
            if (stunDuration > 0) {
                stunDuration -= Time.deltaTime;
            }
            else if (stunDuration <= 0) {
                RemoveEffect(effects.Find((x) => x.effectType == EffectType.Stun));
                SetSpeed(defaultSpeed);
                isStunned = false;
            }
        }
        if (isDmgOverTime) {
            if(dmgOverTimeDuration > 0) {
                dmgOverTimeDuration -= Time.deltaTime;
            }
            else if(dmgOverTimeDuration <= 0) {
                RemoveEffect(effects.Find((x) => x.effectType == EffectType.DmgOverTime));
                isDmgOverTime = false;
            }
        }
    }

    public void Slow(Effect effect) {
        speed -= effect.effectStrenght;
        if(speed <= 0) {
            speed = 1;
        }
        slowDuration = effect.duration;
        isSlowed = true;
    }

    public void Stun(Effect effect) {
        stunDuration = effect.duration;
        isStunned = true;
    }

    public void DmgOverTime(Effect effect) {
        dmgOverTimeDuration = effect.duration;
        isDmgOverTime = true;
    }

    //OBS: used as InvokeRepeating in start function (PlayerStats and EnemyStats)
    protected void DoDamageOverTime() {
        if (isDmgOverTime) {
            Effect theEffect = effects.Find((x) => x.effectType == EffectType.DmgOverTime);
            if(theEffect != null) {
                TakeDamage((int)theEffect.effectStrenght);
            }
        }
    }

    

}
