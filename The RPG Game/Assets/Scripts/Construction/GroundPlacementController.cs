using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundPlacementController : MonoBehaviour
{
    #region singelton

    public static GroundPlacementController instance;

    private void Awake() {
        if (instance != null) {
            Debug.LogWarning("Warning: More than one instance of GroundPlacementController found!");
            return;
        }
        instance = this;
    }

    #endregion

    private Player player;
    public GameObject objectParent;

    public Material prefabMat;
    public Material redMat;
    public Material blueMat;

    public PlaceableObject placeableObject;
    public GameObject currentPlaceableObject;
    public LayerMask groundLayer;

    

    public float rotation;

    public int collisionsDetected;
    public bool tooFarAway;

    private void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
        objectParent = GameObject.Find("PlaceableObjects");
    }

    // Update is called once per frame
    void Update()
    {
        CreateNewObject();
        SetMaterial();

        if (currentPlaceableObject != null) {
            player.isConstructing = true;

            CheckObjectPlacemenet();
            MoveObjectWithMouse();
            RotateObject();
            PlaceObject();
        }
        else {
            player.isConstructing = false;
        }
    }

    public void CreateNewObject() {
        if (placeableObject != null) {
            if (currentPlaceableObject == null) {
                collisionsDetected = 0;
                currentPlaceableObject = Instantiate(placeableObject.prefab);
                currentPlaceableObject.name = currentPlaceableObject.name.Replace("(Clone)", string.Empty);
                currentPlaceableObject.transform.parent = objectParent.transform;
                currentPlaceableObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                currentPlaceableObject.GetComponentInChildren<NavMeshObstacle>().enabled = false;
                currentPlaceableObject.GetComponentInChildren<Collider>().isTrigger = true;
                currentPlaceableObject.GetComponentInChildren<Renderer>().sharedMaterial = blueMat;
            }
            else if(currentPlaceableObject.name != placeableObject.prefab.name) {
                collisionsDetected = 0;
                currentPlaceableObject = Instantiate(placeableObject.prefab);
                currentPlaceableObject.name = currentPlaceableObject.name.Replace("(Clone)", string.Empty);
                currentPlaceableObject.transform.parent = objectParent.transform;
                currentPlaceableObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                currentPlaceableObject.GetComponentInChildren<NavMeshObstacle>().enabled = false;
                currentPlaceableObject.GetComponentInChildren<Collider>().isTrigger = true;
                currentPlaceableObject.GetComponentInChildren<Renderer>().sharedMaterial = blueMat;
            }
        }
        else if(placeableObject == null){
            Destroy(currentPlaceableObject);
        }
    }

    public void MoveObjectWithMouse() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer)){
            currentPlaceableObject.transform.position = hit.point;
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
    }

    public void RotateObject() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            rotation += 10;
        }
        else if (Input.GetKeyDown(KeyCode.E)) {
            rotation -= 10;
        }
        else {
            rotation += 0;
        }
        currentPlaceableObject.transform.Rotate(Vector3.up, rotation);
    }

    public void CheckObjectPlacemenet() {
        //cancel build
        if (Input.GetMouseButtonDown(1)) {
            //Inventory.instance.AddItem(placeableObject);
            placeableObject = null;

            Tooltip.HideTooltip_static();
        }

        //set color of object to red if building is not permittet, otherwise color blue
        if (collisionsDetected > 0 || tooFarAway) {
            currentPlaceableObject.GetComponentInChildren<Renderer>().sharedMaterial = redMat;
        }
        else if (collisionsDetected == 0 || !tooFarAway) {
            currentPlaceableObject.GetComponentInChildren<Renderer>().sharedMaterial = blueMat;
        }

        //check that the distance is not too far away from player to craft
        float distance = Vector3.Distance(player.transform.position, currentPlaceableObject.transform.position);
        if (distance <= currentPlaceableObject.GetComponentInChildren<Interactable>().GetRadius()) {
            tooFarAway = false;
        }
        else {
            tooFarAway = true;
        }
    }


    public void PlaceObject() {
        if (Input.GetMouseButtonDown(0) && collisionsDetected == 0 && !tooFarAway) {
            currentPlaceableObject.GetComponentInChildren<NavMeshObstacle>().enabled = true;
            currentPlaceableObject.GetComponentInChildren<Collider>().isTrigger = false;
            currentPlaceableObject.GetComponentInChildren<Renderer>().sharedMaterial = prefabMat;
            currentPlaceableObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("ObjectLayer");  
            currentPlaceableObject = null;
            placeableObject.RemoveFromInventory();
            placeableObject = null;
        }
    }


    public void SetMaterial() {
        if(placeableObject != null) {
            if (prefabMat == null) {
                prefabMat = placeableObject.prefab.gameObject.GetComponentInChildren<Renderer>().sharedMaterial;
            }
        }
    }


    public void SetPlaceableObject(PlaceableObject po) {
        placeableObject = po;
    }
}
