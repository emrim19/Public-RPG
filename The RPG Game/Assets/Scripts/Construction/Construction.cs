using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Construction : Interactable{

    public string objectName;
    public PlaceableObject placeableObject;

    public void Awake() {
        gameObject.layer = LayerMask.NameToLayer("ObjectLayer");
        player = GameObject.Find("Player").GetComponent<Player>();
        gameObject.tag = "StaticObject";
        groundLayer = LayerMask.GetMask("GroundLayer");
    }
    
    public void PickUpObject() {
        player.MoveToPoint(transform.position);
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= GetRadius()) {
            OnPickingUpObject();
            Inventory.instance.AddItem(placeableObject);
            Destroy(transform.parent.gameObject);
        }
    }

    protected virtual void OnPickingUpObject() {
        //additional things to do when object is being picked up
    }

    public override void DoingAction() {
        base.DoingAction();
        PickUpObject();
    }



    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("StaticObject") || other.gameObject.CompareTag("Player")) {
            GroundPlacementController.instance.collisionsDetected++;
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("StaticObject") || other.gameObject.CompareTag("Player")) {
            GroundPlacementController.instance.collisionsDetected--;
        }
    }

    private void OnMouseOver() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }
        Tooltip.ShowTooltip_static(objectName);

        //MenuBar BAR
        if (Input.GetMouseButtonDown(1)) {
            MenuBar.SetTarget_static(this);
        }
    }

    private void OnMouseExit() {
        Tooltip.HideTooltip_static();
    }

    


}
