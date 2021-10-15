using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldManager : MonoBehaviour
{
    #region singelton

    public static WorldManager instance;

    private void Awake() {
        if (instance != null) {
            Debug.LogWarning("Warning: More than one instance of WorldManager found!");
            return;
        }
        instance = this;
    }
    #endregion

    public ItemPickup[] itemsInWorld;
    public Construction[] constructionsInWorld;
    public Chest[] chestsInWorld;
    public Campfire[] campfiresInWorld;
    public Furnace[] furnaceInWorld;
    public Crockpot[] crockpotsInWorld;

    // Start is called before the first frame update
    void Start()
    {
        SetItemsInWorld();
        SetConstructionsInWorld();
        SetChestsInWorld();
        SetCampfiresInWorld();
        SetFurnacesInWorld();
        SetCrockpotsInWorld();
    }

    //World
    public ItemPickup[] GetItemsInWorld() {
        return FindObjectsOfType<ItemPickup>();
    }
    public void SetItemsInWorld() {
        itemsInWorld = GetItemsInWorld();
    }
    public void DestroyItemsInWorld() {
        SetItemsInWorld();

        for(int i = 0; i < itemsInWorld.Length; i++) {
            Destroy(itemsInWorld[i].gameObject);
        }
    }

    //Construction
    public Construction[] GetConstructionsInWorld() {
        return FindObjectsOfType<Construction>();
    }
    public void SetConstructionsInWorld() {
        constructionsInWorld = GetConstructionsInWorld();
    }
    public void DestroyConstructionsInWorld() {
        SetConstructionsInWorld();

        for(int i = 0; i < constructionsInWorld.Length; i++) {
            Destroy(constructionsInWorld[i].transform.parent.gameObject);
        }
    }

    //Chest
    public Chest[] GetChestsInWorld() {
        return FindObjectsOfType<Chest>();
    }
    public Chest GetChest(int index) {
        return chestsInWorld[index];
    }
    public void SetChestsInWorld() {
        chestsInWorld = FindObjectsOfType<Chest>();
    }

    //Campfire
    public Campfire[] GetCampfiresInWorlds() {
        return FindObjectsOfType<Campfire>();
    }
    public Campfire GetCampfire(int index) {
        return campfiresInWorld[index];
    }
    public void SetCampfiresInWorld() {
        campfiresInWorld = GetCampfiresInWorlds();
    }

    //Furnace
    public Furnace[] GetFurnacesInWorld() {
        return FindObjectsOfType<Furnace>();
    }
    public Furnace GetFurnace(int index) {
        return furnaceInWorld[index];
    }
    public void SetFurnacesInWorld() {
        furnaceInWorld = GetFurnacesInWorld();
    }

    //Crockpot
    public Crockpot[] GetCrockpotsInWorld() {
        return FindObjectsOfType<Crockpot>();
    }
    public Crockpot GetCrockpot(int index) {
        return crockpotsInWorld[index];
    }
    public void SetCrockpotsInWorld() {
        crockpotsInWorld = GetCrockpotsInWorld();
    }
}
