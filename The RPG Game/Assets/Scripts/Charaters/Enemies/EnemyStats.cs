using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats {
    
    private Enemy enemy;

    private void Start() {
        enemy = GetComponent<Enemy>();
    }

    private void Update() {
        UpdateAttackSpeed();
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
        enemy.lookRadius = 20;
    }

    void UpdateAttackSpeed() {
        float attSpeed = ((float)attackSpeed.GetValue() / 10);
        enemy.animator.SetFloat(Animator.StringToHash("AttackSpeedModifier"), 1 + attSpeed);
    }


}
