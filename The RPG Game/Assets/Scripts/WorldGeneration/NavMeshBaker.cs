using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{

    [SerializeField]
    NavMeshSurface[] surfaces;


    public void Bake() {
        for(int i = 0; i < surfaces.Length; i++) {
            surfaces[i].BuildNavMesh();
        }
    }
}
