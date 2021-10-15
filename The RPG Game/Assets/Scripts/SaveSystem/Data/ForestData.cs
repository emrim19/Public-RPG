using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ForestData
{
    public string[] forestPaths;
    public float[] x_coordinates;
    public float[] y_coordinates;
    public float[] z_coordinates;
    public float[] x_rotation;
    public float[] y_rotation;
    public float[] z_rotation;
    public float[] scale;


    public ForestData(Transform forestParent) {
        forestPaths = new string[forestParent.childCount];
        x_coordinates = new float[forestParent.childCount];
        y_coordinates = new float[forestParent.childCount];
        z_coordinates = new float[forestParent.childCount];
        x_rotation = new float[forestParent.childCount];
        y_rotation = new float[forestParent.childCount];
        z_rotation = new float[forestParent.childCount];
        scale = new float[forestParent.childCount];


        for (int i = 0; i < forestPaths.Length; i++) {
            forestPaths[i] = forestParent.GetChild(i).name;
            x_coordinates[i] = forestParent.GetChild(i).position.x;
            y_coordinates[i] = forestParent.GetChild(i).position.y;
            z_coordinates[i] = forestParent.GetChild(i).position.z;
            x_rotation[i] = forestParent.GetChild(i).localRotation.eulerAngles.x;
            y_rotation[i] = forestParent.GetChild(i).localRotation.eulerAngles.y;
            z_rotation[i] = forestParent.GetChild(i).localRotation.eulerAngles.z;
            scale[i] = forestParent.GetChild(i).localScale.x;
        }
    }

}
