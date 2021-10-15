using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Object", menuName = "Item/PlaceableObject")]
public class PlaceableObject : Item
{
    public override void Use() {
        base.Use();
        GroundPlacementController.instance.SetPlaceableObject(this);
    }
}
