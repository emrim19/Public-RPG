using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crockpot : Construction
{
    public CrockpotUI crockpotUI;
    private ParticleSystem fireParticles;
    private Light fireLight;

    public float totalFuelCapacity;
    public float currentFuel;

    //For saving and loading
    public Food food1;
    public Food food2;
    public Food food3;
    public Food food4;
    public Food outputFood;

    public void Start() {

        fireParticles = GetComponentInChildren<ParticleSystem>();
        fireLight = GetComponentInChildren<Light>();

        totalFuelCapacity = 100;

        DeactivateFire();
        StartCoroutine(BurnTimer(1));
    }

    protected override void Interact() {
        base.Interact();
        UpdateCrockpot();
    }

    protected override void OnPickingUpObject() {
        base.OnPickingUpObject();
        if (crockpotUI.input.Count > 0) {
            for (int i = 0; i < crockpotUI.input.Count; i++) {
                Inventory.instance.AddItem(crockpotUI.input[i]);
            }
        }
        if(crockpotUI.output != null) {
            Inventory.instance.AddItem(crockpotUI.output);
        }

        if(food1 != null) {
            Inventory.instance.AddItem(food1);
        }
        if (food2 != null) {
            Inventory.instance.AddItem(food2);
        }
        if (food3 != null) {
            Inventory.instance.AddItem(food3);
        }
        if (food4 != null) {
            Inventory.instance.AddItem(food4);
        }
        if (outputFood != null) {
            Inventory.instance.AddItem(outputFood);
        }
    }

    public void UpdateCrockpot() {
        LoadTheThings();
        crockpotUI.ToggleUI();
        crockpotUI.UpdateFoodList();
        crockpotUI.UpdateUI();
        crockpotUI.UpdateNumberOfWood();
        
    }

    //for saving and loading
    public void LoadTheThings() {
        if (food1 != null) {
            crockpotUI.input.Add(food1);
        }
        if (food2 != null) {
            crockpotUI.input.Add(food2);
        }
        if (food3 != null) {
            crockpotUI.input.Add(food3);
        }
        if (food4 != null) {
            crockpotUI.input.Add(food4);
        }
        if (outputFood != null) {
            crockpotUI.output = outputFood;
        }

        food1 = null;
        food2 = null;
        food3 = null;
        food4 = null;
        outputFood = null;
    }

    public void ActivateFire() {
        fireParticles.gameObject.SetActive(true);
        fireLight.gameObject.SetActive(true);
    }
    public void DeactivateFire() {
        fireParticles.gameObject.SetActive(false);
        fireLight.gameObject.SetActive(false);
    }
    public void AddFuel(int fuel) {
        currentFuel += fuel;
        if (currentFuel > 0 && currentFuel < totalFuelCapacity) {

        }
        else if (currentFuel > totalFuelCapacity) {
            currentFuel = totalFuelCapacity;
        }
    }
    public IEnumerator BurnTimer(float duration) {
        while (true) {
            if (currentFuel == 0) {
                DeactivateFire();
            }
            else if(currentFuel > 0) {
                ActivateFire();
            }

            yield return new WaitForSeconds(duration);
            if (currentFuel > 0) {
                currentFuel--;
            }
        }
    }

}
 