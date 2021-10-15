using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public enum ItemType {
    PlaceableObject,
    Wood,
    Mineral,
    Bar,
    Equipment, 
    Food,
    Ammo,
    Material
}


public abstract class Item : ScriptableObject {
    //protected Player player;
    protected LayerMask groundLayer;

    [SerializeField]
    protected string path;

    public GameObject prefab;
    public string _name;
    public bool stackable;
    public int amount;
    [TextArea(15, 20)]
    public string description;
    public Sprite icon;
    public ItemType type;


    public void Awake() {
        amount = 1;
        //player = GameObject.Find("Player").GetComponent<Player>();
        groundLayer = LayerMask.GetMask("GroundLayer");

        path = "Items/" +  type.ToString() + "/" + _name;
        path = path.Replace(" ", string.Empty);
        
    }


    public virtual void Use() {
        Debug.Log("Used " + _name);
    }

    public void RemoveFromInventory() {
        Inventory.instance.RemoveItem(this);
    }

    public void DropItemOnGround() {
        Inventory.instance.DropItem(this);
        Tooltip.HideTooltip_static();
    }

    public string GetPath() {
        return path;
    }

    public void SetPath(string path) {
        this.path = path;
    }

}
