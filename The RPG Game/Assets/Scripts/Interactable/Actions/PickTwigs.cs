using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickTwigs : Interactable
{
    public string _name;

    public Item stick;

    public GameObject itemParent;

    public void Start() {
        itemParent = GameObject.Find("Items");
    }

    protected override void Interact() {
        base.Interact();
        for(int i = 0; i < Random.Range(1,4); i++) {
            GameObject prefab = Instantiate(stick.prefab, transform.position, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
            prefab.transform.SetParent(itemParent.transform);
        }
        Destroy(gameObject.transform.parent.gameObject);
    }


    private void OnMouseOver() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }
        Tooltip.ShowTooltip_static(_name);

        //MenuBar BAR
        if (Input.GetMouseButtonDown(1)) {
            MenuBar.SetTarget_static(this);
        }
    }

    private void OnMouseExit() {
        Tooltip.HideTooltip_static();
    }
}
