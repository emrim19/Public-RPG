using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AdvancedCraftingUI : UI
{
    public Player player;

    public CraftingTable craftingTable;

    public CraftingRecipe recipe;

    public CraftingRecipe[] basicRecipes;
    public CraftingRecipe[] advancedRecipes;
    public CraftingRecipe[] allRecipes;
    public List<CraftingRecipe> tools = new List<CraftingRecipe>();
    public List<CraftingRecipe> craftingObjects = new List<CraftingRecipe>();

    public List<List<CraftingRecipe>> allTheLists = new List<List<CraftingRecipe>>();
    public int listIndex = 0;

    public CraftingSlot mainSlot;
    public CraftingSlot slot1;
    public CraftingSlot slot2;
    public CraftingSlot slot3;
    public CraftingSlot slot4;

    public Transform craftingParent;
    public MaterialSlot[] itemMaterialsSlots;

    public Button checkButton;
    public Button nextButton;
    public Button backButton;
    public int index = 0;

    public Text itemDescription;
    public Text itemCountText;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();

        checkButton.onClick.AddListener(ClickButton);
        nextButton.onClick.AddListener(ClickNext);
        backButton.onClick.AddListener(ClickBack);

        onItemChangedCallback += UpdateSlot;

        itemMaterialsSlots = craftingParent.GetComponentsInChildren<MaterialSlot>();

        SetRecipesInArray();
        PlaceRecipesInRespectiveList();

        UpdateSlot();
    }

    public void Update() {
        FocusCraftingTable();
    }

    public void FocusCraftingTable() {
        if (player.focus != null) {
            if (player.focus.GetType().ToString() == "CraftingTable") {
                craftingTable = player.focus as CraftingTable;
            }
            else if (player.focus.GetType().ToString() != "CraftingTable") {
                if (craftingTable != null) {
                    DeactivateUI();
                    craftingTable = null;
                }
            }
        }
        else if (player.focus == null) {
            if (craftingTable != null) {
                DeactivateUI();
                craftingTable = null;
            }
        }
    }


    public void SetRecipesInArray() {
        basicRecipes = Resources.LoadAll<CraftingRecipe>("Recipes");
        advancedRecipes = Resources.LoadAll<CraftingRecipe>("RecipesAdvanced");
        allRecipes = basicRecipes.Concat(advancedRecipes).ToArray();
    }

    public void PlaceRecipesInRespectiveList() {
        for (int i = 0; i < allRecipes.Length; i++) {
            if (allRecipes[i].result.item.type == ItemType.Equipment) {
                tools.Add(allRecipes[i]);
            }
            else if (allRecipes[i].result.item.type == ItemType.PlaceableObject) {
                craftingObjects.Add(allRecipes[i]);
            }
        }

        allTheLists.Add(tools);
        allTheLists.Add(craftingObjects);

        recipe = allTheLists[listIndex][index];
        
        
    }

    public void ClickButton() {
        Craft();
    }

    public void ClickButton1() {
        ClickBack();
        ClickBack();
    }
    public void ClickButton2() {
        ClickBack();
    }
    public void ClickButton3() {
        ClickNext();
    }
    public void ClickButton4() {
        ClickNext();
        ClickNext();
    }


    public void ButtonChooseList(int i) {
        listIndex = i;
        index = 0;
        recipe = allTheLists[listIndex][index];

        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
    }

    public void ClickNext() {
        index++;

        if (index > allTheLists[listIndex].Count - 1) {
            index = 0;
            recipe = allTheLists[listIndex][index];

            if (onItemChangedCallback != null) {
                onItemChangedCallback.Invoke();
            }

        }
        else {
            recipe = allTheLists[listIndex][index];

            if (onItemChangedCallback != null) {
                onItemChangedCallback.Invoke();
            }
        }
    }

    public void ClickBack() {
        index--;

        if (index < 0) {
            index = allTheLists[listIndex].Count - 1;
            recipe = allTheLists[listIndex][index];

            if (onItemChangedCallback != null) {
                onItemChangedCallback.Invoke();
            }
        }
        else {
            recipe = allTheLists[listIndex][index];

            if (onItemChangedCallback != null) {
                onItemChangedCallback.Invoke();
            }
        }
    }


    public void UpdateSlot() {
        mainSlot.AddItem(recipe.result.item);
        itemCountText.text = recipe.result.amount.ToString();
        itemDescription.text = recipe.result.item.description;
        ShowMaterials();

        if (index == 0) {
            slot1.AddItem(allTheLists[listIndex][allTheLists[listIndex].Count - 2].result.item);
            slot2.AddItem(allTheLists[listIndex][allTheLists[listIndex].Count - 1].result.item);
            slot3.AddItem(allTheLists[listIndex][index + 1].result.item);
            slot4.AddItem(allTheLists[listIndex][index + 2].result.item);
        }
        else if (index == 1) {
            slot1.AddItem(allTheLists[listIndex][allTheLists[listIndex].Count - 1].result.item);
            slot2.AddItem(allTheLists[listIndex][index - 1].result.item);
            slot3.AddItem(allTheLists[listIndex][index + 1].result.item);
            slot4.AddItem(allTheLists[listIndex][index + 2].result.item);
        }
        else if (index == allTheLists[listIndex].Count - 2) {
            slot1.AddItem(allTheLists[listIndex][index - 2].result.item);
            slot2.AddItem(allTheLists[listIndex][index - 1].result.item);
            slot3.AddItem(allTheLists[listIndex][index + 1].result.item);
            slot4.AddItem(allTheLists[listIndex][0].result.item);
        }
        else if (index == allTheLists[listIndex].Count- 1) {
            slot1.AddItem(allTheLists[listIndex][index - 2].result.item);
            slot2.AddItem(allTheLists[listIndex][index - 1].result.item);
            slot3.AddItem(allTheLists[listIndex][0].result.item);
            slot4.AddItem(allTheLists[listIndex][1].result.item);
        }
        else if (index >= 2 && index <= allTheLists[listIndex].Count - 2) {
            slot1.AddItem(allTheLists[listIndex][index - 2].result.item);
            slot2.AddItem(allTheLists[listIndex][index - 1].result.item);
            slot3.AddItem(allTheLists[listIndex][index + 1].result.item);
            slot4.AddItem(allTheLists[listIndex][index + 2].result.item);
        }
    }




    public void ShowMaterials() {
        for (int i = 0; i < itemMaterialsSlots.Length; i++) {
            if (i < recipe.materials.Count) {
                itemMaterialsSlots[i].AddItem(recipe.materials[i].item);
                itemMaterialsSlots[i].textAmount.text = recipe.materials[i].amount.ToString();
            }
            else {
                itemMaterialsSlots[i].ClearSlot();
                itemMaterialsSlots[i].textAmount.text = " ";
            }
        }


    }

    public bool CanCraft() {
        bool[] gotItems = new bool[recipe.materials.Count];

        if (Inventory.instance.items.Count > 0) {
            for (int i = 0; i < recipe.materials.Count; i++) {
                if (Inventory.instance.ContainsItem(recipe.materials[i].item)) {
                    Item someItem = Inventory.instance.items[Inventory.instance.FindItemIndex(recipe.materials[i].item)];
                    int amount = Inventory.instance.ItemCount(someItem);

                    if (amount >= recipe.materials[i].amount) {
                        gotItems[i] = true;
                    }
                    else {
                        gotItems[i] = false;
                    }
                }
            }
        }



        bool canCraft = true;
        for (int i = 0; i < gotItems.Length; i++) {
            if (gotItems[i] == false) {
                canCraft = false;
                break;
            }
        }

        return canCraft;
    }
    //outdated
    public bool CanCrafttttt() {
        bool[] allMaterials = new bool[recipe.materials.Count];

        if (Inventory.instance.items.Count > 0) {
            for (int i = 0; i < recipe.materials.Count; i++) {
                if (Inventory.instance.ContainsItem(recipe.materials[i].item)) {
                    if (Inventory.instance.ItemCount(recipe.materials[i].item) >= recipe.materials[i].amount) {
                        allMaterials[i] = true;
                    }
                    else {
                        allMaterials[i] = false;
                    }
                }
            }
        }
        bool canCraft = true;
        for (int i = 0; i < allMaterials.Length; i++) {
            if (allMaterials[i] == false) {
                canCraft = false;
                break;
            }
        }
        return canCraft;
    }


    public void Craft() {
        if (CanCraft()) {
            List<Item> theItems = GetMaterials();

            int i = 0;
            while (theItems.Count > 0) {
                if (Inventory.instance.items[i]._name != theItems[0]._name) {
                    i++;
                }
                else if (Inventory.instance.items[i]._name == theItems[0]._name) {
                    Inventory.instance.RemoveItem(Inventory.instance.items[i]);
                    theItems.RemoveAt(0);
                    i = 0;
                }
            }


            Item item = ScriptableObject.Instantiate(recipe.result.item);
            if (item.stackable) {
                item.name = item.name.Replace("(Clone)", "");
                item.amount = recipe.result.amount;
                Inventory.instance.AddItem(item);
            }
            else if (!item.stackable) {
                item.name = item.name.Replace("(Clone)", "");
                item.amount = 1;
                for (int j = 0; j < recipe.result.amount; j++) {
                    Inventory.instance.AddItem(item);
                }
            }
        }
    }

    public List<Item> GetMaterials() {
        List<Item> items = new List<Item>();
        for (int i = 0; i < recipe.materials.Count; i++) {
            for (int j = 0; j < recipe.materials[i].amount; j++) {
                items.Add(recipe.materials[i].item);
            }
        }
        return items;
    }
}
