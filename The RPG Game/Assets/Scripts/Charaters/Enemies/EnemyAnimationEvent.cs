using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    public Enemy enemy;
    

    public void MeeleAttack() {
        if(enemy.target != null) {
            enemy.target.GetComponent<CharacterStats>().TakeDamage(enemy.enemyStats.CalcMeeleDamage());
        }
    }

}
