using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    public Enemy enemy;

    public void Attack() {
        if(enemy.target != null) {
            enemy.combat.Attack(enemy.target.GetComponent<CharacterStats>());
        }
    }
}
