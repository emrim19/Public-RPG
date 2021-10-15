using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CampfireData
{
    public float[] currentFuelPerCampfire;

    public CampfireData(WorldManager worldManager) {
        worldManager.SetCampfiresInWorld();

        currentFuelPerCampfire = new float[worldManager.campfiresInWorld.Length];

        for(int i = 0; i < worldManager.campfiresInWorld.Length; i++) {
            currentFuelPerCampfire[i] = worldManager.GetCampfire(i).currentFuel;
        }
    }
}
