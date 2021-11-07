using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats {
    
    private Enemy enemy;

    private void Start() {
        enemy = GetComponent<Enemy>();

        InvokeRepeating("DoDamageOverTime", 0, 0.5f);
    }

    private void Update() {
        UpdateAttackSpeed(enemy.animator);
        UpdateMovementSpeed(enemy.animator);
        UpdateEffects();
        Die();
    } 

    public override void Die() {
        base.Die();
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    public override void TakeDamage(int damage) {
        base.TakeDamage(damage);
        enemy.lookRadius = enemy.combatlookRadius;
    }


}
