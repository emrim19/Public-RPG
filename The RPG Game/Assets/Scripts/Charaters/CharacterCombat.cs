using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {

    private CharacterStats myStats;
    private Player player;

    private void Start() {
        myStats = GetComponent<CharacterStats>();
        player = GetComponent<Player>();
    }


    public void Attack(CharacterStats targetStats) {
        DoDamage(targetStats);
    }
     
    private void DoDamage(CharacterStats stats) {
        int damage = CalculateDamage(stats);
        DamagePopup.Create(stats.gameObject.transform.position, damage);
        stats.TakeDamage(damage);
    }

    public int CalculateDamage(CharacterStats stats) {
        int minDamage = Mathf.FloorToInt(myStats.damage.GetValue() / 2);
        int maxDamage = myStats.damage.GetValue();

        if(minDamage <= 0){
            minDamage = 1;
        }

        float damage = Random.Range(minDamage, (maxDamage + 1));

        //for ammo amplification with a bow
        if (myStats.gameObject.GetComponent<Player>()) {
            if (player.CheckWeaponEquiped() != null) {
                if (player.CheckWeaponEquiped().weaponType == WeaponType.Bow) {
                    if (EquipmentManager.instance.currentAmmo != null) {
                        damage += damage + EquipmentManager.instance.currentAmmo.damageAmp;
                    }
                }
            }
        }
           
        
        

        float damage2 = damage * CalculateDamageReduction(stats);
        int theDamage = Mathf.FloorToInt(damage2);
        
        if(theDamage <= 0) {
            return 1;
        }
        else {
            return theDamage;
        }
    }

    public float CalculateDamageReduction(CharacterStats stats) {
        float damageReduction = 100 / (100 + (float)stats.defence.GetValue());
        return damageReduction;
    }

}
