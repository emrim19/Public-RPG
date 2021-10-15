using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    public PlayerStats playerStats;

    public Slider healthSlider;
    public Slider staminaSlider;
    public Slider foodSlider;
    public Slider overheadHealthBar;

    public Text healthAmount;
    public Text staminaAmount;
    public Text foodAmount;

    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

        SetSliderValues();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSliders();
    }

    public void SetSliderValues() {
        healthSlider.maxValue = playerStats.maxHealth;
        staminaSlider.maxValue = playerStats.maxStamina;
        foodSlider.maxValue = playerStats.maxFood;

        //overhead health bar
        overheadHealthBar.maxValue = playerStats.maxHealth;
    }

    public void UpdateSliders() {
        healthSlider.value = playerStats.health;
        staminaSlider.value = playerStats.stamina;
        foodSlider.value = playerStats.foodValue;

        healthAmount.text = Mathf.CeilToInt(healthSlider.value).ToString() + "/ " + healthSlider.maxValue.ToString();
        staminaAmount.text =  Mathf.CeilToInt(staminaSlider.value).ToString() + "/ " +staminaSlider.maxValue.ToString();
        foodAmount.text = Mathf.CeilToInt(foodSlider.value).ToString() + "/ " +foodSlider.maxValue.ToString() ;

        //overhead health bar
        if (playerStats.player.inCombat) {
            overheadHealthBar.gameObject.SetActive(true);
            overheadHealthBar.value = playerStats.health;
            overheadHealthBar.gameObject.transform.LookAt(cam.transform.position);
            overheadHealthBar.gameObject.transform.rotation = Quaternion.LookRotation(healthSlider.gameObject.transform.position - (cam.transform.position));
        }
        else {
            overheadHealthBar.gameObject.SetActive(false);
        }
    }



}
