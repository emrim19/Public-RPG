using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats {
    public Enemy enemy;


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

    void UpdateAttackSpeed() {
        float attSpeed = ((float)attackSpeed.GetValue() / 10);
        enemy.animator.SetFloat(Animator.StringToHash("AttackSpeedModifier"), 1 + attSpeed);
    }

}
