using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestGenerator : MonoBehaviour
{
    public int forestSize = 25;
    public int elementSpacing = 5;
    public Element[] elements;

    public int plantSpacing;
    public Plant[] plants;
    public Transform plantParent;

    public int NPC_Spacing;
    public NPC[] NPCs;
    public Transform NPC_Parent;
    
    public LayerMask groundLayer;
    

    private void Awake() {
        GenerateWorld();
    }

    public void GenerateWorld() {
        //for major elements liketrees and rocks
        for (int x = 0; x < forestSize; x += elementSpacing) {
            for (int z = 0; z < forestSize; z += elementSpacing) {

                for (int i = 0; i < elements.Length; i++) {
                    Element element = elements[i];

                    if (element.CanPlace()) {
                        RaycastHit hit;
                        Ray ray = new Ray(transform.position + Vector3.up * 100, Vector3.down);

                        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer)) {
                            if (hit.collider != null) {
                                Vector3 position = new Vector3(x, hit.point.y, z);
                                Vector3 offset = new Vector3(Random.Range(-1.25f, 1.25f), 0f, Random.Range(-1.25f, 1.25f));
                                Vector3 rotation = new Vector3(Random.Range(0, 0.5f), Random.Range(0, 360f), Random.Range(0, 0.5f));
                                Vector3 scale = Vector3.one * Random.Range(0.95f, 1.15f);

                                GameObject newElement = Instantiate(element.GetRandom());
                                newElement.name = newElement.name.Replace("(Clone)", string.Empty);
                                newElement.transform.SetParent(transform);
                                newElement.transform.position = position + offset;
                                newElement.transform.eulerAngles = rotation;
                                newElement.transform.localScale = scale;
                                break;
                            }
                        }
                    }
                }
            }
        }

        //for plants and other details
        for (int x = 0; x < forestSize; x += plantSpacing) {
            for (int z = 0; z < forestSize; z += plantSpacing) {

                for (int i = 0; i < plants.Length; i++) {
                    Plant plant = plants[i];

                    if (plant.CanPlace()) {
                        RaycastHit hit;
                        Ray ray = new Ray(transform.position + Vector3.up * 100, Vector3.down);

                        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer)) {
                            if (hit.collider != null) {
                                Vector3 position = new Vector3(x, hit.point.y, z);
                                Vector3 offset = new Vector3(Random.Range(-1.25f, 1.25f), 0f, Random.Range(-1.25f, 1.25f));
                                Vector3 rotation = new Vector3(Random.Range(0, 0.5f), Random.Range(0, 360f), Random.Range(0, 0.5f));
                                Vector3 scale = Vector3.one * Random.Range(0.95f, 1.15f);

                                GameObject newPlant = Instantiate(plant.GetRandom());
                                newPlant.transform.SetParent(plantParent);
                                newPlant.transform.position = position + offset;
                                newPlant.transform.eulerAngles = rotation;
                                newPlant.transform.localScale = scale;
                                break;
                            }
                        }
                    }
                }
            }
        }

        //for enemies
        for (int x = 0; x < forestSize; x += NPC_Spacing) {
            for (int z = 0; z < forestSize; z += NPC_Spacing) {

                for (int i = 0; i < NPCs.Length; i++) {
                    NPC npc = NPCs[i];

                    if (npc.CanPlace()) {
                        RaycastHit hit;
                        Ray ray = new Ray(transform.position + Vector3.up * 100, Vector3.down);

                        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer)) {
                            if (hit.collider != null) {
                                Vector3 position = new Vector3(x, hit.point.y, z);
                                Vector3 offset = new Vector3(Random.Range(-1.25f, 1.25f), 0f, Random.Range(-1.25f, 1.25f));
                                Vector3 rotation = new Vector3(Random.Range(0, 0.5f), Random.Range(0, 360f), Random.Range(0, 0.5f));
                                Vector3 scale = Vector3.one * Random.Range(0.95f, 1.15f);

                                GameObject newNPC = Instantiate(npc.GetRandom());
                                newNPC.transform.SetParent(NPC_Parent);
                                newNPC.transform.position = position + offset;
                                newNPC.transform.eulerAngles = rotation;
                                newNPC.transform.localScale = scale;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}



[System.Serializable]
public class Element {
    public string _name;

    [Range(1, 100)]
    public int density;
    
    public GameObject[] prefabs;

    public bool CanPlace() {
        if(Random.Range(0,100) < density) {
            return true;
        }
        else {
            return false;
        }
    }

    public GameObject GetRandom() {
        return prefabs[Random.Range(0, prefabs.Length)];
    }
}

[System.Serializable]
public class Plant {
    public string _name;

    [Range(1, 100)]
    public int density;

    public GameObject[] prefabs;

    public bool CanPlace() {
        if (Random.Range(0, 100) < density) {
            return true;
        }
        else {
            return false;
        }
    }

    public GameObject GetRandom() {
        return prefabs[Random.Range(0, prefabs.Length)];
    }
}

[System.Serializable]
public class NPC {
    public string _name;

    [Range(1, 100)]
    public int density;

    public GameObject[] prefabs;

    public bool CanPlace() {
        if (Random.Range(0, 100) < density) {
            return true;
        }
        else {
            return false;
        }
    }

    public GameObject GetRandom() {
        return prefabs[Random.Range(0, prefabs.Length)];
    }
}
