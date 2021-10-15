using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : Construction
{

    public FurnaceUI furnaceUI;

    public ParticleSystem fireParticles;
    public Light fireLight;

    public float totalFuelCapacity;
    public float currentFuel;

    //For loading and saving
    public Item inputItem;
    public int inputAmount;
    public float sliderValue;
    public Item outputItem;
    public int outputAmount;
    public Item bufferItem;
    


    // Start is called before the first frame update
    void Start() {
        furnaceUI.DeactivateUI();

        fireParticles = GetComponentInChildren<ParticleSystem>();
        fireLight = GetComponentInChildren<Light>();

        totalFuelCapacity = 100;

        DeactivateFire();
        StartCoroutine(BurnTimer(1));
    }


    protected override void Interact() {
        base.Interact();
        UpdateFurnace();
    }

    protected override void OnPickingUpObject() {
        base.OnPickingUpObject();
        if(furnaceUI.inputSlot.items.Count > 0) {
            for(int i = 0; i < furnaceUI.inputSlot.items.Count; i++) {
                Inventory.instance.AddItem(furnaceUI.inputSlot.items[i]);
            }
        }
        if (furnaceUI.outputSlot.items.Count > 0) {
            for (int i = 0; i < furnaceUI.outputSlot.items.Count; i++) {
                Inventory.instance.AddItem(furnaceUI.outputSlot.items[i]);
            }
        }
        if (furnaceUI.bufferSlot.items.Count > 0) {
            for (int i = 0; i < furnaceUI.bufferSlot.items.Count; i++) {
                Inventory.instance.AddItem(furnaceUI.bufferSlot.items[i]);
            }
        }

        if(inputItem != null) {
            for(int i = 0; i < inputAmount; i++) {
                Inventory.instance.AddItem(inputItem);
            }
        }
        if (outputItem != null) {
            for (int i = 0; i < outputAmount; i++) {
                Inventory.instance.AddItem(outputItem);
            }
        }
        if (bufferItem != null) {
            Inventory.instance.AddItem(bufferItem);
        }

    }

    public void UpdateFurnace() {
        furnaceUI.ToggleUI();
        furnaceUI.UpdateNumberOfWood();
        furnaceUI.UpdateNumberOfMinerals();
        LoadTheThings();
    }

    //for saving and loading
    public void LoadTheThings() {
        if (furnaceUI.IsActive()) {
             if (outputItem != null) {
                for (int i = 0; i < outputAmount; i++) {
                    furnaceUI.outputSlot.Additem(outputItem);
                }
            }
            if(inputItem != null) {
                for(int i = 0; i < inputAmount; i++) {
                    furnaceUI.inputSlot.Additem(inputItem);
                    furnaceUI.inputSlot.cookSlider.value = sliderValue;
                }
            }
            if(bufferItem != null) {
                furnaceUI.bufferSlot.Additem(bufferItem);
            }

            inputItem = null;
            inputAmount = 0;
            outputItem = null;
            outputAmount = 0;
            bufferItem = null;
        }
    }


    //particle effects
    public void ActivateFire() {
        fireParticles.gameObject.SetActive(true);
        fireLight.gameObject.SetActive(true);
    }
    public void DeactivateFire() {
        fireParticles.gameObject.SetActive(false);
        fireLight.gameObject.SetActive(false);
    }
    public IEnumerator BurnTimer(float duration) {
        while (true) {
            if (currentFuel == 0) {
                DeactivateFire();
            }
            else if (currentFuel > 0) {
                ActivateFire();
            }

            yield return new WaitForSeconds(duration);
            if (currentFuel > 0) {
                currentFuel--;
            }
        }
    }


    //Add fuel to furnace
    public void AddFuel(float fuel) {
        currentFuel += fuel;
        if (currentFuel > 0 && currentFuel < totalFuelCapacity) {
            ActivateFire();
        }
        else if (currentFuel > totalFuelCapacity) {
            currentFuel = totalFuelCapacity;
            ActivateFire();
        }
    }

}
