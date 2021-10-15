using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FurnaceData
{
    public float[] currentFuelPerFurnace;
    public string[] inputPath;
    public int[] inputAmount;
    public string[] outpurPath;
    public int[] outputAmount;
    public string[] bufferPath;
    public float[] sliderValue;

    public FurnaceData(WorldManager worldManager) {
        worldManager.SetFurnacesInWorld();

        currentFuelPerFurnace = new float[worldManager.furnaceInWorld.Length];
        inputPath = new string[worldManager.furnaceInWorld.Length];
        inputAmount = new int[worldManager.furnaceInWorld.Length];
        outpurPath = new string[worldManager.furnaceInWorld.Length];
        outputAmount = new int[worldManager.furnaceInWorld.Length];
        bufferPath = new string[worldManager.furnaceInWorld.Length];
        sliderValue = new float[worldManager.furnaceInWorld.Length];
        
        for (int i = 0; i < worldManager.furnaceInWorld.Length; i++) {
            currentFuelPerFurnace[i] = worldManager.GetFurnace(i).currentFuel;

            if(worldManager.GetFurnace(i).furnaceUI.inputSlot.items.Count > 0) {
                inputPath[i] = worldManager.GetFurnace(i).furnaceUI.inputSlot.items[0].GetPath();
                inputAmount[i] = worldManager.GetFurnace(i).furnaceUI.inputSlot.items.Count;
                Debug.Log(inputPath[i]);
            }
            else {
                inputPath[i] = null;
                inputAmount[i] = 0;
            }

            if (worldManager.GetFurnace(i).furnaceUI.outputSlot.items.Count > 0) {
                outpurPath[i] = worldManager.GetFurnace(i).furnaceUI.outputSlot.items[0].GetPath();
                outputAmount[i] = worldManager.GetFurnace(i).furnaceUI.outputSlot.items.Count;
            }
            else if(worldManager.GetFurnace(i).furnaceUI.outputSlot.items.Count == 0) {
                outpurPath[i] = null;
                outputAmount[i] = 0;
            }

            if (worldManager.GetFurnace(i).furnaceUI.bufferSlot.items.Count > 0) {
                bufferPath[i] = worldManager.GetFurnace(i).furnaceUI.bufferSlot.items[0].GetPath();
            }
            else {
                bufferPath[i] = null;
            }

            if (worldManager.GetFurnace(i).furnaceUI.inputSlot.cookSlider.IsActive()) {
                sliderValue[i] = worldManager.GetFurnace(i).furnaceUI.inputSlot.cookSlider.value;
            }
            else if(!worldManager.GetFurnace(i).furnaceUI.inputSlot.cookSlider.IsActive()){
                sliderValue[i] = 0;
            }

            
            if(worldManager.GetFurnace(i).inputItem != null) {
                inputPath[i] = worldManager.GetFurnace(i).inputItem.GetPath();
                inputAmount[i] = worldManager.GetFurnace(i).inputAmount;
            }
            if (worldManager.GetFurnace(i).outputItem != null) {
                outpurPath[i] = worldManager.GetFurnace(i).outputItem.GetPath();
                outputAmount[i] = worldManager.GetFurnace(i).outputAmount;
            }
            if (worldManager.GetFurnace(i).bufferItem != null) {
                bufferPath[i] = worldManager.GetFurnace(i).bufferItem.GetPath();
            }
        }



    }

}
