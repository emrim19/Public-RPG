using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CrockpotData
{
    public float[] currentFuelPerCrockpot;
    public string[] foodPath1;
    public string[] foodPath2;
    public string[] foodPath3;
    public string[] foodPath4;
    public string[] foodOutputPath;

    public CrockpotData(WorldManager worldManager) {
        worldManager.SetCrockpotsInWorld();

        currentFuelPerCrockpot = new float[worldManager.crockpotsInWorld.Length];
        foodPath1 = new string[worldManager.crockpotsInWorld.Length];
        foodPath2 = new string[worldManager.crockpotsInWorld.Length];
        foodPath3 = new string[worldManager.crockpotsInWorld.Length];
        foodPath4 = new string[worldManager.crockpotsInWorld.Length];
        foodOutputPath = new string[worldManager.crockpotsInWorld.Length];

        for (int i = 0; i < worldManager.crockpotsInWorld.Length; i++) {
            currentFuelPerCrockpot[i] = worldManager.GetCrockpot(i).currentFuel;

            if(worldManager.GetCrockpot(i).crockpotUI.input.Count == 1) {
                foodPath1[i] = worldManager.GetCrockpot(i).crockpotUI.input[0].GetPath();

            }
            if (worldManager.GetCrockpot(i).crockpotUI.input.Count == 2) {
                foodPath1[i] = worldManager.GetCrockpot(i).crockpotUI.input[0].GetPath();
                foodPath2[i] = worldManager.GetCrockpot(i).crockpotUI.input[1].GetPath();

            }
            if (worldManager.GetCrockpot(i).crockpotUI.input.Count == 3) {
                foodPath1[i] = worldManager.GetCrockpot(i).crockpotUI.input[0].GetPath();
                foodPath2[i] = worldManager.GetCrockpot(i).crockpotUI.input[1].GetPath();
                foodPath3[i] = worldManager.GetCrockpot(i).crockpotUI.input[2].GetPath();

            }
            if (worldManager.GetCrockpot(i).crockpotUI.input.Count == 4) {
                foodPath1[i] = worldManager.GetCrockpot(i).crockpotUI.input[0].GetPath();
                foodPath2[i] = worldManager.GetCrockpot(i).crockpotUI.input[1].GetPath();
                foodPath3[i] = worldManager.GetCrockpot(i).crockpotUI.input[2].GetPath();
                foodPath4[i] = worldManager.GetCrockpot(i).crockpotUI.input[3].GetPath();

            }

            if(worldManager.GetCrockpot(i).crockpotUI.output != null) {
                foodOutputPath[i] = worldManager.GetCrockpot(i).crockpotUI.output.GetPath();
            }


            if(worldManager.GetCrockpot(i).food1 != null) {
                foodPath1[i] = worldManager.GetCrockpot(i).food1.GetPath();
            }
            if (worldManager.GetCrockpot(i).food2 != null) {
                foodPath2[i] = worldManager.GetCrockpot(i).food2.GetPath();
            }
            if (worldManager.GetCrockpot(i).food3 != null) {
                foodPath3[i] = worldManager.GetCrockpot(i).food3.GetPath();
            }
            if (worldManager.GetCrockpot(i).food4 != null) {
                foodPath4[i] = worldManager.GetCrockpot(i).food4.GetPath();
            }
            if (worldManager.GetCrockpot(i).outputFood != null) {
                foodOutputPath[i] = worldManager.GetCrockpot(i).outputFood.GetPath();
            }
        }
    }


}
