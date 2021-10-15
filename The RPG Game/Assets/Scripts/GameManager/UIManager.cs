using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region singelton

    public static UIManager instance;

    private void Awake() {
        if (instance != null) {
            Debug.LogWarning("Warning: More than one instance of UIManager found!");
            return;
        }
        instance = this;
    }

    #endregion


    public UI[] allUI;

    //ui for the buttons
    private UI inventoryUI;
    private UI statsUI;
    private UI abilitiesUI;
    private UI questsUI;
    private UI equipmentUI;
    private UI craftingUI;
    
    private UI optionsUI;

    private Button inventoryButton;
    private Button skillsButton;
    private Button abilityButton;
    private Button questButton;
    private Button equipmentButton;



    // Start is called before the first frame update
    void Start()
    {
        allUI = GetComponentsInChildren<UI>();

        inventoryUI = GameObject.Find("Inventory").GetComponent<UI>();
        statsUI = GameObject.Find("Stats").GetComponent<UI>();
        abilitiesUI = GameObject.Find("Abilities").GetComponent<UI>();
        questsUI = GameObject.Find("Quests").GetComponent<UI>();
        equipmentUI = GameObject.Find("Equipment").GetComponent<UI>();
        craftingUI = GameObject.Find("CraftingUI").GetComponent<UI>();

        optionsUI = GameObject.Find("OptionsUI").GetComponent<UI>();

        inventoryButton = GameObject.Find("InventoryButton").GetComponent<Button>();
        skillsButton = GameObject.Find("SkillTreeButton").GetComponent<Button>();
        abilityButton = GameObject.Find("AbilitiesButton").GetComponent<Button>();
        questButton = GameObject.Find("QuestsButton").GetComponent<Button>();
        equipmentButton = GameObject.Find("EquipmentButton").GetComponent<Button>();


        DeactivateAllUI();
        //equipmentUI.ActivateUI();
        //Invoke("Meh", 0.1f);
    }

    public void Meh() {
        equipmentUI.DeactivateUI();
    }

    private void Update() {
        if (!inventoryUI.IsActive()){
            craftingUI.DeactivateUI();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            DeactivateAllUI();
        }
        if (Input.GetKeyDown(KeyCode.Tab)) {
            optionsUI.ToggleUI();
        }
    }

    public void ToggleInventory() {
        inventoryUI.ToggleUI();
        statsUI.DeactivateUI();
        abilitiesUI.DeactivateUI();
        questsUI.DeactivateUI();
        equipmentUI.DeactivateUI();
    }

    public void ToggleSkills() {
        inventoryUI.DeactivateUI();
        statsUI.ToggleUI();
        abilitiesUI.DeactivateUI();
        questsUI.DeactivateUI();
        equipmentUI.DeactivateUI();
    }

    public void ToggleAbilities() {
        inventoryUI.DeactivateUI();
        statsUI.DeactivateUI();
        abilitiesUI.ToggleUI();
        questsUI.DeactivateUI();
        equipmentUI.DeactivateUI();
    }

    public void ToggleQuests() {
        inventoryUI.DeactivateUI();
        statsUI.DeactivateUI();
        abilitiesUI.DeactivateUI();
        questsUI.ToggleUI();
        equipmentUI.DeactivateUI();
    }

    public void ToggleEquipment() {
        inventoryUI.DeactivateUI();
        statsUI.DeactivateUI();
        abilitiesUI.DeactivateUI();
        questsUI.DeactivateUI();
        equipmentUI.ToggleUI();
    }

    //deactivate all UI
    public void DeactivateAllUI() {
        for(int i = 0; i < allUI.Length; i++) {
            allUI[i].DeactivateUI();
        }
    }


    //deactivate the main ui (invent, equipment etc.)
    public void DeactivateAllMainUI() {
        inventoryUI.DeactivateUI();
        statsUI.DeactivateUI();
        abilitiesUI.DeactivateUI();
        questsUI.DeactivateUI();
        equipmentUI.DeactivateUI();
    }

}
