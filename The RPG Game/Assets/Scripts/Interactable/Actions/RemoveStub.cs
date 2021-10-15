using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RemoveStub : Interactable
{

    public string _name;
    public Wood wood;


    protected override void Interact() {
        base.Interact();

        Wood theItem = ScriptableObject.Instantiate(wood);
        theItem.name = theItem.name.Replace("(Clone", string.Empty);
        SpawnGameObject(theItem.prefab);
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
