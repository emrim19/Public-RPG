using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampfireUI : UI
{
    public Player player;
    public GameObject campfireUI;
    public Campfire campfire;

    public Transform parentTransform;
    public Button[] buttons;
    public Text[] numTexts;

    public int oakNum;
    public int mahoganyNum;

    

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
        campfireUI = GameObject.Find("CampfireUI");

        buttons = parentTransform.GetComponentsInChildren<Button>();
        numTexts = parentTransform.GetComponentsInChildren<Text>();

        buttons[0].onClick.AddListener(PutOakOnFire);
        buttons[1].onClick.AddListener(PutMahoganyOnFire);

        onItemChangedCallback += UpdateNumberOfWood;
    }

    public void Update() {
        if (player.focus != null) {
            if (player.focus.GetType().ToString() == "Campfire") {
                campfire = player.focus as Campfire;
                UpdateNumberOfWood();
            }
            else if (player.focus.GetType().ToString() != "Campfire") {
                if (campfire != null) {
                    DeactivateUI();
                    campfire = null;
                }
            }
        }
        else if (player.focus == null) {
            if (campfire != null) {
                DeactivateUI();
                campfire = null;
            }
        }
    }

    public void UpdateNumberOfWood() {
        oakNum = 0;
        mahoganyNum = 0;

        for(int i = 0; i < Inventory.instance.items.Count; i++) {
            if(Inventory.instance.items[i]._name == "Oak Logs") {
                oakNum++;
            }
            else if (Inventory.instance.items[i]._name == "Mahogany Logs") {
                mahoganyNum++;
            }
        }

        numTexts[0].text = oakNum.ToString();
        numTexts[1].text = mahoganyNum.ToString();

    }

    public void PutOakOnFire() {
        if(oakNum > 0) {
            for(int i = 0; i < Inventory.instance.items.Count; i++) {
                if(Inventory.instance.items[i]._name == "Oak Logs") {
                    Inventory.instance.RemoveAtIndex(i);
                    campfire.AddFuel(10);
                    return;
                }
            }
        }
        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
    }

    public void PutMahoganyOnFire() {
        if (mahoganyNum > 0) {
            for (int i = 0; i < Inventory.instance.items.Count; i++) {
                if (Inventory.instance.items[i]._name == "Mahogany Logs") {
                    Inventory.instance.RemoveAtIndex(i);
                    campfire.AddFuel(25);
                    return;
                }
            }
        }
        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
    }



}
