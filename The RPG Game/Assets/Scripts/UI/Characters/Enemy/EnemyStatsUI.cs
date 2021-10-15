using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatsUI : MonoBehaviour
{
    public Enemy enemy;
    public Slider healthSlider;
    public EnemyStats stats;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        stats = GetComponent<EnemyStats>();
        SetSliderValues();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlider();
    }


    public void SetSliderValues() {
        healthSlider.maxValue = stats.maxHealth;
    }

    public void UpdateSlider() {
        if(healthSlider != null && enemy != null) {
            if (enemy.inCombat) {
                healthSlider.gameObject.SetActive(true);
                healthSlider.value = enemy.enemyStats.health;
                healthSlider.gameObject.transform.LookAt(Camera.main.transform.position);
                healthSlider.gameObject.transform.rotation = Quaternion.LookRotation(healthSlider.gameObject.transform.position - (Camera.main.transform.position));
            }
            else if (!enemy.inCombat) {
                healthSlider.gameObject.SetActive(false);
            }
        }
    }
}
