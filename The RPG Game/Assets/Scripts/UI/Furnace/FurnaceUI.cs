using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceUI : UI
{
    public Furnace furnace;
    private Player player;

    public FurnaceSlot inputSlot;
    public FurnaceSlot outputSlot;
    public FurnaceSlot bufferSlot;

    public Button inputButton;
    public Button outputButton;
    public Button bufferButton;

    public int oakNum;
    public int mahoganyNum;
    public Transform woodParent;
    public Button[] woodButtons;
    public Text[] woodTexts;

    public int ironNum;
    public int goldNum;
    public Transform mineralParent;
    public Button[] mineralButtons;
    public Text[] mineralTexts;

    public Slider fuelSlider;

    public List<Item> buffer = new List<Item>();
    public Bar[] bars;

    public float cookTimer;



    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();

        woodButtons = woodParent.GetComponentsInChildren<Button>();
        woodTexts = woodParent.GetComponentsInChildren<Text>();
        mineralButtons = mineralParent.GetComponentsInChildren<Button>();
        mineralTexts = mineralParent.GetComponentsInChildren<Text>();

        woodButtons[0].onClick.AddListener(PutOakOnFire);
        woodButtons[1].onClick.AddListener(PutMahoganyOnFire);

        mineralButtons[0].onClick.AddListener(PutIronInOven);
        mineralButtons[1].onClick.AddListener(PutGoldInOven);

        inputButton.onClick.AddListener(RemoveMineralFromOven);
        outputButton.onClick.AddListener(AddOutputToInventory);

        bufferButton.onClick.AddListener(AddBufferToInventory);

        inputSlot.ClearSlot();
        outputSlot.ClearSlot();
        bufferSlot.ClearSlot();
    }

    // Update is called once per frame
    void Update() {
        FocusFurnace();
        UpdateSlider();
        CookMineral();
        UpdateBuffer();
    }

    public void FocusFurnace() {
        if (player.focus != null) {
            if (player.focus.GetInstanceID() != furnace.GetInstanceID()) {
                DeactivateUI();
            }
        }
        else if (player.focus == null) {
            DeactivateUI();
        }
    }

    public void UpdateSlider() {
        fuelSlider.maxValue = furnace.totalFuelCapacity;
        fuelSlider.value = furnace.currentFuel;
    }

    //Place in interact function (in furnace)
    public void UpdateNumberOfWood() {
        oakNum = 0;
        mahoganyNum = 0;

        for (int i = 0; i < Inventory.instance.items.Count; i++) {
            if (Inventory.instance.items[i]._name == "Oak Logs") {
                oakNum++;
            }
            else if (Inventory.instance.items[i]._name == "Mahogany Logs") {
                mahoganyNum++;
            }
        }
        woodTexts[0].text = oakNum.ToString();
        woodTexts[1].text = mahoganyNum.ToString();
    }
    public void UpdateNumberOfMinerals() {
        ironNum = 0;
        goldNum = 0;

        for (int i = 0; i < Inventory.instance.items.Count; i++) {
            if (Inventory.instance.items[i]._name == "Iron Ore") {
                ironNum++;
            }
            else if (Inventory.instance.items[i]._name == "Gold Ore") {
                goldNum++;
            }
        }

        mineralTexts[0].text = ironNum.ToString();
        mineralTexts[1].text = goldNum.ToString();
    }

    public void UpdateBuffer() {
        if (buffer.Count > 0) {
            if (outputSlot.items.Count == 0) {
                for (int i = 0; i < buffer.Count; i++) {
                    outputSlot.Additem(buffer[i]);
                }
                buffer.Clear();
                bufferSlot.RemoveItem();
            }
        }
    }

    public void PutOakOnFire() {
        if (oakNum > 0) {
            for (int i = 0; i < Inventory.instance.items.Count; i++) {
                if (Inventory.instance.items[i]._name == "Oak Logs") {
                    Inventory.instance.RemoveAtIndex(i);
                    furnace.AddFuel(10);

                    UpdateNumberOfWood();
                    return;
                }
            }
        }

    }
    public void PutMahoganyOnFire() {
        if (mahoganyNum > 0) {
            for (int i = 0; i < Inventory.instance.items.Count; i++) {
                if (Inventory.instance.items[i]._name == "Mahogany Logs") {
                    Inventory.instance.RemoveAtIndex(i);
                    furnace.AddFuel(25);

                    UpdateNumberOfWood();
                    return;
                }
            }
        }
    }

    public void PutIronInOven() {
        if (ironNum > 0) {
            if (inputSlot.items.Count == 0 || inputSlot.items[0]._name == "Iron Ore") {
                for (int i = 0; i < Inventory.instance.items.Count; i++) {
                    if (Inventory.instance.items[i]._name == "Iron Ore") {
                        inputSlot.Additem(Inventory.instance.items[i]);
                        Inventory.instance.RemoveAtIndex(i);

                        UpdateNumberOfMinerals();
                        return;
                    }
                }
            }
        }
    }
    public void PutGoldInOven() {
        if (goldNum > 0) {
            if (inputSlot.items.Count == 0 || inputSlot.items[0]._name == "Gold Ore") {
                for (int i = 0; i < Inventory.instance.items.Count; i++) {
                    if (Inventory.instance.items[i]._name == "Gold Ore") {
                        inputSlot.Additem(Inventory.instance.items[i]);
                        Inventory.instance.RemoveAtIndex(i);

                        UpdateNumberOfMinerals();
                        return;
                    }
                }
            }
        }
    }


    public void RemoveMineralFromOven() {
        if (inputSlot.items != null) {
            if (inputSlot.items.Count >= 1) {
                Inventory.instance.AddItem(inputSlot.items[inputSlot.items.Count - 1]);
                cookTimer = 0;
                inputSlot.RemoveItem();

                UpdateNumberOfWood();
                UpdateNumberOfMinerals();
            }
        }
    }

    public void CookMineral() {
        if (buffer.Count == 0) {
            if (inputSlot.items != null) {
                if (inputSlot.items.Count >= 1) {
                    inputSlot.cookSlider.maxValue = 10;
                    inputSlot.cookSlider.value = cookTimer;

                    if (fuelSlider.value > 0) {
                        if (cookTimer < inputSlot.cookSlider.maxValue) {
                            cookTimer += Time.deltaTime;
                            if (cookTimer >= inputSlot.cookSlider.maxValue) {
                                PutBarInOutput();
                                cookTimer = 0;
                            }
                        }
                    }
                }
            }
        }
    }

    public void PutBarInOutput() {
        if (buffer.Count == 0) {
            if (inputSlot.items.Count >= 1 && cookTimer >= inputSlot.cookSlider.maxValue) {

                //Iron 
                if (inputSlot.items[inputSlot.items.Count - 1]._name == "Iron Ore") {
                    if (outputSlot.items.Count == 0 || outputSlot.items[0]._name == bars[0]._name) {
                        outputSlot.Additem(bars[0]);
                        inputSlot.RemoveItem();
                    }
                    else if (outputSlot.items[0]._name != bars[0]._name) {
                        buffer.Add(bars[0]);
                        bufferSlot.Additem(bars[0]);
                        inputSlot.RemoveItem();
                    }
                }
                //gold
                else if (inputSlot.items[inputSlot.items.Count - 1]._name == "Gold Ore") {
                    if (outputSlot.items.Count == 0 || outputSlot.items[0]._name == bars[1]._name) {
                        outputSlot.Additem(bars[1]);
                        inputSlot.RemoveItem();
                    }
                    else if (outputSlot.items[0]._name != bars[1]._name) {
                        buffer.Add(bars[1]);
                        bufferSlot.Additem(bars[1]);
                        inputSlot.RemoveItem();
                    }
                }
            }
        }
    }


    public void AddOutputToInventory() {
        if (outputSlot.items.Count >= 1) {
            Item item = ScriptableObject.Instantiate(outputSlot.items[outputSlot.items.Count - 1]);
            item.name = item.name.Replace("(Clone)", "");
            Inventory.instance.AddItem(item);
            outputSlot.RemoveItem();
        }
    }

    public void AddBufferToInventory() {
        if (bufferSlot.items.Count >= 1) {
            Item item = ScriptableObject.Instantiate(bufferSlot.items[bufferSlot.items.Count - 1]);
            item.name = item.name.Replace("(Clone)", "");
            Inventory.instance.AddItem(item);
            buffer.Clear();
            bufferSlot.RemoveItem();
        }
    }

}
