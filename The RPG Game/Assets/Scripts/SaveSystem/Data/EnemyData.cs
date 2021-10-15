using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyData
{
    public string[] enemyPaths;
    public float[] x_coordinates;
    public float[] y_coordinates;
    public float[] z_coordinates;
    public float[] x_rotation;
    public float[] y_rotation;
    public float[] z_rotation;
    public float[] scale;

    public EnemyData(Transform enemiesTransfornm) {
        enemyPaths = new string[enemiesTransfornm.childCount];
        x_coordinates = new float[enemiesTransfornm.childCount];
        y_coordinates = new float[enemiesTransfornm.childCount];
        z_coordinates = new float[enemiesTransfornm.childCount];
        x_rotation = new float[enemiesTransfornm.childCount];
        y_rotation = new float[enemiesTransfornm.childCount];
        z_rotation = new float[enemiesTransfornm.childCount];
        scale = new float[enemiesTransfornm.childCount];

        for (int i = 0; i < enemyPaths.Length; i++) {
            enemyPaths[i] = enemiesTransfornm.GetChild(i).name;
            x_coordinates[i] = enemiesTransfornm.GetChild(i).position.x;
            y_coordinates[i] = enemiesTransfornm.GetChild(i).position.y;
            z_coordinates[i] = enemiesTransfornm.GetChild(i).position.z;
            x_rotation[i] = enemiesTransfornm.GetChild(i).localRotation.eulerAngles.x;
            y_rotation[i] = enemiesTransfornm.GetChild(i).localRotation.eulerAngles.y;
            z_rotation[i] = enemiesTransfornm.GetChild(i).localRotation.eulerAngles.z;
            scale[i] = enemiesTransfornm.GetChild(i).localScale.x;
        }
    }
}
