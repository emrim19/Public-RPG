using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public Player player;

    public float stamina;
    public float maxStamina;
    public float foodValue;
    public float maxFood;


    #region singelton

    public static PlayerStats instance;

    private void Awake() {
        if (instance != null) {
            Debug.LogWarning("Warning: More than one instance of PlayerStats found!");
            return;
        }
        instance = this;
    }

    #endregion

    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        player = GetComponent<Player>();
    }

    public void Update() {
        UpdateStamina();
        UpdateFood();
        UpdateHealth();
        UpdateAttackSpeed();
        Die();
    }
    
    public void OnEquipmentChanged(Equipment newItem, Equipment oldItem) {
        if(newItem != null) {
            damage.AddModifier(newItem.attackMod);
            defence.AddModifier(newItem.defenceMod);
            attackSpeed.AddModifier(newItem.attackSpeedMod);
            attackRange.AddModifier(newItem.attackRange);
            magic.AddModifier(newItem.magicMod);

        }

        if(oldItem != null) {
            damage.RemoveModifier(oldItem.attackMod);
            defence.RemoveModifier(oldItem.defenceMod);
            attackSpeed.RemoveModifier(oldItem.attackSpeedMod);
            attackRange.RemoveModifier(oldItem.attackRange);
            magic.RemoveModifier(oldItem.magicMod);
        }
    }
        

    //UPDATE FOR STATS
    public void UpdateStamina() {
        if (player.isRunning && player.agent.velocity.magnitude > 0) {
            if (stamina > 0) {
                stamina -= Time.deltaTime * 3;
            }
            else if(stamina <= 0) {
                stamina = 0;
                player.isRunning = false;
            }
        }
        else {
            if(stamina < maxStamina) {
                stamina += Time.deltaTime * 3;
            }
            else if(stamina >= maxStamina) {
                stamina = maxStamina;
            }
        }
    }
    public void UpdateFood() {
        if(foodValue > 0) {
            foodValue -= Time.deltaTime / 10;
        }
        else if(foodValue <= 0) {
            foodValue = 0;
        }

        if(foodValue > maxFood) {
            foodValue = maxFood;
        }
    }
    public void UpdateHealth() {
        if(health > maxHealth) {
            health = maxHealth;
        }

        if(health <= 0) {
            health = 0;
        }
    }

    void UpdateAttackSpeed() {
        float attSpeed = ((float)attackSpeed.GetValue() / 10);
        player.animator.SetFloat(Animator.StringToHash("AttackSpeedModifier"), 1 + attSpeed);
    }

    //EAT FOOD
    public void EatFood(Food food) {
        print("You ate " + food._name);
        health += food.healthValue;
        stamina += food.staminaValue;
        foodValue += food.foodValue;
    }

    public override void Die() {
        base.Die();
        if(health <= 0) {
            player.KillPlayer();
            health = 90;
        }
        
    }

}
