using UnityEngine;

public abstract class CharacterStats : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Stat damage;
    public Stat defence;
    public Stat attackSpeed;
    public Stat attackRange;
    public Stat magic;
    

    public virtual void Die() {
        //do something when dead
    }

    public void TakeDamage(float damage) {
        health -= damage;
    }


}
