using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Campfire : Construction
{
    public CampfireUI campfireUI;
    public ParticleSystem ps;
    public Light pointLight;

    public float totalFuelCapacity;
    public float currentFuel;
    public bool isFiredUp = false;

    private float burnTimer = 5;




    
    public void Start() {
        totalFuelCapacity = 100;

        campfireUI = GameObject.Find("CampfireUI").GetComponent<CampfireUI>();
        DeactivateFire();

        StartTimer(burnTimer);
    }

    public void StartTimer(float duration) {
        StartCoroutine(FloatTimer(duration));
    }

    public IEnumerator FloatTimer(float duration) {
        while (true) {
            if (currentFuel == 0) {
                DeactivateFire();
            }
            if(currentFuel > 0) {
                ActivateFire();
            }

            yield return new WaitForSeconds(duration);
            if(currentFuel > 0) {
                currentFuel--;
            }
        }
    }

    

    protected override void Interact() {
        base.Interact();
        campfireUI.ActivateUI();
        if (campfireUI.onItemChangedCallback != null) {
            campfireUI.onItemChangedCallback.Invoke();
        }
    }

    public void ActivateFire() {
        ps.gameObject.SetActive(true);
        pointLight.enabled = true;
        isFiredUp = true;

        pointLight.range = currentFuel / 2;
        ps.startLifetime = (currentFuel / 100) + 0.5f;
    }

    public void DeactivateFire() {
        ps.gameObject.SetActive(false);
        pointLight.enabled = false;
        isFiredUp = false;
    }

    public void AddFuel(float fuel) {
        currentFuel += fuel;
        if(currentFuel > 0 && currentFuel < totalFuelCapacity) {
            ActivateFire();
        }
        else if(currentFuel > totalFuelCapacity) {
            currentFuel = totalFuelCapacity;
            ActivateFire();
        }
    }


    private void OnMouseOver() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }
        Tooltip.ShowTooltip_static(objectName + " (" + currentFuel + ")");


        //MenuBar BAR 
        if (Input.GetMouseButtonDown(1)) {

        }
    }

    private void OnMouseExit() {
        Tooltip.HideTooltip_static();
    }

    private void OnDestroy() {
        campfireUI.DeactivateUI();
    }


}
