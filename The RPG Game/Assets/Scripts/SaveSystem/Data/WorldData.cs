using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class WorldData
{
    //items
    public string[] itemPaths;
    public float[] x_coordinates_item;
    public float[] y_coordinates_item;
    public float[] z_coordinates_item;
    public float[] x_rotation_item;
    public float[] y_rotation_item;
    public float[] z_rotation_item;

    //constructs
    public string[] constructionPath;
    public float[] x_coordinates_con;
    public float[] y_coordinates_con;
    public float[] z_coordinates_con;
    public float[] x_rotation_con;
    public float[] y_rotation_con;
    public float[] z_rotation_con;



    public WorldData(WorldManager worldManager) {
        //items
        worldManager.SetItemsInWorld();

        itemPaths = new string[worldManager.itemsInWorld.Length];
        x_coordinates_item = new float[worldManager.itemsInWorld.Length];
        y_coordinates_item = new float[worldManager.itemsInWorld.Length];
        z_coordinates_item = new float[worldManager.itemsInWorld.Length];
        x_rotation_item = new float[worldManager.itemsInWorld.Length];
        y_rotation_item = new float[worldManager.itemsInWorld.Length];
        z_rotation_item = new float[worldManager.itemsInWorld.Length];

        for (int i = 0; i < worldManager.itemsInWorld.Length; i++) {
            itemPaths[i] = worldManager.itemsInWorld[i].item.GetPath();
            x_coordinates_item[i] = worldManager.itemsInWorld[i].transform.position.x;
            y_coordinates_item[i] = worldManager.itemsInWorld[i].transform.position.y;
            z_coordinates_item[i] = worldManager.itemsInWorld[i].transform.position.z;
            x_rotation_item[i] = worldManager.itemsInWorld[i].transform.localRotation.eulerAngles.x;
            y_rotation_item[i] = worldManager.itemsInWorld[i].transform.localRotation.eulerAngles.y;
            z_rotation_item[i] = worldManager.itemsInWorld[i].transform.localRotation.eulerAngles.z;
        }
        

        //constructions
        worldManager.SetConstructionsInWorld();

        constructionPath = new string[worldManager.constructionsInWorld.Length];
        x_coordinates_con = new float[worldManager.constructionsInWorld.Length];
        y_coordinates_con = new float[worldManager.constructionsInWorld.Length];
        z_coordinates_con = new float[worldManager.constructionsInWorld.Length];
        x_rotation_con = new float[worldManager.constructionsInWorld.Length];
        y_rotation_con = new float[worldManager.constructionsInWorld.Length];
        z_rotation_con = new float[worldManager.constructionsInWorld.Length];

        for (int i = 0; i < worldManager.constructionsInWorld.Length; i++) {
            constructionPath[i] = worldManager.constructionsInWorld[i].placeableObject.GetPath();
            x_coordinates_con[i] = worldManager.constructionsInWorld[i].transform.position.x;
            y_coordinates_con[i] = worldManager.constructionsInWorld[i].transform.position.y;
            z_coordinates_con[i] = worldManager.constructionsInWorld[i].transform.position.z;
            x_rotation_con[i] = worldManager.constructionsInWorld[i].transform.parent.localRotation.eulerAngles.x;
            y_rotation_con[i] = worldManager.constructionsInWorld[i].transform.parent.localRotation.eulerAngles.y;
            z_rotation_con[i] = worldManager.constructionsInWorld[i].transform.parent.localRotation.eulerAngles.z;
        }
    }
}
