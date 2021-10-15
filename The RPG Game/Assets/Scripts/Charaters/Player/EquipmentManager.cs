using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    
    #region
    public static EquipmentManager instance;

    private void Awake() {
        instance = this;

        inventory = Inventory.instance;

        int numOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numOfSlots];
        equipSlot = equipParent.GetComponentsInChildren<EquipSlot>();
        weaponTransform = GameObject.Find("PlayerRightHand").GetComponent<Transform>();
        shieldTransform = GameObject.Find("PlayerLeftHand").GetComponent<Transform>();
    }

    #endregion

    public Equipment[] currentEquipment;
    public EquipSlot[] equipSlot;

    public Ammo currentAmmo;
    public AmmoSlot ammoSlot;

    public Transform equipParent;

    private Inventory inventory;

    public Weapon weapon;
    public GameObject weaponPrefab; 
    public Transform weaponTransform;
   
    public Equipment shield;
    public GameObject shieldPrefab;
    public Transform shieldTransform;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;


    private void Start() {
        inventory = Inventory.instance;
        // onEquipmentChanged += UpdateEquipment;
    }


    private void Update() {
        InstantiateWeaponInHand();
        InstantiateShieldInhand();
        CheckTorch();
    }

    /*
    public void UpdateEquipment(Equipment newItem, Equipment oldItem) {

    }
    */

    //equip a new item
    public void Equip(Equipment newItem) {
        //find the slot where the item fits
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;
        //if there is already an item in the slot, put it back in inventory
        if (currentEquipment[slotIndex] != null) {
            oldItem = currentEquipment[slotIndex];
            inventory.AddItem(oldItem);
        }

        currentEquipment[slotIndex] = newItem;
        equipSlot[slotIndex].EquipInSlot(newItem);

        //trigger a callback whenever an item is equiped
        if (onEquipmentChanged != null) {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }
    }

    //unequip item and add it to inventory
    public void Unequip(int slotIndex) {
        if (currentEquipment[slotIndex] != null) {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.AddItem(oldItem);

            currentEquipment[slotIndex] = null;
            equipSlot[slotIndex].ClearSlot();
            Tooltip.HideTooltip_static();

            if (onEquipmentChanged != null) {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }

    public void EquipAmmo(Ammo ammo) {
        if (currentAmmo != null) {
            print(ammo._name + "/ " + currentAmmo._name);

            if (ammo._name == currentAmmo._name) {
                int newAmount = ammo.amount;
                currentAmmo.amount += newAmount;
                print(currentAmmo.amount);

            }
            else if (ammo._name != currentAmmo._name) {
                inventory.AddItem(currentAmmo);
                Ammo newAmmo = ScriptableObject.Instantiate(ammo);
                newAmmo.amount = ammo.amount;

                currentAmmo = newAmmo;
                ammoSlot.EquipInSlot(newAmmo);
            }
        }
        else if(currentAmmo == null) {
            Ammo newAmmo = ScriptableObject.Instantiate(ammo);
            newAmmo.amount = ammo.amount;

            currentAmmo = newAmmo;
            ammoSlot.EquipInSlot(newAmmo);
        }
    }

    public void UnequipAmmo() {
        if (currentAmmo != null) {
            Ammo oldItem = currentAmmo;
            inventory.AddItem(oldItem);

            currentAmmo = null;
            ammoSlot.ClearSlot();
            Tooltip.HideTooltip_static();
        }
    }

    public void RemoveAmmo() {
        if(currentAmmo != null) {
            if (currentAmmo.amount > 0) {
                currentAmmo.amount--;
            }
            if (currentAmmo.amount <= 0) {
                currentAmmo = null;
                ammoSlot.ClearSlot();
            }
        }
    }


    public void DestroyEquipment(int slotIndex) {
        if (currentEquipment[slotIndex] != null) {
            Equipment oldItem = currentEquipment[slotIndex];

            currentEquipment[slotIndex] = null;
            equipSlot[slotIndex].ClearSlot();
            Tooltip.HideTooltip_static();

            if (onEquipmentChanged != null) {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }

    //instantiate the weapon in hand when equiped/ delete it if not
    public void InstantiateWeaponInHand() {

        //if weapon is equiped, set it as the weapon variable
        if (currentEquipment[4] != null) {
            weapon = currentEquipment[4] as Weapon;
        }
        //if not, it is null
        else if (currentEquipment[4] == null) {
            weapon = null;
        }

        //if weapon variable is not null, instantiate the weapon in hand of player
        if (weapon != null) {
            string path = "Equiplables/" + weapon._name;
            path = path.Replace(" ", string.Empty);
            //print(path);
            GameObject theGameObject = Resources.Load<GameObject>(path);

            if (weaponPrefab == null) {
                weaponPrefab = theGameObject;
                Instantiate(theGameObject, weaponTransform);
            }
            if (weaponPrefab != theGameObject) {
                weaponPrefab = null;
                if (weaponTransform.childCount > 0) {
                    Destroy(weaponTransform.transform.GetChild(0).gameObject);
                }
            }

        }
        else if (weapon == null) {
            weaponPrefab = null;
            if (weaponTransform.childCount > 0) {
                Destroy(weaponTransform.transform.GetChild(0).gameObject);
            }
        }
    }

    public void InstantiateShieldInhand() {
        //if weapon is equiped, set it as the weapon variable
        if (currentEquipment[5] != null) {
            shield = currentEquipment[5] as Equipment;
        }
        //if not, it is null
        else if (currentEquipment[5] == null) {
            shield = null;
        }

        //if weapon variable is not null, instantiate the weapon in hand of player
        if (shield != null) {
            string path = "Equiplables/" + shield._name;
            path = path.Replace(" ", string.Empty);
            //print(path);
            GameObject theGameObject = Resources.Load<GameObject>(path);
            
            if (shieldPrefab == null) {
                shieldPrefab = theGameObject;
                Instantiate(theGameObject, shieldTransform);
            }
            if (shieldPrefab != theGameObject) {
                shieldPrefab = null;
                if (shieldTransform.childCount > 0) {
                    Destroy(shieldTransform.transform.GetChild(0).gameObject);
                }
            }

        }
        else if (shield == null) {
            shieldPrefab = null;
            if (shieldTransform.childCount > 0) {
                Destroy(shieldTransform.transform.GetChild(0).gameObject);
            }
        }
    }



    public float torchTimer = 1f;

    public void CheckTorch() {

        if(weapon != null) {
            if (weapon._name == "Torch") {
                if(torchTimer > 0) {
                    torchTimer -= Time.deltaTime;
                    if(torchTimer <= 0) {
                        weapon.LoseDurability(1);
                        torchTimer = 1f;
                    }
                }
            }
            if(weapon.currentDurability <= 0) {
                DestroyEquipment(4);
            }
        }
    }
}
